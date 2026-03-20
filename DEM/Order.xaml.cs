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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order_window : Window
    {
        int id;
        public Order_window(int idOrder)
        {
            InitializeComponent();
            id = idOrder;
            using (DemContext db = new DemContext())
            {
                StatusBox.ItemsSource = db.Statuses.ToList();
            }
            if (id > -1) setContent();
        }
        private void setContent()
        {
            using (DemContext db = new DemContext())
            {
                Order order = db.Orders.Where(p => p.Id == id).FirstOrDefault();
                ArtBox.Text = order.Art.ToString();
                AdresBox.Text = order.Adres;
                StatusBox.SelectedValue = order.StatusId;
                TimeOnly timeOnly = new TimeOnly(0, 0, 0);
                DateOnly dateStart = new DateOnly();
                dateStart = (DateOnly)order.DateStart;
                StartDate.SelectedDate = dateStart.ToDateTime(timeOnly);
                EndDate.SelectedDate = ((DateOnly)order.DateEnd).ToDateTime(timeOnly);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ArtBox.Text,out int articul))
            {
                if (StatusBox.SelectedIndex > -1)
                {
                    if (StartDate.SelectedDate.HasValue && EndDate.SelectedDate.HasValue)
                    {
                        if (!(StartDate.SelectedDate > EndDate.SelectedDate))
                        {
                            if (id > 0)
                            {
                                using (DemContext db = new DemContext())
                                {
                                    Order order = db.Orders.Where(p => p.Id == id).FirstOrDefault();
                                    order.Art = articul;
                                    order.StatusId = ((Status)StatusBox.SelectedItem).Id;
                                    order.Adres = AdresBox.Text;
                                    order.DateStart = DateOnly.FromDateTime(StartDate.SelectedDate.Value);
                                    order.DateEnd = DateOnly.FromDateTime(EndDate.SelectedDate.Value);
                                    db.SaveChanges();
                                    MessageBox.Show("Успешно сохранено!");
                                }
                            }
                            else
                            {
                                Order order = new Order();
                                order.Art = articul;
                                order.StatusId = ((Status)StatusBox.SelectedItem).Id;
                                order.Adres = AdresBox.Text;
                                order.DateStart = DateOnly.FromDateTime(StartDate.SelectedDate.Value);
                                order.DateEnd = DateOnly.FromDateTime(EndDate.SelectedDate.Value);
                                using (DemContext db = new DemContext())
                                {
                                    db.Orders.Add(order);
                                    db.SaveChanges();
                                    MessageBox.Show("Успешно добавлено");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Некорректный периоды даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите дату начала и конца", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Статус должен быть выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Артикул должен быть числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (DemContext db = new DemContext())
            {
                Order order = db.Orders.Where(p => p.Id == id).FirstOrDefault();
                db.Orders.Remove(order);
                db.SaveChanges();
                this.Close();
                MessageBox.Show("Успешно удалено");
            }
        }
    }
}
