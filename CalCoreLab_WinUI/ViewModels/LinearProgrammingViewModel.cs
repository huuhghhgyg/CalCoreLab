using CalCore.LP;
using CalCoreLab_WinUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.ViewModels
{
    public partial class LinearProgrammingViewModel : ObservableObject
    {
        public string[] SymbolItems = new string[] { "≤", "=", "≥" };

        public ObservableCollection<LPItem> LPItemCollection = new() {
            new LPItem("3 4", "≤", "9")
        };

        //Target targetType = Target.min;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddConsCommand))]
        string _consCoeffString = "5 2";

        [ObservableProperty]
        int _consSymbolIndex = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddConsCommand))]
        string _consb = "8";

        bool CanAddItem => ConsCoeffString.Length > 0 && Consb.Length > 0;

        [RelayCommand(CanExecute = nameof(CanAddItem))]
        void AddCons()
        {
            LPItemCollection.Add(new LPItem(ConsCoeffString, SymbolItems[ConsSymbolIndex], Consb));
            ConsCoeffString = string.Empty;
            ConsSymbolIndex = 0;
            Consb = string.Empty;
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DelConsCommand))]
        int _listSelectedIndex = -1;

        bool CanDeleteItem => ListSelectedIndex >= 0;

        [RelayCommand(CanExecute = nameof(CanDeleteItem))]
        void DelCons()
        {
            LPItemCollection.RemoveAt(ListSelectedIndex);
        }

        [ObservableProperty]
        int _solveTarget = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SolveLPCommand))]
        string _targetFunction = "10 5";

        bool CanSolve
        {
            get
            {
                if (TargetFunction.Length == 0) return false;

                return true;
            }
        }

        [ObservableProperty]
        string _solveOutput = string.Empty;

        [ObservableProperty]
        int _maxIterateCount = 1;

        [ObservableProperty]
        bool _isMaxIterateEnabled = false;

        uint? MaxIterate => IsMaxIterateEnabled ? (uint?)MaxIterateCount : null;

        [RelayCommand(CanExecute = nameof(CanSolve))]
        void SolveLP()
        {
            //目标函数
            string target = SolveTarget == 0 ? "min" : "max";
            double[] objFunc = Str2DoubleArray(TargetFunction);

            LPBuilder lp = new LPBuilder(target, objFunc);

            //约束方程
            foreach (LPItem item in LPItemCollection)
            {
                string coeff = item.ConsCoeff;
                coeff = coeff.Trim(); //删除前后的空格
                coeff = Regex.Replace(coeff, @"\s\s+", " ");
                try
                {
                    lp.AddConstraint(Str2DoubleArray(coeff), item.Sym, double.Parse(item.b));
                }
                catch(Exception ex)
                {
                    SolveOutput = $"{ex.Message}：在（{coeff}）的字符转数字时遇到格式错误。";
                    return;
                }
            }

            try
            {
                SolveOutput = lp.Solve(MaxIterate);
            }
            catch (Exception ex)
            {
                SolveOutput = $"求解失败：{ex.Message}";
            }
        }

        double[] Str2DoubleArray(string str)
        {
            string[] strArr = str.Split();
            var list = from v in strArr
                       select double.Parse(v);
            return list.ToArray();
        }
    }
}
