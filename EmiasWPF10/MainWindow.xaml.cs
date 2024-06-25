using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace EmiasWPF10
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
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

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string employeeNumber = EmployeeNumberTextBox.Text;
            string password = PasswordBox.Password;

            var loginRequest = new LoginRequest
            {
                EmployeeNumber = employeeNumber,
                Password = password
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
                        var doctorId = await response.Content.ReadAsStringAsync();
                        DoctorMainWindow doctorMainWindow = new DoctorMainWindow(int.Parse(doctorId));
                        doctorMainWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный номер сотрудника или пароль");
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

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminAuthWindow adminAuthWindow = new AdminAuthWindow();
            adminAuthWindow.Show();
            Close();
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
    }

    public class LoginRequest
    {
        public string EmployeeNumber { get; set; }
        public string Password { get; set; }
    }
}
