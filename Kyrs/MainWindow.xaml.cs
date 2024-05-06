using Kyrs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kyrs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ComponentRepository _componentRepository;

        public MainWindow()
        {
            InitializeComponent();
            _componentRepository = new ComponentRepository("components.json");

            // Инициализация списка категорий
            comboBoxCategory.ItemsSource = new List<string> { "Механические компоненты", "Электрические компоненты", "Гидравлические компоненты", "Система нагрева", "Другие комплектующие" };

            // Загрузка компонентов в DataGrid при запуске окна
            LoadComponents();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string componentName = txtComponentName.Text;
            string manufacturer = txtManufacturer.Text;
            string articleNumber = txtArticleNumber.Text;
            string category = comboBoxCategory.SelectedItem as string;
            string quantityText = txtQuantity.Text;
            string pricePerUnitText = txtPricePerUnit.Text;

            // Проверяем, все ли поля заполнены
            if (!Validator.AreFieldsFilled(componentName, manufacturer, articleNumber, category, quantityText, pricePerUnitText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверяем корректность ввода количества и цены за единицу
            if (!Validator.IsValidQuantity(quantityText))
            {
                MessageBox.Show("Пожалуйста, введите корректное значение количества.");
                return;
            }

            if (!Validator.IsValidPrice(pricePerUnitText))
            {
                MessageBox.Show("Пожалуйста, введите корректное значение цены за единицу.");
                return;
            }

            // Создаем новый объект Component
            Component newComponent = new Component
            {
                Name = componentName,
                Quantity = int.Parse(quantityText),
                Manufacturer = manufacturer,
                PricePerUnit = decimal.Parse(pricePerUnitText),
                Category = category,
                ArticleNumber = articleNumber
            };

            // Проверяем валидность компонента с помощью класса Validator
            if (!Validator.IsValidComponent(newComponent))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                return;
            }

            // Добавляем компонент в репозиторий
            _componentRepository.Add(newComponent);

            MessageBox.Show($"Добавлено комплектующее: {newComponent.Name}");

            // Очищаем поля ввода после добавления компонента
            txtComponentName.Clear();
            txtQuantity.Clear();
            txtPricePerUnit.Clear();
            txtManufacturer.Clear();
            txtArticleNumber.Clear();
            comboBoxCategory.SelectedIndex = -1;

            // Загрузка обновленных компонентов в DataGrid
            LoadComponents();
        }


        private void LoadComponents()
        {
            // Получаем все компоненты из репозитория
            List<Component> components = _componentRepository.GetAll();

            // Устанавливаем источник данных для DataGrid
            componentsDataGrid.ItemsSource = components;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (componentsDataGrid.SelectedItem != null)
            {
                Component selectedComponent = (Component)componentsDataGrid.SelectedItem;
                _componentRepository.Delete(selectedComponent);
                LoadComponents();
            }
        }

        private void componentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (componentsDataGrid.SelectedItem != null)
            {
                // Получаем выбранный компонент
                Component selectedComponent = (Component)componentsDataGrid.SelectedItem;

                // Загружаем его свойства в текстовые поля
                txtComponentName.Text = selectedComponent.Name;
                txtQuantity.Text = selectedComponent.Quantity.ToString();
                txtManufacturer.Text = selectedComponent.Manufacturer;
                txtPricePerUnit.Text = selectedComponent.PricePerUnit.ToString();
                comboBoxCategory.SelectedItem = selectedComponent.Category;
                txtArticleNumber.Text = selectedComponent.ArticleNumber;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string componentName = txtComponentName.Text;
            string manufacturer = txtManufacturer.Text;
            string articleNumber = txtArticleNumber.Text;
            string category = comboBoxCategory.SelectedItem as string;
            string quantityText = txtQuantity.Text;
            string pricePerUnitText = txtPricePerUnit.Text;

            // Проверяем, все ли поля заполнены
            if (!Validator.AreFieldsFilled(componentName, manufacturer, articleNumber, category, quantityText, pricePerUnitText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверяем корректность ввода количества и цены за единицу
            if (!Validator.IsValidQuantity(quantityText))
            {
                MessageBox.Show("Пожалуйста, введите корректное значение количества.");
                return;
            }

            if (!Validator.IsValidPrice(pricePerUnitText))
            {
                MessageBox.Show("Пожалуйста, введите корректное значение цены за единицу.");
                return;
            }

            // Создаем новый объект Component с обновленными данными
            Component updatedComponent = new Component
            {
                Name = componentName,
                Quantity = int.Parse(quantityText),
                Manufacturer = manufacturer,
                PricePerUnit = decimal.Parse(pricePerUnitText),
                Category = category,
                ArticleNumber = articleNumber
            };

            // Проверяем валидность компонента с помощью класса Validator
            if (!Validator.IsValidComponent(updatedComponent))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                return;
            }

            // Если все проверки пройдены успешно, обновляем компонент в репозитории
            if (componentsDataGrid.SelectedItem != null)
            {
                Component selectedComponent = (Component)componentsDataGrid.SelectedItem;
                _componentRepository.Update(selectedComponent, updatedComponent);
                LoadComponents();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window s = new LoginWindow();
            this.Hide();
            s.Show(); 
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


}

