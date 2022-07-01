using EmployeeServices.Api.Interfaces;
using EmployeeServices.Wpf.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeeServices.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly IDataService dataServiceProvider;
        public MainWindow(IDataService dataServiceProvider)
        {
            InitializeComponent();
            this.dataServiceProvider = dataServiceProvider;
            this.DataContext = new EmployeeViewModel(this.dataServiceProvider);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DataGridEmployees.SelectAllCells();
            this.DataGridEmployees.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, this.DataGridEmployees);
            this.DataGridEmployees.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            try
            {
                StreamWriter sw = new StreamWriter("export.csv");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("export.csv");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
