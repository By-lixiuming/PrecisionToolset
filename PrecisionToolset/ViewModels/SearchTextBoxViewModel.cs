using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Vml;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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

        private string _searchText = string.Empty;

        /// <summary>
        /// 检索文本内容
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set 
            { 
                SetProperty(ref _searchText, value);
                SearchEnable = value != string.Empty ? true : false;
            }
        }

        private string _resultText = string.Empty;

        /// <summary>
        /// 检索结果输出
        /// </summary>
        public string ResultText
        {
            get => _resultText;
            set => SetProperty(ref _resultText, value);
        }

        private bool _searchEnable;

        /// <summary>
        /// 检索按钮的状态
        /// </summary>
        public bool SearchEnable
        {
            get => _searchEnable;
            set => SetProperty(ref _searchEnable, value);
        }


        public ICommand SearchFileCmd { get; set; } 
        public ICommand SearchTextBoxCmd {  get; set; }

        public SearchTextBoxViewModel()
        {
            SearchFileCmd = new RelayCommand(SearchFileClick);
            SearchTextBoxCmd = new RelayCommand(SearchTextBoxClick);
        }

        private void SearchTextBoxClick()
        {
            if (!File.Exists(FilePath))
            {
                Growl.Warning("文件路径不存在！");
                return;
            }
            else
            {
                IWorkbook workbook = null;
                string fileExt = Path.GetExtension(FilePath).ToLower();
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileExt == ".xlsx") 
                    {
                        workbook = new XSSFWorkbook(fs);
                    } 
                    else if (fileExt == ".xls")
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    if (workbook == null) 
                    {
                        Growl.Warning("Excel读取失败！");
                        return;
                    }

                    ResultText = "检索中。。。";

                    string result = "";
                    foreach (ISheet sheet in workbook)
                    {
                        XSSFDrawing? drawing = sheet.CreateDrawingPatriarch() as XSSFDrawing;
                        if (drawing != null)
                        {
                            foreach (XSSFShape shape in drawing.GetShapes())
                            {
                                if (shape is XSSFSimpleShape)
                                {
                                    XSSFSimpleShape simpleShape = (XSSFSimpleShape)shape;
                                    if (simpleShape.ShapeType == 5)
                                    {
                                        if (simpleShape.Text.Contains(SearchText))
                                        {
                                            XSSFClientAnchor anchor = (XSSFClientAnchor)shape.GetAnchor();
                                            CellReference startCellRef = new CellReference(anchor.Row1, anchor.Col1);
                                            string startCellReference = startCellRef.FormatAsString();
                                            string line = String.Format("表格: {0}, 单元格: {1}\n", sheet.SheetName, startCellReference);
                                            result += line;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    ResultText = result;
                }
            }
        }

        private void SearchFileClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            }
        }
    }
}
