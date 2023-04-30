using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.Models
{
    public partial class NormalizeItem : ObservableObject
    {
        public NormalizeItem()
        {
        }

        [ObservableProperty]
        ObservableCollection<string> dataPropertyCollection = new ObservableCollection<string>() { "极大型", "极小型", "中间型", "区间型" };

        /// <summary>
        /// 指标类型
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MiddleVisibility))]
        [NotifyPropertyChangedFor(nameof(RangeVisibility))]
        [NotifyPropertyChangedFor(nameof(DataPropertyIndex))]
        NormalizeEnums dataProperty = NormalizeEnums.Positive;

        public int DataPropertyIndex
        {
            get => (int)DataProperty;
            set
            {
                DataProperty = (NormalizeEnums)value;
            }
        }

        [ObservableProperty]
        /// <summary>
        /// 中间型指标值
        /// </summary>
        double middleValue;

        [ObservableProperty]
        /// <summary>
        /// 区间型指标上界
        /// </summary>
        double upperbound;

        [ObservableProperty]
        /// <summary>
        /// 区间型指标下界
        /// </summary>
        double lowerbound;

        public Visibility MiddleVisibility
        {
            get => DataProperty == NormalizeEnums.Middle ? Visibility.Visible : Visibility.Collapsed;
        }
        public Visibility RangeVisibility
        {
            get => DataProperty == NormalizeEnums.Range ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public enum NormalizeEnums
    {
        Positive = 0, //极大型
        Negative = 1, //极小型
        Middle = 2, //中间型
        Range = 3, //区间型
    }
}
