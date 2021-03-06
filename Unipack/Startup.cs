using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Unipack.Data;
using Unipack.Data.Interfaces;
using Unipack.Data.Services;
using Unipack.Options;

namespace Unipack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var initConfig = new Config();
            Configuration.GetSection("Secrets").Bind(initConfig);

            services.Configure<Config>(Configuration.GetSection("Secrets"));

            services.AddMemoryCache();
            services.AddAuthorization(o =>
            {
                o.AddPolicy("Moderators", s => s.Requirements.Add(new RolesAuthorizationRequirement(new string[] { "Admin", "Moderator" })));
                o.AddPolicy("Admins", s => s.RequireRole("Admin"));
            });
            services.AddControllers(o =>
                {

                    o.Filters.Add(new AuthorizeFilter());
                    o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                })
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                    }
                );
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Unipack API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<Context>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);

                options.LoginPath = "/api/account/login";
                options.AccessDeniedPath = "/api/account/denied";
                options.SlidingExpiration = true;
            });
            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IVacationService, VacationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPackListService, PackListService>();
            services.AddScoped<DataInit>();

            services.AddDbContext<Context>(options => options.UseSqlServer(initConfig.DatabaseDSN));

            var key = initConfig.UserSignInKey;
            var keyBytes = Encoding.UTF8.GetBytes(key ?? throw new ArgumentNullException(nameof(services)));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInit dataInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UsePathBase("/unipack");

            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
                option.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromDays(1),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets(webSocketOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //dataInit.InitAsync().Wait();
        }
    }
}
