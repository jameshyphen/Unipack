using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Unipack.Models
{
    public class User
    {
        #region Properties

        public int UserId { get; set; }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (value.Length > 80 || string.IsNullOrEmpty(value))
                    throw new ArgumentException("Email invalid.");

                var emailValid = new MailAddress(value);

                if (emailValid.Address != value)
                    throw new FormatException("Invalid email.");
                _email = value;
            }
        }

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                if (value.Length > 50 || string.IsNullOrEmpty(value))
                    throw new ArgumentException("Username invalid.");
                _username = value;
            }
        }

        private string _firstName;

        public string Firstname
        {
            get => _firstName;
            set
            {
                if (value.Length > 25 || string.IsNullOrEmpty(value))
                    throw new ArgumentException("Firstname invalid.");
                _firstName = value;
            }
        }

        private string _lastName;

        public string Lastname
        {
            get => _lastName;
            set
            {
                if (value.Length > 25 || string.IsNullOrEmpty(value))
                    throw new ArgumentException("Lastname invalid.");
                _lastName = value;
            }
        }

        public ICollection<Vacation> Vacations { get; set; }

        #endregion


        #region Constructors

        public User() { }

        public User(string fName, string lName, string email, string username) : this()
        {
            Firstname = fName;
            Lastname = lName;
            Username = username;
            Email = email.ToLower();
            Vacations = new HashSet<Vacation>();
        }

        public User(string fName, string lName, string email) : this(fName, lName, email, fName + lName)
        {
        }

        #endregion


        #region Behavior

        public void AddVacation(Vacation vacation)
        {
            Vacations.Add(vacation);
        }

        #endregion

    }
}
