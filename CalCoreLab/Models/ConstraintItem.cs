using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalCoreLab.Models
{
    public class ConstraintItem
    {
        public double[] Coeff { get; set; } = new double[0];
        public ConstraintType Symbol { get; set; }
        public double B { get; set; }
    }

    public enum ConstraintType 
    {
        LE = 0, // <=
        EQ = 1, // ==
        GE = 2, // >=
    }
}
