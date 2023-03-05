using CalCore;
using CalCore.Data;
using CalCoreLab.Models;
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
using Wpf.Ui.Common.Interfaces;

namespace CalCoreLab.ViewModels
{
    public partial class DataProcessViewModel : ObservableObject, INavigationAware
    {
        string _originalMatrix = "";
        string _formattedMatrix = "";

        /// <summary>
        /// 表格格式的文本
        /// </summary>
        public string OriginalMatrix
        {
            get => _originalMatrix;
            set
            {
                SetProperty(ref _originalMatrix, value);
                SetProperty(ref _formattedMatrix, Table2Matirx(_originalMatrix), nameof(FormattedMatrix));
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
                SetProperty(ref _formattedMatrix, value);
                SetProperty(ref _originalMatrix, Matrix2Table(_formattedMatrix), nameof(OriginalMatrix));
            }
        }

        [ObservableProperty]
        ObservableCollection<NormalizeItem> normalizeItems = new ObservableCollection<NormalizeItem>();

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
                value = Regex.Replace(value, @"(\n|\r|\s|\t)*;(\n|\r|\s|\t)*", ";\n");
                //value = value.Replace(";", ";\n");
                SetProperty(ref _normalizeMatrixString, value);

                // 导入矩阵
                try
                {
                    NormalizeMatrix = new Matrix(value);
                }
                catch (Exception ex)
                {
                    normalizeInfoString = $"矩阵初始化错误：{ex.Message}"; //显示错误信息
                }

                if (NormalizeMatrix == null) return; //如果没有生成新矩阵，则停止
                InitializeNormalizeMatrix();
            }
        }

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

        /// <summary>
        /// 将表格粘贴的文本转换为矩阵格式文本
        /// </summary>
        /// <param name="tableString">表格文本</param>
        /// <returns>矩阵格式文本</returns>
        string Table2Matirx(string tableString)
        {
            tableString = tableString.Trim();
            //.Replace('\r','\n')
            //.Replace('\t',' ')
            //.Replace("\n",";\n");

            tableString = Regex.Replace(tableString, @"(\s|\t)*(\r|\n)+(\s|\t)*", ";\n");
            tableString = Regex.Replace(tableString, @"\s+", "\t");
            return $"[{tableString.Replace('\t', ' ')}]";
        }

        /// <summary>
        /// 将矩阵格式的文本转换为表格格式的文本
        /// </summary>
        /// <param name="matrixString">矩阵格式的文本</param>
        /// <returns>表格文本</returns>
        string Matrix2Table(string matrixString)
        {
            matrixString = Regex.Replace(matrixString, @"\s*(\[|\])\s*", ""); //删除多余空格
            matrixString = Regex.Replace(matrixString, @"\s*;\s*(\r|\n)?", "\n"); //换行符替换
            matrixString = Regex.Replace(matrixString, @"(\s*,\s*)|,", " "); //逗号替换为空格
            return matrixString.Replace(' ', '\t'); //替换间隔为tab
        }

        [RelayCommand]
        public void NormalizeData()
        {
            if (NormalizeMatrix == null) return; //排除NormalizeMatrix为空的情况
            Matrix resultMatrix = new Matrix(NormalizeMatrix); //复制矩阵进行计算

            for (int i = 0; i < normalizeItems.Count; i++)
            {
                switch (normalizeItems[i].DataProperty)
                {
                    case NormalizeEnums.Negative:
                        Normalize.NormalizeFromMin(resultMatrix, i + 1);
                        break;
                    case NormalizeEnums.Middle:
                        Normalize.NormalizeFromVal(resultMatrix, normalizeItems[i].MiddleValue, i + 1);
                        break;
                    case NormalizeEnums.Range:
                        Normalize.NormalizeFromRange(resultMatrix, normalizeItems[i].Lowerbound, normalizeItems[i].Upperbound, i + 1);
                        break;
                }
            }

            NormalizeResult = resultMatrix.ValueString; //输出结果
        }
        #endregion

        #region 事件
        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
