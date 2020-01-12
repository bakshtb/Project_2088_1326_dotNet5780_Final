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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGuestRequest_Click(object sender, RoutedEventArgs e)
        {
            Window GuestRequest = new GuestRequestWindow();
            this.Hide();
            GuestRequest.ShowDialog();
            this.Show();
        }

        private void btnHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            Window HostingUnit = new HostingUnitWindow();
            this.Hide();
            HostingUnit.ShowDialog();
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
