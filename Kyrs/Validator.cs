using Kyrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kyrs
{
    public static class Validator
    {
        public static bool IsUserValid(User user)
        {
            // Проверка на валидность данных пользователя
            if (string.IsNullOrWhiteSpace(user.Login))
                return false;

            if (string.IsNullOrWhiteSpace(user.Email))
                return false;

            if (string.IsNullOrWhiteSpace(user.Password))
                return false;

            if (user.Password != user.ConfirmPassword)
                return false;

            return true;
        }
        public static class EmailValidator
        {
            public static bool IsValidEmail(string email)
            {
                // Проверяем формат электронной почты с помощью регулярного выражения
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                return Regex.IsMatch(email, pattern);
            }
        }
        public static bool IsValidComponent(Component component)
        {
            // Проверка на валидность компонента
            return !string.IsNullOrWhiteSpace(component.Name) &&
                   component.Quantity > 0 &&
                   component.PricePerUnit > 0;
        }

        public static bool AreFieldsFilled(params string[] fields)
        {
            // Проверка заполнения всех полей
            foreach (string field in fields)
            {
                if (string.IsNullOrWhiteSpace(field))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidQuantity(string quantity)
        {
            // Проверка корректности ввода количества
            return int.TryParse(quantity, out int result) && result > 0;
        }

        public static bool IsValidPrice(string price)
        {
            // Проверка корректности ввода цены
            return decimal.TryParse(price, out decimal result) && result > 0;
        }
    }
}
