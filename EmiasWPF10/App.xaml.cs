using System;
using System.Windows;

namespace EmiasWPF10
{
    public partial class App : Application
    {
        private static string theme = "LightTheme";
        public static string Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                ResourceDictionary newTheme = new ResourceDictionary();
                newTheme.Source = new Uri($"Resources/{theme}.xaml", UriKind.Relative);

                ResourceDictionary oldTheme = Current.Resources.MergedDictionaries[0];
                Current.Resources.MergedDictionaries.Remove(oldTheme);
                Current.Resources.MergedDictionaries.Add(newTheme);
            }
        }
    }
}