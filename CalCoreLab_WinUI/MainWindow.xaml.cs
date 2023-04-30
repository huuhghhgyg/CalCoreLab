// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CalCoreLab_WinUI.ViewModels;
using CalCoreLab_WinUI.Views;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using static System.Net.WebRequestMethods;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CalCoreLab_WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(CalculatePage));
            ViewModel = new MainWindowViewModel();
        }

        MainWindowViewModel ViewModel { get; }

        public void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
            
            if (args.IsSettingsInvoked) //—°÷–¡ÀSettings
            {
                contentFrame.Navigate(typeof(SettingsPage));
                return;
            }

            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavigateByTag(navItemTag);
            }
        }

        void NavigateByTag(string tag)
        {
            if (tag == "Calculate") contentFrame.Navigate(typeof(CalculatePage));
            else if (tag == "DataProcess") contentFrame.Navigate(typeof(DataProcessPage));
            else if (tag == "LP") contentFrame.Navigate(typeof(LinearProgrammingPage));
        }
    }
}
