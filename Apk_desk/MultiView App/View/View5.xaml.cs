using System;
using System.Collections.Generic;
using System.Text;
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
    using ViewModel;

    /// <summary>
    /// Logika interakcji dla klasy View5.xaml
    /// </summary>
    public partial class View5 : UserControl
    {
        public View5()
        {
            InitializeComponent();

            DataContext = new View5_ViewModel();
        }
    }
}
