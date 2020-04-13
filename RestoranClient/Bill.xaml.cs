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

namespace RestoranClient
{
    /// <summary>
    /// Логика взаимодействия для MakeBill.xaml
    /// </summary>
    public partial class Bill : UserControl
    {
        public event Action<int?, string> BillResult;
        public Bill()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BillResult.Invoke(null, String.Empty);
        }
    }
}
