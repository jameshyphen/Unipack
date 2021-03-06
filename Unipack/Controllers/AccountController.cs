﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Unipack.Data.Interfaces;
using Unipack.DTOs;
using Unipack.Models;
using Unipack.Options;

namespace Unipack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly string _signInKey;

        public AccountController(UserManager<IdentityUser> userManager, IUserService _userService, ILogger<AccountController> _logger, IOptions<Config> config)
        {
            this._userManager = userManager;
            this._userService = _userService;
            this._logger = _logger;
            this._signInKey = config.Value.UserSignInKey;
        }

        /// <summary>
        /// Registers a user. Returns a token and the created user.
        /// </summary>
        /// <param name="model">This is the RegisterDto model with the required information.</param>  
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        //POST : /api/account/register
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!_userService.UsernameAvailable(model.Username))
            {
                return BadRequest(new { message = "Username already taken." });
            }
            var identityUser = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                User newUser = new User(model.FirstName, model.LastName, model.Email, model.Username);
                _userService.Add(newUser);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", identityUser.Id),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_signInKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                User modelUser = newUser;

                UserDto user = new UserDto
                {
                    UserId = modelUser.UserId,
                    Email = modelUser.Email,
                    Username = modelUser.Username,
                    FirstName = modelUser.Firstname,
                    LastName = modelUser.Lastname
                };
                return Ok(new { token, userdto = user });
            }
            return BadRequest(model);
        }

        /// <summary>
        /// Authenticates user. Returns a token and the user.
        /// </summary>
        /// <param name="model">This is the LoginDto model with the required information.</param>  
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        //POST : /api/account/login
        public async Task<IActionResult> Login(LoginDto model)
              {
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            //_logger.LogInformation($"Call to /api/account/login with body {json}");

            var identityUser = await _userManager.FindByNameAsync(model.Username);
            if (identityUser != null && await _userManager.CheckPasswordAsync(identityUser, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", identityUser.Id),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_signInKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                User modelUser = _userService.GetByUserName(model.Username);

                UserDto user = new UserDto
                {
                    UserId = modelUser.UserId,
                    Email = modelUser.Email,
                    Username = modelUser.Username,
                    FirstName = modelUser.Firstname,
                    LastName = modelUser.Lastname
                };

                return Ok(new { token, userdto = user });
            }
            return BadRequest(new { message = "Username or password is incorrect." });

        }
    }
}
