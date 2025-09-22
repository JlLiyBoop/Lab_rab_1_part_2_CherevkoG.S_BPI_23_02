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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void typefunc_SC(object sender , SelectedCellsChangedEventArgs e)
        {
            if (typefunc.SelectedItem is ComboBox selectedItem)
            {
                string functionType = selectedItem.Tag.ToString();
                osntext.IsEnabled = (functionType == "Log");
                osn.IsEnabled = (functionType == "Log");
            }   
        }
        private void schet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ftype = GetfType();
                double osn = 0;
                double x = 0;
                if (ftype == "Log")
                {
                    if (!ValidateOsn(out osn)) { return; }
                }
                if (!ValidateX(out x)) { return; }
            }
            catch (Exception ex) {
                MessageBox.Show("Что-то пошло не так");
            }
        }
        private string GetfType()
        {
            if (typefunc.SelectedItem is ComboBoxItem selectedItem)
                return selectedItem.Tag.ToString();
            return string.Empty;
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
