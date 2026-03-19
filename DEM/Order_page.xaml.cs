using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Order_page.xaml
    /// </summary>
    public partial class Order_page : Page
    {
        public Order_page()
        {
            InitializeComponent();
            InitList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HeadWindow window = (HeadWindow)Window.GetWindow(this);
            Frame frame = (Frame)window.FindName("PageWiever");
            frame.GoBack();
        }

        public void InitList()
        {
            using (DemContext db = new DemContext())
            {
                var orders = db.Orders.Include(p => p.Status).ToList();
                OrderList.ItemsSource = orders;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
