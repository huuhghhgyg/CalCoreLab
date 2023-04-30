using CalCoreLab_WinUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.ViewModels
{
    public partial class CalculateViewModel : ObservableObject
    {
        private string _input = "";
        [ObservableProperty]
        private string _result = "";
        [ObservableProperty]
        private int cursorIndex;

        public string Input
        {
            get => _input;
            set
            {
                if (SetProperty(ref _input, value))
                    ClearContentCommand.NotifyCanExecuteChanged();

                if (!string.IsNullOrEmpty(_input))
                {
                    try
                    {
                        Calculate();
                    }
                    catch (Exception ex)
                    {
                        Result = $"无法计算:{ex.Message},输入值：({_input})";
                    }
                }
            }
        }

        bool CanClearInput => !string.IsNullOrEmpty(Input);

        [RelayCommand]
        void Calculate()
        {
            //预处理
            string input = Input;
            //input = input.Replace("%", "/100");
            input = Regex.Replace(input, @"(\d+(\.\d+)?)%", percentReplaceMatch); //正则表达式通过括号将匹配值分组

            //计算
            Result = CalCore.Core.Calculate(input);
        }

        // 替换方法
        string percentReplaceMatch(Match m) => $"({m.Groups[1].Value}/100)";

        [RelayCommand(CanExecute = nameof(CanClearInput))]
        void ClearContent()
        {
            Input = string.Empty;
        }
    }
}
