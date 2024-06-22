using System;
using System.Windows;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        private void Polis_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PolicyNumberTextBox.Text == "Номер полиса")
            {
                PolicyNumberTextBox.Text = "";
            }
        }

        private void Polis_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PolicyNumberTextBox.Text))
            {
                PolicyNumberTextBox.Text = "Номер полиса";
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string policyNumber = PolicyNumberTextBox.Text;

            var loginRequest = new PatientLoginRequest
            {
                PolicyNumber = policyNumber
            };

            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonSerializer.Serialize(loginRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:7221/api/auth/login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Авторизация успешна!");
                        // Откройте окно для пациентов, например PatientMainWindo
                    }
                    else
                    {
                        MessageBox.Show("Неверный номер полиса или пароль.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к серверу: {ex.Message}");
                }
            }
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    public class PatientLoginRequest
    {
        public string PolicyNumber { get; set; }
        public string Password { get; set; }
    }
}
