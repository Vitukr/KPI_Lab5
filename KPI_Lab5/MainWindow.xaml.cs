using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KPI_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        public Dictionary<string, string> AllSettings
        {
            get { return (Dictionary<string, string>)GetValue(AllSettingsProperty); }
            set { SetValue(AllSettingsProperty, value); }
        }
        public static readonly DependencyProperty AllSettingsProperty = DependencyProperty.Register("AllSettings", typeof(Dictionary<string, string>), typeof(MainWindow), new PropertyMetadata(new Dictionary<string, string>()));

        public KeyValuePair<string, string> CurrentSetting
        {
            get { return (KeyValuePair<string, string>)GetValue(CurrentSettingProperty); }
            set { SetValue(CurrentSettingProperty, value); }
        }
        public static readonly DependencyProperty CurrentSettingProperty = DependencyProperty.Register("CurrentSetting", typeof(KeyValuePair<string, string>), typeof(MainWindow), new PropertyMetadata(new KeyValuePair<string, string>()));

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void ButtonAll_Click(object sender, RoutedEventArgs e)
        {
            AllSettings = ConfigSettings.ReadAllSettings();
        }

        private void ButtonAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxKey.Text))
            {
                ConfigSettings.AddUpdateAppSettings(TextBoxKey.Text.Trim(), TextBoxValue.Text.Trim());
                AllSettings = ConfigSettings.ReadAllSettings();
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentSetting.Key))
            {
                ConfigSettings.RemoveSetting(CurrentSetting.Key);
                AllSettings = ConfigSettings.ReadAllSettings();
            }
        }

        private void DataGridAppSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridAppSettings.SelectedItem is KeyValuePair<string, string> kvp)
            {
                CurrentSetting = new KeyValuePair<string, string>(kvp.Key, kvp.Value);
            }
            else
            {
                CurrentSetting = new KeyValuePair<string, string>();
            }
        }
    }
}