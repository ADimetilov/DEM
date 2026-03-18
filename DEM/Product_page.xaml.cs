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
