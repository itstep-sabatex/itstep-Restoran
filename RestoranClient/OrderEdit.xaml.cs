using Microsoft.EntityFrameworkCore;
using RestoranClient.Data;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace RestoranClient
{
    /// <summary>
    /// Interaction logic for OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : UserControl
    {
        public OrderEdit()
        {
            InitializeComponent();
        }

        public event Action<Order> OnSaveOrder;
        public event Action OnCancelOrder;
        public Order Order { get; set; }


        public void ShowOrderEdit(Order order = null)
        {
            cbAbonent.ItemsSource = Config.Abonents;
            cbSource.ItemsSource = Config.SourceItems;
            
             
            using (var context = new RestoranDbContext())
            {
                if (order == null)
                {
                    Order = new Order
                    {
                        abonent_id = Config.Abonents[0]?.id,
                        waiter_id = Config.WaiterId,
                        source_id = Config.SourceItems[0]?.id,
                        time_order = DateTime.Now,
                        Details = new List<Detail>()
                    };
                }
                else
                {
                    Order = context.Order.Include("Details").SingleOrDefault(pk => pk.id == order.id);
                }
            }

            cbAbonent.SelectedValue = Order.abonent_id;
            cbSource.SelectedValue = Order.source_id;
            dpStart.Text = Order.time_order.ToString("H.mm.ss");
            dpEnd.Text = Order.end_order?.ToString("H.mm.ss") ?? "Активний";

            //grid
            cbitem.ItemsSource = Config.FoodItems;

            dgOrder.ItemsSource = new ObservableCollection<Detail>(Order.Details);
            Visibility = Visibility.Visible;
        }

        private void SaveOrder(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            OnSaveOrder?.Invoke(Order);
        }

        private void CancelOrder(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            OnCancelOrder?.Invoke();
        }


    }
}
