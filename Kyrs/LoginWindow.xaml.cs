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
    public partial class LoginWindow : Window
    {
        private AuthManager authManager;

        public LoginWindow()
        {
            InitializeComponent();
            authManager = new AuthManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = loginTextBox.Text;
            string password = passwordTextBox.Text;

            // Проверка логина и пароля
            if (authManager.Authenticate(username, password))
            {
                MessageBox.Show("Авторизация успешна!");

                // Создание и открытие нового окна MainWindow
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Закрытие текущего окна
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Попробуйте снова.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Window s = new Registration();
            this.Hide();
            s.Show();
        }
    }
}