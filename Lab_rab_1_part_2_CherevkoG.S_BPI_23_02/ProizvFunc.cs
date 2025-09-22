using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_rab_1_part_2_CherevkoG.S_BPI_23_02
{
    public class ProizvFunc : Base
    {
        private Base proizfunc;
        private string proizvexp;

        public ProizvFunc(Base func, string exp)
            : base($"d/dx({func.Name})")
        {
            proizfunc = func;
            proizvexp = exp;
        }

        public override double Raschetfunc(double x)
        {
            double h = 0.0001;
            try
            {
                double f_x_plus_h = proizfunc.Raschetfunc(x + h);
                double f_x = proizfunc.Raschetfunc(x);
                return (f_x_plus_h - f_x) / h;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка вычисления производной: {ex.Message}");
            }
        }

        public override Base Proizvodn()
        {
            return new ProizvFunc(this, $"d²/dx²({proizfunc.Name})");
        }

        public override string FInfo()
        {
            return $"Производная функции: {proizfunc.Name}" +
                   $"Выражение: {proizvexp}" +
                   $"Метод: численное дифференцирование";
        }


        public string Exp
        {
            get { return proizvexp; }
        }

        public Base Proizfunc
        {
            get { return proizfunc; }
        }
    }
}
