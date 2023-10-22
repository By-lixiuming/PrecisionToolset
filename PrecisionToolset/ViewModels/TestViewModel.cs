// 测试用的ViewModel类 对应的XAML文件是 Views/TestControl.xaml
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrecisionToolset.ViewModels
{
    public class TestViewModel : ObservableObject
    {
        private string _status = string.Empty;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public ICommand buttonCommand { get; }


        private double _gridWidth;

        public double GridWidth
        {
            get => _gridWidth;
            set => SetProperty(ref _gridWidth, value);
        }

        public RelayCommand IncreaseWidthCommand { get; private set; }


        private void IncreaseWidth()
        {
            // 当命令执行时，增加Grid的宽度
            GridWidth += 50;
        }

        public TestViewModel() 
        { 
            Status = "Hello";
            buttonCommand = new RelayCommand<object>(ButtonClick);
            IncreaseWidthCommand = new RelayCommand(IncreaseWidth);
        }

        private void ButtonClick(object b)
        {
            int a = 1;
        }
    }
}
