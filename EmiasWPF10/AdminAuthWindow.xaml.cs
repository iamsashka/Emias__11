using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace EmiasWPF10
{
    public partial class AdminAuthWindow : Window
    {
        public AdminAuthWindow()
        {
            InitializeComponent();
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
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


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length > 0)
            {
                PlaceholderPasswordText.Visibility = Visibility.Collapsed;
            }
            else
            {
                PlaceholderPasswordText.Visibility = Visibility.Visible;
            }
        }

        private void Id_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmployeeNumberTextBox.Text == "Номер сотрудника")
            {
                EmployeeNumberTextBox.Text = "";
            }
        }

        private void Id_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeNumberTextBox.Text))
            {
                EmployeeNumberTextBox.Text = "Номер сотрудника";
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string employeeNumber = EmployeeNumberTextBox.Text;
            string password = PasswordBox.Password;

            var loginRequest = new LoginRequestt
            {
                EmployeeNumberr = employeeNumber,
                Passwordd = password
            };

            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonSerializer.Serialize(loginRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:7221/api/auth/adminlogin", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Авторизация успешна!");
                        AdminWindow adminMainWindow = new AdminWindow();
                        adminMainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверно");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к серверу: {ex.Message}");
                }
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
        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            PatientAuthWindow patientAuthWindow = new PatientAuthWindow();
            patientAuthWindow.Show();
            Close();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminAuthWindow adminAuthWindow = new AdminAuthWindow();
            adminAuthWindow.Show();
            Close();

        }

        private void Doctorк_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }

    public class LoginRequestt
    {
        public string EmployeeNumberr { get; set; }
        public string Passwordd { get; set; }
    }
}
