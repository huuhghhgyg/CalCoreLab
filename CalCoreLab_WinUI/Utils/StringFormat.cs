using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalCoreLab_WinUI.Utils
{
    public static class StringFormat
    {
        /// <summary>
        /// 将表格粘贴的文本转换为矩阵格式文本
        /// </summary>
        /// <param name="tableString">表格文本</param>
        /// <returns>矩阵格式文本</returns>
        public static string Table2Matirx(string tableString)
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
        public static string Matrix2Table(string matrixString)
        {
            matrixString = Regex.Replace(matrixString, @"\s*(\[|\])\s*", ""); //删除多余空格
            matrixString = Regex.Replace(matrixString, @"\s*;\s*(\r|\n)?", "\n"); //换行符替换
            matrixString = Regex.Replace(matrixString, @"(\s*,\s*)|,", " "); //逗号替换为空格
            return matrixString.Replace(' ', '\t'); //替换间隔为tab
        }

    }
}
