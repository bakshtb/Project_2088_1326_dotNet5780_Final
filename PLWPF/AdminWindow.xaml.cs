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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            lsvAdminGuestRequest.ItemsSource = MainWindow.bl.GetReadableListOfGuestRequest();

            lsvAdminHosts.ItemsSource = MainWindow.bl.GetReadableListOfHosts();

            int i = 0;
            foreach (var host in MainWindow.bl.getListHostingUnit())
            {
                HostingUnitUserControl h = new HostingUnitUserControl(host, true);
                AdminHostingUnitGrid.Children.Add(h);

                var rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                AdminHostingUnitGrid.RowDefinitions.Add(rowDefinition);

                Grid.SetRow(h, i++);
            }

            lsvAdminOrders.ItemsSource = MainWindow.bl.GetReadableListOfOrder();

            txbNumOfGuestRequests.Text = MainWindow.bl.getListGuestRequest().Count().ToString();
            txbNumOfHostes.Text = MainWindow.bl.getListHosts().Count().ToString();
            txbNumOfHostingUnits.Text = MainWindow.bl.getListHostingUnit().Count().ToString();
            txbNumOfOrders.Text = MainWindow.bl.getListOrders().Count().ToString();
            txbNumOfSuccessfulOrders.Text = MainWindow.bl.SumOfApprovedOrderOfAllHosts().ToString();
            txbSumMaildedGuestRequests.Text = MainWindow.bl.getSumMaildedOrders().ToString();
            txbNumOfProfit.Text = MainWindow.bl.getAdminProfit().ToString(); 
        }

        private void lsvAdminOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        //    HostingUnit hostingUnit = MainWindow.bl.GetHostingUnit(lsvAdminOrders.SelectedItem)
        //    GuestRequest guestRequest= MainWindow.bl.getGuestReqByOrder((Order)lsvAdminOrders.SelectedItem);

        //    MessageBox.Show("מידע על ההזמנה:" + "\nבקשת לקוח:\n\n" + guestRequest.ToString() + "מידע על יחידת האירוח:\n " + hostingUnit.ToString(), "מידע", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.RtlReading);

        }

        private void btnUpdatePass_Click(object sender, RoutedEventArgs e)
        {            
            if (psbAdminPass.Password == psbVerificationAdminPass.Password)
            {
                if (psbAdminPass.Password == "")
                {
                    MessageBox.Show("הסיסמה לא יכולה להיות ריקה", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                }
                else
                {
                    MainWindow.bl.setAdminPass(psbAdminPass.Password);
                    MessageBox.Show("הסיסמה עודכנה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                }                
            }
            else
                MessageBox.Show("הסיסמאות לא תואמות, אנא נסה שוב!", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
        }

        private void btnUpdateFee_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.bl.isNotDigit(txbUpdateFee.Text))
            {
                MessageBox.Show("נא להזין מספר!", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            else
                BE.Configuration.fee = int.Parse(txbUpdateFee.Text);
        }
    }
}