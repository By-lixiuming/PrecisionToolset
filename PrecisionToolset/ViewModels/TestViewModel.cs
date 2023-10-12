using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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

        public TestViewModel() 
        { 
            Status = "Hello";
        }
    }
}
