using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DEM
{
    /// <summary>
    /// Логика взаимодействия для Product_page.xaml
    /// </summary>
    public partial class Product_page : Page
    {
        List<PostTemplate> postTemplates = new List<PostTemplate>();
        public Product_page()
        {
            InitializeComponent();
            setContent();
            InitListBox();
            InitComboBox();
            //SetNewCost();
        }
        public void setContent()
        {
            UserNameBlock.Text = Current_user.fio;
            if (Current_user.role == "Admin")
            {
                DelProduct.Visibility = Visibility.Visible;
            }
            else
            {
                DelProduct.Visibility = Visibility.Hidden;
            }
            if (Current_user.role == "Admin" || Current_user.role == "Man")
            {
                AddProduct.Visibility = Visibility.Visible;
                SearchTitile.Visibility = Visibility.Visible;
                SearchBox.Visibility = Visibility.Visible;
                PostTitle.Visibility = Visibility.Visible;
                PostBox.Visibility = Visibility.Visible;
                Orders.Visibility = Visibility.Visible;
                postTemplates.Add(new PostTemplate { name = "Все поставщики", id = -1 });
                PostBox.ItemsSource = postTemplates;
                PostBox.SelectedIndex = 0;
            }
            else
            {
                AddProduct.Visibility = Visibility.Hidden;
                SearchTitile.Visibility = Visibility.Hidden;
                SearchBox.Visibility = Visibility.Hidden;
                PostTitle.Visibility = Visibility.Hidden;
                PostBox.Visibility = Visibility.Hidden;
                Orders.Visibility = Visibility.Hidden;
            }
        }

        public void InitComboBox()
        {
            using (DemContext db = new DemContext())
            {
                var suppliers = db.Suppliers.ToList();
                foreach (Supplier supplier in suppliers)
                {
                    postTemplates.Add(new PostTemplate{ name = supplier.Supplier1, id = supplier.Id });
                }
            }
        }

        public void InitListBox()
        {
            using (DemContext db = new DemContext())
            {
                var products = db.Products;
                var products_source = products.Include(p => p.Man).Include(p=>p.Category).Include(p=>p.Supplier).Include(p=>p.Unit).ToList();
                for (int i = 0; i < products_source.Count; i++)
                {
                    products_source[i].PathPhoto = Directory.GetCurrentDirectory().ToString() + $"\\Images\\{products_source[i].PathPhoto}";
                }
                ProductListBox.ItemsSource = products_source;
            }
        }

        //public void SetNewCost()
        //{
        //    Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        for (int i = 0; i < ProductListBox.Items.Count; i++)
        //        {
        //            ListBoxItem item = (ListBoxItem)ProductListBox.ItemContainerGenerator.ContainerFromIndex(i);
        //            Run Sale = (Run)item.FindName("Sale");
        //            int sale = Convert.ToInt32(Sale.Text);
        //            if (sale > 15)
        //            {

        //                Run OldCost = (Run)item.FindName("OldCost");
        //                OldCost.Foreground = Brushes.Red;
        //                OldCost.TextDecorations = TextDecorations.Strikethrough;
        //                Run Cost = (Run)item.FindName("NewCost");
        //                Cost.Text = (Convert.ToDouble(OldCost.Text) * (sale / 100)).ToString();

        //            }
        //        }
        //    }), DispatcherPriority.Render);
            
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PostBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            HeadWindow window = (HeadWindow)Window.GetWindow(this);
            Frame frame = (Frame)window.FindName("PageWiever");
            frame.Content = new Order_page();
        }
    }
}
