using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_rab_1_part_2_CherevkoG.S_BPI_23_02
{
    internal class Ln : Base
    {
        public Ln() : base("ln(x)") { }
        public override double Raschetfunc(double x)
        {
            if (x <= 0)
                throw new ArgumentException("Аргумент логарифма должен быть больше нуля");
            return Math.Log(x);
        }
        public override Base Proizvodn()
        {
            return new ProizvFunc(this, "1/x");
        }
        public override string FInfo()
        {
            return base.FInfo() + "Область определения: x > 0\n";
        }
    }
}
