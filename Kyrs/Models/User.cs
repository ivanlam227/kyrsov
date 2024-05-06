using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 

        public User(string login, string email, string password, string confirmPassword)
        {
            Login = login;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
