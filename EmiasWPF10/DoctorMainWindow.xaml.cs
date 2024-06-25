using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmiasWPF10
{
    public partial class DoctorMainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private int doctorId;

        public DoctorMainWindow(int doctorId)
        {
            InitializeComponent();
            this.doctorId = doctorId;
            ScheduleDatePicker.SelectedDate = DateTime.Today;
            LoadDoctorSchedule(DateTime.Today);
        }

        private async void LoadDoctorSchedule(DateTime date)
        {
            try
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Doctors/{doctorId}/date/{date.ToString("yyyy-MM-dd")}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var appointments = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(data);
                    ScheduleDataGrid.ItemsSource = appointments;
                }
                else
                {
                    MessageBox.Show("Нет записей на выбранную дату.");
                    ScheduleDataGrid.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ScheduleDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScheduleDatePicker.SelectedDate.HasValue)
            {
                LoadDoctorSchedule(ScheduleDatePicker.SelectedDate.Value);
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class AppointmentViewModel
    {
        public TimeSpan AppointmentTime { get; set; }
        public string PatientFullName { get; set; }
        public string StatusName { get; set; }
    }
}
