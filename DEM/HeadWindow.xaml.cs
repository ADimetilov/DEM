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
using System.Windows.Shapes;

namespace DEM
{
    /// <summary>
    /// Логика взаимодействия для HeadWindow.xaml
    /// </summary>
    public partial class HeadWindow : Window
    {
        public HeadWindow()
        {
            InitializeComponent();
            PageWiever.Content = new Product_page();
        }
    }
}
