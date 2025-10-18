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

namespace Lab_rab_1_part_2_CherevkoG.S_BPI_23_02
{

    public partial class MainWindow : Window
    {
        private Base thisBase;

        private bool DarkTheme = false;
        public MainWindow()
        {
            InitializeComponent();
            typefunc.SelectedIndex = 0;
        }

        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DarkTheme = !DarkTheme;
            ChangeTheme();
        }

        private void ChangeTheme()
        {
            var background = DarkTheme ? Color.FromRgb(45, 45, 48) : Colors.White;
            var foreground = DarkTheme ? Color.FromRgb(208, 208, 208) : Colors.Black;
            var controlbackground = DarkTheme ? Color.FromRgb(30, 30, 30) : Colors.White;
            var border = DarkTheme ? Color.FromRgb(62, 62, 66) : Colors.LightGray;
            var buttonbackground = DarkTheme ? Color.FromRgb(51, 51, 55) : Color.FromRgb(221, 221, 221);

            MainGrid.Background = new SolidColorBrush(background);

            this.Background = new SolidColorBrush(background);
            MainGrid.Background = new SolidColorBrush(background);

            foreach (TextBlock textBlock in FindAllInType<TextBlock>(this))
            {
                textBlock.Foreground = new SolidColorBrush(foreground);
            }

            typefunc.Background = new SolidColorBrush(controlbackground);
            typefunc.Foreground = new SolidColorBrush(foreground);
            typefunc.BorderBrush = new SolidColorBrush(border);

            foreach (TextBox textBox in FindAllInType<TextBox>(this))
            {
                textBox.Background = new SolidColorBrush(controlbackground);
                textBox.Foreground = new SolidColorBrush(foreground);
                textBox.BorderBrush = new SolidColorBrush(border);
                textBox.CaretBrush = new SolidColorBrush(foreground);
            }

            foreach (Button button in FindAllInType<Button>(this))
            {
                if (button != ThemeToggleButton)
                {
                    button.Background = new SolidColorBrush(buttonbackground);
                    button.Foreground = new SolidColorBrush(foreground);
                    button.BorderBrush = new SolidColorBrush(border);
                }
            }

            ThemeToggleButton.Content = DarkTheme ? "Светлая тема" : "Темная тема";
        }

        private static IEnumerable<T> FindAllInType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindAllInType<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void Typefunc_SC(object sender, SelectionChangedEventArgs e)
        {
            if (typefunc.SelectedItem is ComboBoxItem selectedItem)
            {
                string functionType = selectedItem.Tag.ToString();
                osntext.IsEnabled = (functionType == "Log");
                osn.IsEnabled = (functionType == "Log");

                CreateBase(functionType);
            }   
        }
        private void CreateBase(string functionType)
        {
            try
            {
                if (functionType == "Ln")
                {
                    thisBase = new Ln();
                }
                else if (functionType == "Log")
                {
                    if (string.IsNullOrWhiteSpace(osntext.Text))
                    {
                        thisBase = null;
                        infotext.Text = "Введите основание логарифма";
                        return;
                    }
                    if (!ValidateOsn(out double osn))
                    {
                        thisBase = null;
                        return;
                    }
                    if (osn <= 0 || osn == 1)
                    {
                        MessageBox.Show("Основание должно быть больше 0 и не равно 1");
                        thisBase = null;
                        return;
                    }
                    thisBase = new Log(osn);
                }

                if (thisBase != null)
                {
                    infotext.Text = thisBase.FInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания функции: {ex.Message}");
                thisBase = null;
            }
        }
        private void schet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (thisBase == null)
                {
                    MessageBox.Show("Сначала выбери функцию");
                    return;
                }
                
                if (!ValidateX(out double x)) { return; }

                double result = thisBase.Raschetfunc(x);
                resulttext.Text = $"f({x}) = {result}";
            }
            catch (Exception ex) {
                MessageBox.Show("Что-то пошло не так");
                resulttext.Text = "Ошибка";
            }
        }
        private void proizv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (thisBase == null)
                {
                    MessageBox.Show("Сначала создайте функцию");
                    return;
                }

                if (!ValidateX(out double x))
                    return;

                Base proizv = thisBase.Proizvodn();
                double proizvznach = proizv.Raschetfunc(x);

                resulttext.Text = $"f'({x}) = {proizvznach}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вычисления производной: {ex.Message}");
                resulttext.Text = "Ошибка";
            }
        }

        private void Osntext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (typefunc.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag.ToString() == "Log")
            {
                CreateBase("Log");
            }
        }
        private bool ValidateOsn(out double osn)
        {
            osn = 0;
            if (string.IsNullOrWhiteSpace(osntext.Text))
            {
                MessageBox.Show("Введите основание логарифма");
                return false;
            }
            string osnText = osntext.Text.Replace('.', ',');
            if (!double.TryParse(osnText, out osn))
            {
                MessageBox.Show("Основание должно быть числом");
                return false;
            }

            if (osn <= 0 || osn == 1)
            {
                MessageBox.Show("Основание должно быть больше 0 и не равно 1");
                return false;
            }

            return true;
        }

        private bool ValidateX(out double x)
        {
            x = 0;
            if (string.IsNullOrWhiteSpace(xtext.Text))
            {
                MessageBox.Show("Введите значение x");
                return false;
            }
            string xText = xtext.Text.Replace('.', ',');
            if (!double.TryParse(xText, out x))
            {
                MessageBox.Show("x должен быть числом");
                return false;
            }

            return true;
        }
    }
}
