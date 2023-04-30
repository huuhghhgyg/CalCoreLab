using CalCore.LP;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.Models
{
    public partial class LPItem: ObservableObject
    {
        public LPItem(string consCoeff, string sym, string b)
        {
            ConsCoeff = consCoeff;
            Sym = sym;
            this.b = b;
        }

        public string ConsCoeff { get; set; }
        public string Sym { get; set; }
        public string b { get; set; }
    }
}
