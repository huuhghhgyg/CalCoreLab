using CalCoreLab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common.Interfaces;

namespace CalCoreLab.Views.Pages
{
    /// <summary>
    /// FormulaPage.xaml 的交互逻辑
    /// </summary>
    public partial class FormulaPage : INavigableView<FormulaViewModel>
    {
        public FormulaPage(FormulaViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        public FormulaViewModel ViewModel { get; }
    }
}
