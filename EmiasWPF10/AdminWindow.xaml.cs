using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;

namespace EmiasWPF10
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            using (var client = new HttpClient())
            {
                var authToken = (string)Application.Current.Properties["AuthToken"];
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = await client.GetAsync("https://localhost:7221/api/user");
                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadAsStringAsync();
                    var userList = JsonSerializer.Deserialize<List<User>>(users);
                    dataGrid.ItemsSource = userList;
                }
                else
                {
                    MessageBox.Show("Ошибка загрузки данных пользователей.");
                }
            }
        }

        private async void AddEntryButton_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                EmployeeNumber = "НовыйНомер",
                Password = "НовыйПароль",
                Role = "Doctor"
            };

            using (var client = new HttpClient())
            {
                var authToken = (string)Application.Current.Properties["AuthToken"];
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var json = JsonSerializer.Serialize(newUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7221/api/user", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Пользователь добавлен успешно!");
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Ошибка добавления пользователя.");
                }
            }
        }

        private async void ChangeEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is User selectedUser)
            {
                selectedUser.Password = "ИзмененныйПароль";

                using (var client = new HttpClient())
                {
                    var authToken = (string)Application.Current.Properties["AuthToken"];
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                    var json = JsonSerializer.Serialize(selectedUser);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:7221/api/user/{selectedUser.Id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Пользователь изменен успешно!");
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка изменения пользователя.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для изменения.");
            }
        }

        private async void DeleteEntryButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is User selectedUser)
            {
                var loggedInUserId = (int)Application.Current.Properties["UserId"];
                var authToken = (string)Application.Current.Properties["AuthToken"];

                if (selectedUser.Id == loggedInUserId)
                {
                    MessageBox.Show("Невозможно удалить текущего авторизованного пользователя.");
                    return;
                }

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7221/api/user/{selectedUser.Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Пользователь удален успешно!");
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления пользователя.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления.");
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
