using CalCore;
using CalCore.Data;
using CalCoreLab_WinUI.Models;
using CalCoreLab_WinUI.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.ViewModels
{
    public partial class DataProcessViewModel : ObservableObject
    {
        string _originalMatrix = "";
        string _formattedMatrix = "";
        public bool IsOriginalGotFocus = false;

        /// <summary>
        /// 表格格式的文本
        /// </summary>
        public string OriginalMatrix
        {
            get => _originalMatrix;
            set
            {
                if (IsOriginalGotFocus)
                {
                    SetProperty(ref _originalMatrix, value);
                    SetProperty(ref _formattedMatrix, StringFormat.Table2Matirx(_originalMatrix), nameof(FormattedMatrix));
                }
            }
        }

        /// <summary>
        /// 矩阵格式的文本
        /// </summary>
        public string FormattedMatrix
        {
            get => _formattedMatrix;
            set
            {
                if (!IsOriginalGotFocus)
                {
                    SetProperty(ref _formattedMatrix, value);
                    SetProperty(ref _originalMatrix, StringFormat.Matrix2Table(_formattedMatrix), nameof(OriginalMatrix));
                }
            }
        }

        public ObservableCollection<NormalizeItem> NormalizeItems = new ObservableCollection<NormalizeItem>();

        [ObservableProperty]
        string normalizeInfoString = "无信息";

        string _normalizeMatrixString = string.Empty;

        Matrix? NormalizeMatrix;

        [ObservableProperty]
        string normalizeResult = string.Empty;

        /// <summary>
        /// 正向化矩阵文本
        /// </summary>
        public string NormalMatrixString
        {
            get => _normalizeMatrixString;
            set
            {
                if (value.IndexOf('[') > 0 && value.IndexOf("]") > 0)
                    value = Regex.Replace(value, @"(\n|\r|\s|\t)*;(\n|\r|\s|\t)*", ";\n");
                //value = value.Replace(";", ";\n");
                SetProperty(ref _normalizeMatrixString, value);

                if (string.IsNullOrEmpty(value))
                {
                    NormalizeInfoString = "无信息";
                    NormalizeItems.Clear();
                    return;
                }

                // 导入矩阵
                try
                {
                    NormalizeMatrix = new Matrix(value);
                }
                catch (Exception ex)
                {
                    NormalizeInfoString = $"矩阵初始化失败：{ex.Message}"; //显示错误信息
                    NormalizeItems.Clear();
                    return;
                }

                if (NormalizeMatrix == null) return; //如果没有生成新矩阵，则停止
                InitializeNormalizeMatrix();
            }
        }

        [ObservableProperty]
        string entropyWeight = string.Empty;

        #region 函数

        void InitializeNormalizeMatrix()
        {
            NormalizeInfoString = $"行：{NormalizeMatrix.Row},列：{NormalizeMatrix.Col}";
            NormalizeItems.Clear();
            for (int i = 0; i < NormalizeMatrix.Col; i++)
            {
                NormalizeItems.Add(new NormalizeItem());
            }
        }

        [RelayCommand]
        public void ProcessData()
        {
            if (NormalizeMatrix == null) return; //排除NormalizeMatrix为空的情况
            Matrix resultMatrix = new Matrix(NormalizeMatrix); //复制矩阵进行计算

            NormalizeData(resultMatrix);
        }

        [RelayCommand]
        void ClearInput()
        {
            NormalMatrixString = string.Empty;
        }

        void NormalizeData(Matrix mt)
        {
            for (int i = 0; i < NormalizeItems.Count; i++)
            {
                switch (NormalizeItems[i].DataProperty)
                {
                    case NormalizeEnums.Negative:
                        Normalize.NormalizeFromMin(mt, i + 1);
                        break;
                    case NormalizeEnums.Middle:
                        Normalize.NormalizeFromVal(mt, NormalizeItems[i].MiddleValue, i + 1);
                        break;
                    case NormalizeEnums.Range:
                        Normalize.NormalizeFromRange(mt, NormalizeItems[i].Lowerbound, NormalizeItems[i].Upperbound, i + 1);
                        break;
                }
            }

            NormalizeResult = mt.ValueString.Replace('\t', ' '); //输出结果
            CalculateEntropyWeight(mt); //输入正向化后的矩阵
        }

        void CalculateEntropyWeight(Matrix mt)
            => EntropyWeight = new Matrix(Evaluation.EntropyWeight(mt)).ValueString;
        #endregion
    }
}
