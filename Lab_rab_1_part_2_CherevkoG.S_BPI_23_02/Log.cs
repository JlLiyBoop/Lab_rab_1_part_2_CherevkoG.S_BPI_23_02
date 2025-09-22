using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_rab_1_part_2_CherevkoG.S_BPI_23_02
{
    internal class Log : Base
    {
        private double _znachosn;

        public Log(double znachosn) : base($"log{znachosn}(x)")
        {
            if (znachosn <= 0 || znachosn == 1 )
            {
                MessageBox.Show("Значение основания логарифма должно быть больше нуля и не равно единице!");
            }
            _znachosn = znachosn;
        }
        public override double Raschetfunc(double x)
        {
            if ( x < 0) { MessageBox.Show("Аргумент логарифма должно быть больше нуля"); }
            return Math.Log(x, _znachosn);    
        }
        public override Base Proizvodn()
        {
            return new ProizvFunc(this, $"1/(x * ln({_znachosn}))");
        }

        public override string FInfo()
        {
            return base.FInfo() + $"Основание: {_znachosn} Область определения: x > 0\n";
        }

        public double Znachosn => _znachosn;
    }
}
