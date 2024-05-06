using Kyrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kyrs
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private UserManager userManager;

        public Registration()
        {
            InitializeComponent();
            userManager = new UserManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string email = Email.Text;
            string password = Pass.Text;
            string confirmPassword = ConfirmPassword.Text;

            try
            {
                // Создание нового пользователя
                User newUser = new User(login, email, password, confirmPassword);

                // Добавление пользователя
                userManager.AddUser(newUser);

                // Оповещение об успешной регистрации
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Закрытие окна регистрации
                Window s = new LoginWindow();
                this.Hide();
                s.Show(); ;
            }
            catch (ArgumentException ex)
            {
                // Вывод сообщения об ошибке при невалидных данных пользователя
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие окна регистрации
            Close();
        }
    }
}