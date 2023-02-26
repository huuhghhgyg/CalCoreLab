using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CalCoreLab.Models
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
        [NotifyPropertyChangedFor(nameof(DataPropertyString))]
        [NotifyPropertyChangedFor(nameof(MiddleVisibility))]
        [NotifyPropertyChangedFor(nameof(RangeVisibility))]
        NormalizeEnums dataProperty = 0;
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
            get => dataProperty == NormalizeEnums.Middle ? Visibility.Visible : Visibility.Collapsed;
        }
        public Visibility RangeVisibility
        {
            get => dataProperty == NormalizeEnums.Range ? Visibility.Visible : Visibility.Collapsed;
        }

        public string DataPropertyString
        {
            get
            {
                switch (DataProperty)
                {
                    case NormalizeEnums.Positive: return "极大型";
                    case NormalizeEnums.Negative: return "极小型";
                    case NormalizeEnums.Middle: return "中间型";
                    case NormalizeEnums.Range: return "区间型";
                    default: return "未识别";
                }
            }
        }
    }
}
