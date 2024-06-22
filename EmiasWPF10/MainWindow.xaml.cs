﻿using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmiasWPF10
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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
                        MessageBox.Show("Авторизация успешна!");
                        DoctorMainWindow doctorMainWindow = new DoctorMainWindow();
                        doctorMainWindow.Show();
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
        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            PatientAuthWindow patientAuthWindow = new PatientAuthWindow();
            patientAuthWindow.Show();
            Close();
        }

    }
    public class LoginRequest
    {
        public string EmployeeNumber { get; set; }
        public string Password { get; set; }
    }
}