using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common.Interfaces;

namespace CalCoreLab.ViewModels
{
    public partial class DataProcessViewModel : ObservableObject, INavigationAware
    {
        string _originalMatrix = "";
        string _formattedMatrix = "";

        public string OriginalMatrix
        {
            get => _originalMatrix;
            set
            {
                SetProperty(ref _originalMatrix, value);
                SetProperty(ref _formattedMatrix, Table2Matirx(_originalMatrix), nameof(FormattedMatrix));
            }
        }

        public string FormattedMatrix
        {
            get => _formattedMatrix;
            set
            {
                SetProperty(ref _formattedMatrix, value);
                SetProperty(ref _originalMatrix, Matrix2Table(_formattedMatrix), nameof(OriginalMatrix));
            }
        }

        string Table2Matirx(string tableString)
        {
            tableString = tableString.Trim()
                .Replace('\r','\n')
                .Replace('\t',' ')
                .Replace("\n",";\n");
            return $"[{tableString}]";
        }

        string Matrix2Table(string matrixString)
        {
            return matrixString.TrimStart('[').TrimEnd(']')
                .Replace("\n","").Replace("\r","")
                .Replace(";","\n")
                .Replace(',', '\t');
        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo()
        {
            //throw new NotImplementedException();
        }
    }
}
