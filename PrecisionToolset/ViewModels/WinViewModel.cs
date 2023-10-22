using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PrecisionToolset.ViewModels
{
    public class WinViewModel : ObservableObject
    {

        private bool _isShow = true;

        public bool IsShow
        {
            get => _isShow;
            set => SetProperty(ref _isShow, value);
        }


        public ICommand MenuBtnCmd { get; set; }

        public ICommand SwitchItemCmd { get; set; }

        public WinViewModel() {

            MenuBtnCmd = new RelayCommand<Button>(MenuBtnClick);
            SwitchItemCmd = new RelayCommand<FunctionEventArgs<object>>(SwitchItem);

        }

        private void MenuBtnClick(Button button)
        {
            if (IsShow)
            {
                Storyboard closeMenu = (Storyboard)button.FindResource("CloseMenu");
                closeMenu.Begin();
            } else
            {
                Storyboard openMenu = (Storyboard)button.FindResource("OpenMenu");
                openMenu.Begin();
            }
            IsShow = !IsShow;
        }

        private void SwitchItem(FunctionEventArgs<object> info)
        {
            Growl.Info((info.Info as SideMenuItem)?.Header.ToString());
        }

    }
}
