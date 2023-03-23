using CalCore;
using CalCore.LP;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common.Interfaces;

namespace CalCoreLab.ViewModels
{
    public partial class LinearProgrammingViewModel : ObservableObject, INavigationAware
    {
        Matrix targetFunctionMatrix = new Matrix(1, 1);

        string targetFunctionString = string.Empty;

        public string TargetFunctionString
        {
            get => targetFunctionString;
            set
            {
                SetProperty(ref targetFunctionString, value);
                try
                {
                    targetFunctionMatrix = new Matrix(targetFunctionString);
                    TargetFunctionInfo = $"{targetFunctionMatrix.Col}个变量";
                }
                catch(Exception ex)
                {
                    TargetFunctionInfo = $"错误：{ex.Message}";
                }
            }
        }

        [ObservableProperty]
        string targetFunctionInfo="无信息";

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo()
        {
            //throw new NotImplementedException();
        }
    }
}
