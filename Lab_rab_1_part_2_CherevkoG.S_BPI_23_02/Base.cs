using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_rab_1_part_2_CherevkoG.S_BPI_23_02
{
    public abstract class Base
    {
        public string Name;
        public Base(string name)
        {
            Name = name;
        }
        public abstract double Raschetfunc(double x);

        public virtual Base Proizvodn()
        {
            {
                throw new NotImplementedException("Виртуальная беда");
            }
        }

        public virtual string FInfo()
        {
            return $"Функция: {Name}";
        }
        public string fname
        {
            get { return Name; }
        }
    }
}
