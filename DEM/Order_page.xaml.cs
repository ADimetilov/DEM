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
            if (Current_user.role == "Admin")
            {
                AddOrderBut.Visibility = Visibility.Visible;
            }
            else
            {
                AddOrderBut.Visibility = Visibility.Hidden;
            }
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
            Order_window orderWindow = new Order_window(-1);
            orderWindow.ShowDialog();
            InitList();
        }

        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderList.SelectedIndex > -1 && Current_user.role=="Admin")
            {
                Order_window orderWindow = new Order_window(((Order)(OrderList.SelectedItem)).Id);
                orderWindow.ShowDialog();
                InitList();
            }
            
        }
    }
}
