using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Kyrs.Models;
using Newtonsoft.Json;
using static Kyrs.Validator;

namespace Kyrs
{
    public class UserManager
    {
        private readonly string _filePath = "users.json";

        public UserManager()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        private List<User> GetAllUsers()
        {
            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        public void AddUser(User user)
        {
            if (!Validator.IsUserValid(user))
            {
                throw new ArgumentException("Недопустимые данные пользователя. Пожалуйста, проверьте введенные данные.");
            }

            if (!Validator.EmailValidator.IsValidEmail(user.Email))
            {
                throw new ArgumentException("Некорректный формат email.");
            }

            List<User> users = GetAllUsers();
            users.Add(user);
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }

}