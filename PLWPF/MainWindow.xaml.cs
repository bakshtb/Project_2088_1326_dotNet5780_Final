using System;
using System.Collections.Generic;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static BL.IBL bl;
        public MainWindow()
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();              
        }


        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            Window GuestRequest = new GuestRequestWindow();
            this.Hide();
            GuestRequest.ShowDialog();
            this.Show();
        }

        private void btnHost_Click(object sender, RoutedEventArgs e)
        {
            Window Host = new HostWindow();
            this.Hide();
            Host.ShowDialog();
            this.Show();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Window Admin = new AdminWindow();
            this.Hide();
            Admin.ShowDialog();
            this.Show();
        }
    }
}
