using PrecisionToolset.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrecisionToolset.Model
{
    public class Menu
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public ContentControl Control { get; set; }
    }

    public static class MenuList
    {
        public static ObservableCollection<Menu> GetList()
        {
            return new ObservableCollection<Menu>()
            {
                new Menu{ Title = "Excel 文本框检索", Image = "Images\\ExcelIcon.jpg", Control = new SearchTextBoxControl() },
                new Menu{ Title = "Excel 中文检查", Image = "Images\\ExcelIcon.jpg", Control = new CheckChineseControl() }

            };
        }
    }
}
