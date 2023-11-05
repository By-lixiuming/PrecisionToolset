using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using HandyControl.Data;
using PrecisionToolset.Model;
using PrecisionToolset.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Menu = PrecisionToolset.Model.Menu;

namespace PrecisionToolset.ViewModels
{
    public class WindowViewModel : ObservableObject
    {

        private bool _isShow = true;

        public bool IsShow
        {
            get => _isShow;
            set => SetProperty(ref _isShow, value);
        }

        /// <summary>
        /// 主画面内容组件
        /// </summary>
        private ContentControl _pageControl;

        public ContentControl PageControl
        {
            get => _pageControl;
            set => SetProperty(ref _pageControl, value);
        }

        private ObservableCollection<Menu> _menus;
        
        public ObservableCollection<Menu> Menus
        {
            get => _menus;
            set => SetProperty(ref _menus, value);
        }



        public ICommand MenuBtnCmd { get; set; }

        public ICommand SwitchItemCmd { get; set; }

        public ICommand MenuChangedCmd { get; set; }

        public WindowViewModel() {

            MenuBtnCmd = new RelayCommand<Button>(MenuBtnClick);
            SwitchItemCmd = new RelayCommand<FunctionEventArgs<object>>(SwitchItem);
            MenuChangedCmd = new RelayCommand<SelectionChangedEventArgs>(MenuChanged);

            PageControl = new SearchTextBoxControl();
            Menus = MenuList.GetList();

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

        private void MenuChanged(SelectionChangedEventArgs e)
        {
            PageControl = ((Menu)e.AddedItems[0]).Control;
            int a = 0;
        }
    }
}
