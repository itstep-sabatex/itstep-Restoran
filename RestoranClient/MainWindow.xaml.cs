﻿using RestoranClient.Data;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MainWindowViewModel> Orders { get; set; }
        public Abonent[] Abonents { get; set; }
        public SourceItem[] SourceItems { get; set; }
        public FoodItem[] FoodItems { get; set; }

        public ObservableCollection<Detail> Details { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Details = new ObservableCollection<Detail>();
            //model = new MainWindowViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Клієнт офіціанта";
            //var frm = new Login();
            //var result = frm.ShowDialog();
            //if (result == false || result == null)
            //{
            //    this.Close();
            //    return;
            //}

            //Title = $"Офіціант {Config.WaiterName} : {DateTime.Now}";
            //DataContext = model;
            //using (var context = new RestoranDbContext())
            //{
            //    var r = context.Order.Where(w => w.waiter_id == Config.WaiterId)
            //        .Join(context.Abonent, ws => ws.abonent_id, ab => ab.id, (ws, ab) => new MainWindowViewModel { id = ws.id, time_order = ws.time_order.ToString("H:mm:ss"), abonent = ab.name,Bill=ws.bill }).ToArray();

            //    Orders = new ObservableCollection<MainWindowViewModel>(r);
            //    Abonents = context.Abonent.ToArray();
            //    SourceItems = context.Sources.ToArray();
            //    FoodItems = context.Items.ToArray();

            //}
            //dg.ItemsSource = Orders;

        }

        private void RefreshGrid()
        {
            using (var context = new RestoranDbContext())
            {
                var r = context.Order.Where(w => w.waiter_id == Config.WaiterId)
                    .Join(context.Abonent, ws => ws.abonent_id, ab => ab.id, (ws, ab) => new MainWindowViewModel { id = ws.id, time_order = ws.time_order.ToString("H:mm:ss"), abonent = ab.name, Bill = ws.bill }).ToArray();

                dg.ItemsSource = new ObservableCollection<MainWindowViewModel>(r);
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {

        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            //cbAbonent.ItemsSource = Abonents;
            //cbAbonent.DisplayMemberPath = "name";
            //cbAbonent.SelectedValuePath = "id";
            //cbAbonent.SelectedValue = Abonents[0]?.id;
            //cbSource.ItemsSource = SourceItems;
            //cbSource.DisplayMemberPath = "name";
            //cbSource.SelectedValuePath = "id";
            //cbSource.SelectedValue = SourceItems[0]?.id;
            //dpStart.Text = DateTime.Now.ToString("H.mm.ss");
            ////grid
            //cbitem.ItemsSource = FoodItems;
            //cbitem.DisplayMemberPath = "name";
            //cbitem.SelectedValuePath = "id";
            //Details.Clear();
            //dgOrder.ItemsSource = Details;


            mainForm.Visibility = Visibility.Collapsed;
            
            orderEdit.ShowOrderEdit();
        }


        private void orderEdit_OnCancelOrder()
        {
            mainForm.Visibility = Visibility.Visible;
            orderEdit.Visibility = Visibility.Collapsed;
        }

        private void orderEdit_OnSaveOrder(Order obj)
        {
            using (var context = new RestoranDbContext())
            {
                context.Order.Update(obj);
                context.SaveChanges();
            }
            RefreshGrid();
            mainForm.Visibility = Visibility.Visible;
            orderEdit.Visibility = Visibility.Collapsed;
        }

        private void Login_LoginResult(int? arg1, string arg2)
        {
            if (arg1 == null)
                Close();
            else
            {
                Config.WaiterId = arg1.Value;
                Config.WaiterName = arg2;
                Title = $"Офіціант {Config.WaiterName} : {DateTime.Now}";
                login.Visibility = Visibility.Hidden;
                mainForm.Visibility = Visibility.Visible;
            }

        }
    }

    public class MainWindowViewModel
    {
        public int id { get; set; }
        public string abonent { get; set; }
        public string time_order { get; set; }
        public decimal? Bill { get; set; }

    }


}