using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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

namespace DEM
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product_window : Window
    {
        int id;
        public FileInfo fileInfo;
        public string fileName = "None";
        public Product_window(int idProduct)
        {
            InitializeComponent();
            id = idProduct;
            using (DemContext db = new DemContext())
            {
                var supplier_list = db.Suppliers.ToList();
                var manufacter_list = db.Manufacters.ToList();
                var category_list = db.Categories.ToList();
                ManufacterBox.ItemsSource = manufacter_list;
                SupplierBox.ItemsSource = supplier_list;
                CategoryBox.ItemsSource = category_list;
            }
            if (idProduct != -1) setContentProduct();
        }

        public void setContentProduct()
        {
            using (DemContext db = new DemContext())
            {
                Product product = (Product)db.Products.Include(p=>p.Man).Where(p => p.Id == id).FirstOrDefault();
                NameBox.Text = product.Name;
                DescBox.Text = product.Desc;
                ManufacterBox.SelectedValue = db.Manufacters.Where(p => p.Id == product.ManId).ToList()[0].Id;
                CategoryBox.SelectedValue = db.Categories.Where(p => p.Id == product.CategoryId).ToList()[0].Id;
                SupplierBox.SelectedValue = db.Suppliers.Where(p => p.Id == product.SupplierId).ToList()[0].Id;
                ScoreBox.Text = product.Score.ToString();
                CostBox.Text = product.Cost.ToString();
                SaleBox.Text = product.Sale.ToString();
                PathImage.Content = product.PathPhoto;
                fileName = product.PathPhoto;
            }
        }

        private void ImageSendBut_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imageSelect = new OpenFileDialog();
            imageSelect.Filter = "Изображение jpg|*.jpg|Изображение png|*.png";
            if (imageSelect.ShowDialog() == true)
            {
                fileInfo = new FileInfo(imageSelect.FileName);
                fileName = System.IO.Path.GetFileName(imageSelect.FileName);
                PathImage.Content = fileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ScoreBox.Text,out int score)&&int.TryParse(CostBox.Text,out int cost)&&int.TryParse(SaleBox.Text,out int sale))
            {
                if (ManufacterBox.SelectedIndex != -1 && SupplierBox.SelectedIndex != -1 && CategoryBox.SelectedIndex !=-1)
                {
                    if (id != -1)
                    {
                        using (DemContext db = new DemContext())
                        {
                            Product product = db.Products.Where(p => p.Id == id).FirstOrDefault();
                            Manufacter selectedManufacter = (Manufacter)ManufacterBox.SelectedItem;
                            Category selectedCategory = (Category)CategoryBox.SelectedItem;
                            Supplier selectedSupplier = (Supplier)SupplierBox.SelectedItem;
                            product.Name = NameBox.Text;
                            product.Desc = DescBox.Text;
                            product.ManId = selectedManufacter.Id;
                            product.SupplierId = selectedSupplier.Id;
                            product.Score = score;
                            product.Cost = cost;
                            product.CategoryId = selectedCategory.Id;
                            product.Sale = sale;
                            product.PathPhoto = fileName;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Product product = new Product();
                        Manufacter selectedManufacter = (Manufacter)ManufacterBox.SelectedItem;
                        Category selectedCategory = (Category)CategoryBox.SelectedItem;
                        Supplier selectedSupplier = (Supplier)SupplierBox.SelectedItem;
                        product.Name = NameBox.Text;
                        product.Desc = DescBox.Text;
                        product.ManId = selectedManufacter.Id;
                        product.SupplierId = selectedSupplier.Id;
                        product.Score = score;
                        product.Cost = cost;
                        product.CategoryId = selectedCategory.Id;
                        product.Sale = sale;
                        product.PathPhoto = fileName;
                        using (DemContext db = new DemContext())
                        {
                            db.Products.Add(product);
                            db.SaveChanges();
                        }
                        fileInfo.CopyTo((Directory.GetCurrentDirectory().ToString() + $"\\Images\\{fileName}"), false);
                    }
                    MessageBox.Show("Успешно сохранено!");
                }
                else
                {
                    MessageBox.Show("Не выбраны значения из выпадающего списка");
                }
            }
            else
            {
                MessageBox.Show("Ошибка конвертации числовых полей");
            }
        }
    }
}
