using Kyrs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kyrs
{
    public class AuthManager
    {
        private string filePath = "users.json";
        private List<User> users;

        public AuthManager()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                users = new List<User>();
            }
            else
            {
                string json = File.ReadAllText(filePath);
                users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
        }

        public bool Authenticate(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Login == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}