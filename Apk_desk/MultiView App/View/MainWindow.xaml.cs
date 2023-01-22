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

namespace MultiViewApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new View4();
        }

        private void dio_clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new View3();
        }

        private void kon_clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new View4();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new View1();
        }

        private void lis_clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new View5();
        }
    }
}
