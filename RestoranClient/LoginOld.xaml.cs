using RestoranClient.Data;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestoranClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginOld : UserControl
    {
        public event Action<int?,string> LoginResult;
        public LoginViewModel model { get; set; }
        public LoginOld()
        {
            InitializeComponent();
            model = new LoginViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var r = model.Waiters.SingleOrDefault(s => s.id == model.WaiterId);
            if (r?.password == model.Password)
            {
                LoginResult?.Invoke(r.id, r.name);
            }
            else
            {
                LoginResult?.Invoke(null, string.Empty);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginResult?.Invoke(null, string.Empty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = model;
            using (var context = new RestoranDbContext())
            {
                model.Waiters = context.Waiters.ToList();
            }
        }
    }

    public class LoginViewModel
    {
        public LoginViewModel()
        {
            Waiters = new List<Waiter>();
            WaiterId = 1;
        }
        public List<Waiter> Waiters { get; set; }
        public int WaiterId { get; set; }
        public string Password { get; set; }
    }
}
