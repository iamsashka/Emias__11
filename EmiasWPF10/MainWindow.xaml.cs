using System.Windows;

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
    }
}