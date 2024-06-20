using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    public partial class PatientAuthWindow : Window
    {
        public PatientAuthWindow()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Обработчик события загрузки окна
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

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string nickname = PatientNumberTextBox.Text;
            string password = PasswordBox.Password;

            var loginRequest = new PatientLoginRequest
            {
                Nickname = nickname,
                Password = password
            };

            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonSerializer.Serialize(loginRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:7221/api/auth/patientLogin", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Авторизация успешна!");
                        // переход на окно для пациента
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к серверу: {ex.Message}");
                }
            }
        }

        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            PatientAuthWindow patientAuthWindow = new PatientAuthWindow();
            patientAuthWindow.Show();
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            PatientRegisterWindow registerWindow = new PatientRegisterWindow();
            registerWindow.Show();
            Close();
        }
    }

    public class PatientLoginRequest
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}

