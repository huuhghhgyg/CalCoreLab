using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common.Interfaces;

namespace CalCoreLab.ViewModels
{
    public partial class FormulaViewModel : ObservableObject, INavigationAware
    {
        private string _input="";
        [ObservableProperty]
        private string _result = "";
        public string Input
        {
            get => _input;
            set
            {
                SetProperty(ref _input, value);
                if (!string.IsNullOrEmpty(_input))
                {
                    try
                    {
                        Result = CalCore.Core.Calculate(_input);
                    }
                    catch (Exception ex)
                    {
                        Result = $"无法计算:{ex.Message}";
                    }
                }
            }
        }

        [RelayCommand]
        void Calculate()
        {
            Result = CalCore.Core.Calculate(Input);
        }

        void INavigationAware.OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        void INavigationAware.OnNavigatedTo()
        {
            //throw new NotImplementedException();
        }
    }
}
