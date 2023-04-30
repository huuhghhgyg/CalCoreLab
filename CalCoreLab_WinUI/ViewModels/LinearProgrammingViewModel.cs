using CalCore.LP;
using CalCoreLab_WinUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.ViewModels
{
    public partial class LinearProgrammingViewModel : ObservableObject
    {
        public string[] SymbolItems = new string[] { "≤", "=", "≥" };

        public ObservableCollection<LPItem> LPItemCollection = new();

        //Target targetType = Target.min;

        [ObservableProperty]
        string _consCoeffString = "12 34";

        [ObservableProperty]
        int _consSymbolIndex = 0;

        [ObservableProperty]
        string _consb = "5";

        [RelayCommand]
        void AddCons()
        {
            LPItemCollection.Add(new LPItem(ConsCoeffString, SymbolItems[ConsSymbolIndex], Consb));
            ConsCoeffString = string.Empty;
            ConsSymbolIndex = 0;
            Consb = string.Empty;
        }
    }
}
