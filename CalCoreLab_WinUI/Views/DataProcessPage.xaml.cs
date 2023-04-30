// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CalCoreLab_WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CalCoreLab_WinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataProcessPage : Page
    {
        public DataProcessPage()
        {
            this.InitializeComponent();
            ViewModel = new();
        }
        DataProcessViewModel ViewModel { get; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => ViewModel.ProcessData();

        private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args) => ViewModel.ProcessData();

        //×¢²á½¹µãÊÂ¼þ
        private void OriginalMatrixText_GotFocus(object sender, RoutedEventArgs e) => ViewModel.IsOriginalGotFocus = true;
        private void FormattedMatrixText_GotFocus(object sender, RoutedEventArgs e) => ViewModel.IsOriginalGotFocus = false;
    }
}
