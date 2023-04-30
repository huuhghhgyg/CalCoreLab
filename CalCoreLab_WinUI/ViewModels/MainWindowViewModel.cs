using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.ViewModels
{
    public partial class MainWindowViewModel:ObservableObject
    {
        [ObservableProperty]
        int count = 0;

        [ObservableProperty]
        ObservableCollection<NavigationViewItem> items = new ObservableCollection<NavigationViewItem>()
        {
            new NavigationViewItem()
            {
                Icon = new FontIcon(){Glyph = "\uE8EF" },
                Content = "Calculate",
                Tag = "Calculate",
            },
            new NavigationViewItem()
            {
                Icon = new FontIcon(){Glyph = "\uE71C" },
                Content = "DataProcess",
                Tag = "DataProcess",
            },
            new NavigationViewItem()
            {
                Icon = new FontIcon(){Glyph = "\uF003" },
                Content = "LinearProgramming",
                Tag = "LP",
            }
        };
    }
}
