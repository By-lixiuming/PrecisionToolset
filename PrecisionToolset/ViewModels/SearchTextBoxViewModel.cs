using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrecisionToolset.ViewModels
{
    class SearchTextBoxViewModel : ObservableObject
    {
        private string _filePath = string.Empty;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public ICommand SelectFileCmd { get; set; } 

        public SearchTextBoxViewModel()
        {
            SelectFileCmd = new RelayCommand(SelectFileClick);
        }

        private void SelectFileClick()
        {
            
        }
    }
}
