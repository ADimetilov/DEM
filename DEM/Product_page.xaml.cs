using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.RightsManagement;
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
        int selectedIndex;
        public Product_page()
        {
            InitializeComponent();
            setContent();
            SetFilter();
            InitComboBox();
            //SetNewCost();
        }
        public void setContent()
        {
            UserNameBlock.Text = Current_user.fio;
            if (Current_user.role == "Admin")
            {
                AddProduct.Visibility = Visibility.Visible;
            }
            else
            {
                AddProduct.Visibility = Visibility.Hidden;
            }
            if (Current_user.role == "Admin" || Current_user.role == "Man")
            {
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product_window product = new Product_window(-1);
            product.ShowDialog();
            SetFilter();
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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilter();
        }

        private void PostBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        public void SetFilter()
        {
            if (SearchBox.Text != "")
            {
                using (DemContext db = new DemContext())
                {
                    var searchText = SearchBox.Text.Trim().ToLower();
                    var postFilterApplied = PostBox.SelectedIndex != 0;
                    int? selectedSupplierId = postFilterApplied
                        ? ((PostTemplate)PostBox.SelectedItem).id
                        : (int?)null;

                    var products = db.Products
            .Include(p => p.Man)
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.Unit)
            .Where(p =>
                (string.IsNullOrEmpty(searchText) ||
                 p.Name.ToLower().Contains(searchText) ||
                 p.Desc.ToLower().Contains(searchText) ||
                 p.Category.Category1.ToLower().Contains(searchText))
                &&
                (!postFilterApplied || p.SupplierId == selectedSupplierId)
            )
            .AsEnumerable();
                    if (UpSortChecker.IsChecked == true)
                    {
                        products = products.OrderBy(p => p.Score);
                    }
                    else if (DownSortChecker.IsChecked == true)
                    {
                        products = products.OrderByDescending(p => p.Score);
                    }
                    var products_list = products.ToList();
                    for (int i = 0; i < products_list.Count; i++)
                    {
                        products_list[i].PathPhoto = Directory.GetCurrentDirectory().ToString() + $"\\Images\\{products_list[i].PathPhoto}";
                        if (products_list[i].Sale > 0)
                        {
                            products_list[i].NewCost = Convert.ToDouble(products_list[i].Cost) - Convert.ToDouble(products_list[i].Cost) * (Convert.ToDouble(products_list[i].Sale) * 0.01);
                        }
                    }
                    ProductListBox.ItemsSource = products_list;
                }
                    
                }
            else
            {
                if (PostBox.SelectedIndex > 0)
                {
                    using (DemContext db = new DemContext())
                    {
                        var postFilterApplied = PostBox.SelectedIndex > 0;
                        int? selectedSupplierId = postFilterApplied
                            ? ((PostTemplate)PostBox.SelectedItem).id
                            : (int?)null;

                        var products = db.Products
                .Include(p => p.Man)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Unit)
                .Where(p =>
                    (p.SupplierId == selectedSupplierId)
                )
                .AsEnumerable();
                        if (UpSortChecker.IsChecked == true)
                        {
                            products = products.OrderBy(p => p.Score);
                        }
                        else if (DownSortChecker.IsChecked == true)
                        {
                            products = products.OrderByDescending(p => p.Score);
                        }
                        
                        var products_list = products.ToList();
                        for (int i = 0; i < products_list.Count; i++)
                        {
                            products_list[i].PathPhoto = Directory.GetCurrentDirectory().ToString() + $"\\Images\\{products_list[i].PathPhoto}";
                            if (products_list[i].Sale > 0)
                            {
                                products_list[i].NewCost = Convert.ToDouble(products_list[i].Cost) - Convert.ToDouble(products_list[i].Cost) * (Convert.ToDouble(products_list[i].Sale) * 0.01);
                            }
                        }
                        ProductListBox.ItemsSource = products_list;
                    }
                }
                else
                {
                    using (DemContext db = new DemContext())
                    {
                        var products = db.Products
                .Include(p => p.Man)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Unit)
                .AsEnumerable();
                        if (UpSortChecker.IsChecked == true)
                        {
                            products = products.OrderBy(p => p.Score);
                        }
                        else if (DownSortChecker.IsChecked == true)
                        {
                            products = products.OrderByDescending(p => p.Score);
                        }
                        var products_list = products.ToList();
                        for (int i = 0; i < products_list.Count; i++)
                        {
                            products_list[i].PathPhoto = Directory.GetCurrentDirectory().ToString() + $"\\Images\\{products_list[i].PathPhoto}";
                            if (products_list[i].Sale > 0)
                            {
                                products_list[i].NewCost = Convert.ToDouble(products_list[i].Cost) - Convert.ToDouble(products_list[i].Cost) * (Convert.ToDouble(products_list[i].Sale) * 0.01);
                            }
                        }
                        ProductListBox.ItemsSource = products_list;
                        
                    }
                }
            }
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex != -1)
            {
                using (DemContext db = new DemContext())
                {
                    db.Products.Remove((Product)ProductListBox.SelectedItem);
                    db.SaveChanges();
                    SetFilter();
                }
            }
        }

        private void ProductListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListBox.SelectedIndex != -1 && Current_user.role == "Admin")
            {
                Product product = (Product)ProductListBox.SelectedItem;
                Product_window product_change = new Product_window(product.Id);
                product_change.ShowDialog();
                SetFilter();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SetFilter();
        }

        private void UpSortChecker_Click(object sender, RoutedEventArgs e)
        {
            SetFilter();
        }

        private void DownSortChecker_Click(object sender, RoutedEventArgs e)
        {
            SetFilter();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            SetFilter();
        }
    }
}
