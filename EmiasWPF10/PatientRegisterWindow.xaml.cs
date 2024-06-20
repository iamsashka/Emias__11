using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmiasWPF10
{
    /// <summary>
    /// Логика взаимодействия для PatientRegisterWindow.xaml
    /// </summary>
    public partial class PatientRegisterWindow : Window
    {
        public PatientRegisterWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.Theme == "LightTheme")
            {
                App.Theme = "DarkTheme";
            }
            else
            {
                App.Theme = "LightTheme";
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string nickname = NicknameTextBox.Text;
            string password = PasswordBox.Password;

            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(nickname) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверка логина на символ '@'
            if (!login.Contains("@"))
            {
                MessageBox.Show("Логин должен содержать символ '@'.");
                return;
            }

            // Проверка номера телефона на начало '+7' и длину 12 символов
            if (!phoneNumber.StartsWith("+7") || phoneNumber.Length != 12 || !Regex.IsMatch(phoneNumber, @"^\+7\d{10}$"))
            {
                MessageBox.Show("Номер телефона должен начинаться с '+7' и содержать 12 символов.");
                return;
            }

            // Пример отправки данных на сервер
            try
            {
                var httpClient = new HttpClient();
                var user = new { Login = login, PhoneNumber = phoneNumber, Nickname = nickname, Password = password };
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://yourapiendpoint.com/register", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Регистрация успешна.");
                    // Закрыть окно или выполнить другие действия
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка регистрации. Пожалуйста, попробуйте снова.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}

