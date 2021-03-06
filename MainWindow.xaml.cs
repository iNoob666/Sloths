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

using Sloths.source.gui;

namespace Sloths
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string id = "";
        public MainWindow()
        {
            InitializeComponent();
            foreach(Button elem in Tools.Children)
            {
                elem.Click += active;
            }
        }


        private void active(object sender, RoutedEventArgs e)
        {
            foreach(Button elem in Tools.Children)
            {
                elem.Background = new SolidColorBrush(Colors.White);
            }
            Button butt = sender as Button;
            butt.Background = new SolidColorBrush(Colors.Red);
            id = butt.Name;
        }
    }
}
