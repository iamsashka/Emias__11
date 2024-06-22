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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EMIAS;
using EMIAS.Controllers;
using EMIAS.Models;

namespace EmiasWPF10
{
    public partial class AdminWindow : Window
    {
        private PatientsController patientsController;
        private DoctorsController doctorsController;
        private AdminsController adminController;
        private UserPage upage = new UserPage();
        private DoctorPage dpage = new DoctorPage();
        private AdminPage apage = new AdminPage();

        public AdminWindow()
        {
            InitializeComponent();
            List<string> Users = new List<string>() { "Пользователь", "Врач", "Администратор" };
            ComboBoxChoiceUser.SelectedItem = 0;
            dataGrid.ItemsSource = (System.Collections.IEnumerable)patientsController.GetPatients();
        }

        private async void AddEntryFunc(int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                Patient patient = new Patient()
                {
                    Oms = Convert.ToInt64(upage.PolicynumberTextBox),
                    Surname = upage.SurnameTextBox.Text,
                    Name = upage.NameTextBox.Text,
                    Patronymic = upage.PatronymicTextBox.Text,
                    BirthDate = DateOnly.Parse(upage.BirthdatesTextBox.Text),
                    Address = upage.AddressPropiskiTextBox.Text
                };

                await patientsController.PostPatient(patient);
            }
            else if (selectedIndex == 1)
            {
                Doctor doctor = new Doctor()
                {
                    Surname = dpage.SurnameTextBox.Text,
                    Name = dpage.NameTextBox.Text,
                    Patronymic = dpage.PatronymicTextBox.Text,
                    IdSpeciality = dpage.SpecialtyComboBox.SelectedIndex,
                    EnterPassword = dpage.AddressTextBox.Text,
                    WorkAddress = dpage.AddressTextBox.Text
                };

                await doctorsController.PostDoctor(doctor);
            }
            else if (selectedIndex == 2)
            {
                Admin admin = new Admin()
                {
                    Surname = apage.SurnameTextBox.Text,
                    Name = apage.NameTextBox.Text,
                    Patronymic = apage.PatronymicTextBox.Text,
                    EnterPassword = apage.PasswordTextBox.Text
                };

                await adminController.PostAdmin(admin);
            }
        }

        private async void ChangeEntryFunc(int selectedIndex, int selectedData)
        {

            if (selectedIndex == 0)
            {
                Patient patient = new Patient()
                {
                    Oms = Convert.ToInt64(upage.PolicynumberTextBox),
                    Surname = upage.SurnameTextBox.Text,
                    Name = upage.NameTextBox.Text,
                    Patronymic = upage.PatronymicTextBox.Text,
                    BirthDate = DateOnly.Parse(upage.BirthdatesTextBox.Text),
                    Address = upage.AddressPropiskiTextBox.Text
                };

                await patientsController.PutPatient((Int64)selectedData, patient);
            }
            else if (selectedIndex == 1)
            {
                Doctor doctor = new Doctor()
                {
                    Surname = dpage.SurnameTextBox.Text,
                    Name = dpage.NameTextBox.Text,
                    Patronymic = dpage.PatronymicTextBox.Text,
                    IdSpeciality = dpage.SpecialtyComboBox.SelectedIndex,
                    EnterPassword = dpage.AddressTextBox.Text,
                    WorkAddress = dpage.AddressTextBox.Text
                };

                await doctorsController.PutDoctor(selectedData, doctor);
            }
            else if (selectedIndex == 2)
            {
                Admin admin = new Admin()
                {
                    Surname = apage.SurnameTextBox.Text,
                    Name = apage.NameTextBox.Text,
                    Patronymic = apage.PatronymicTextBox.Text,
                    EnterPassword = apage.PasswordTextBox.Text
                };

                await adminController.PutAdmin(selectedData, admin);
            }

        }

        private async void DeleteEntryFunc(int selectedIndex, int selectedData)
        {
            if (selectedIndex == 0)
            {
                await patientsController.DeletePatient((Int64)selectedData);
            }
            else if (selectedIndex == 1)
            {
                await doctorsController.DeleteDoctor(selectedData);
            }
            else if (selectedIndex == 2)
            {
                await adminController.DeleteAdmin(selectedData);
            }
        }

        private void ComboBoxChoiceUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxChoiceUser.SelectedIndex == 0)
            {
                SelectedUserFrame.Content = new UserPage();
                dataGrid.ItemsSource = (System.Collections.IEnumerable)patientsController.GetPatients();
            }
            else if (ComboBoxChoiceUser.SelectedIndex == 1)
            {
                SelectedUserFrame.Content = new DoctorPage();
                dataGrid.ItemsSource = (System.Collections.IEnumerable)doctorsController.GetDoctors();
            }
            else if (ComboBoxChoiceUser.SelectedIndex == 2)
            {
                SelectedUserFrame.Content = new AdminPage();
                dataGrid.ItemsSource = (System.Collections.IEnumerable)adminController.GetAdmins();
            }
        }

        private void AddEntryButton_Click(object sender, RoutedEventArgs e)
        {
            AddEntryFunc(ComboBoxChoiceUser.SelectedIndex);
        }

        private void ChangeEntryButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeEntryFunc(ComboBoxChoiceUser.SelectedIndex, (int)dataGrid.SelectedItems[0]);
        }
        private void DeleteEntryButton_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteEntryFunc(ComboBoxChoiceUser.SelectedIndex, (int)dataGrid.SelectedItems[0]);
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //authentication_Window = new Authentication_Window;
            //Close();
            //authentication_Window.Show();

        }

    }
}