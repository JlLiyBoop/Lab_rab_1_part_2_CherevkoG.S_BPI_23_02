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
        public MainWindow()
        {
            InitializeComponent();
            typefunc.SelectedIndex = 0;
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
                }
                
                if (!ValidateX(out double x)) { return; }

                double result = thisBase.Raschetfunc(x);
                resulttext.Text = $"f({x}) = {result}";
            }
            catch (Exception ex) {
                MessageBox.Show("Что-то пошло не так");
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
            }
        }
        private string GetfType()
        {
            if (typefunc.SelectedItem is ComboBoxItem selectedItem)
                return selectedItem.Tag.ToString();
            return string.Empty;
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

            if (!double.TryParse(osntext.Text, out osn))
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

            if (!double.TryParse(xtext.Text, out x))
            {
                MessageBox.Show("x должен быть числом");
                return false;
            }

            return true;
        }
    }
}
