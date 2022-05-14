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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Globalization;
using System.ComponentModel;

namespace DataLibrary
{
    public partial class MainWindow : Window
    {
        public ViewData? DataV { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            DataV = new ViewData();
            this.DataContext = DataV;
            this.timesList.ItemsSource = DataV.benchmarks.times;
            this.accuracyList.ItemsSource = DataV.benchmarks.accuracy;
            this.MM.DataContext = DataV.benchmarks;
        }

        private void VMTime(object sender, RoutedEventArgs e)
        {
            try
            {
                Enum.TryParse(Function.Text, out VMf func);
                DataV.AddVMTime(new VMGrid(Convert.ToInt32(Nodes.Text), Convert.ToSingle(Start.Text), Convert.ToSingle(End.Text), func));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void VMAccuracy(object sender, RoutedEventArgs e)
        {
            try
            {
                Enum.TryParse(Function.Text, out VMf func);
                DataV.AddVMAccuracy(new VMGrid(Convert.ToInt32(Nodes.Text), Convert.ToSingle(Start.Text), Convert.ToSingle(End.Text), func));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MenuNewClick(object sender, RoutedEventArgs e)
        {
            if (DataV.Changed)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Изменения в данных не сохранены. Сохранить? ","Внимание!", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg_save = new Microsoft.Win32.SaveFileDialog();
                    if ((bool)dlg_save.ShowDialog())
                    {
                        DataV.Save(dlg_save.FileName);
                    }
                }
            }

            DataV.BenchmarksClear();
        }
        private void MenuOpenClick(object sender, RoutedEventArgs e)
        {
            if (DataV.Changed)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Изменения в данных не сохранены. Сохранить? ", "Внимание!", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg_save = new Microsoft.Win32.SaveFileDialog();
                    if ((bool)dlg_save.ShowDialog())
                    {
                        DataV.Save(dlg_save.FileName);
                    }
                }
            }

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if ((bool)dlg.ShowDialog())
            {
                DataV.BenchmarksClear();
                DataV.Load(dlg.FileName);
            }

        }
        private void MenuSaveClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if ((bool)dlg.ShowDialog())
            {
                DataV.Save(dlg.FileName);
                DataV.Changed = false;
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DataV.Changed)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Изменения в данных не сохранены. Сохранить? ", "Внимание!", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg_save = new Microsoft.Win32.SaveFileDialog();
                    if ((bool)dlg_save.ShowDialog())
                    {
                        DataV.Save(dlg_save.FileName);
                        DataV.Changed = false;
                    }
                }
            }
        }
    }

    [ValueConversion(typeof(Double[]), typeof(String))]
    public class min_max_converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    float[] min_max = (float[])value;
                    return "Минимальный: " + min_max[0] + "\nМаксимальный: " + min_max[1];
                }
                return DependencyProperty.UnsetValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }


    [ValueConversion(typeof(Boolean), typeof(String))]
    public class change_converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    if ((bool)value)
                    {
                        return "Коллекция была изменена.";
                    }
                    else
                    {
                        return "Коллекция не была изменена.";
                    }
                }
                return DependencyProperty.UnsetValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
