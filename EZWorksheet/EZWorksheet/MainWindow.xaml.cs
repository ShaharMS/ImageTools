using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EZWorksheet
{
    //VARIABLE | Title           | TYPE IS | TextBlock
    //VARIABLE | CreateWorksheet | TYPE IS | Button
    //VARIABLE | ScanWorksheet   | TYPE IS | Button



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateWorksheet.Content = (int.Parse(CreateWorksheet.Content.ToString()) + 1).ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ScanWorksheet.Content = (int.Parse(ScanWorksheet.Content.ToString()) + 1).ToString();
        }
    }
}
