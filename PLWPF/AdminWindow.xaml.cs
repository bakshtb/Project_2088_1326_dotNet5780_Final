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
        public static BL.IBL bl;
        public AdminWindow()
        {
            bl = BL.FactoryBL.GetBL();

            InitializeComponent();            

            lsvAdminGuestRequest.ItemsSource = MainWindow.bl.GetReadableListOfGuestRequest();

            lsvAdminHosts.ItemsSource = MainWindow.bl.GetReadableListOfHosts();

            cmbGuestRequestArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();

            cmbHotingUnitArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();

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
                bl.setFee(int.Parse(txbUpdateFee.Text));
        }

        private void cmbGuestRequestArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            foreach (var items in bl.GetGuestRequestsAreaByGroups())
            {
                if (items.Key == (AreaEnum)cmbGuestRequestArea.SelectedIndex)
                {
                    lsvGuestRequestArea.ItemsSource = bl.GetReadableListOfGuestRequest(items);
                    return;
                }               
            }
            lsvGuestRequestArea.ItemsSource = null;
        }

        private void txbGuestRequestNumOfGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            int sum = 0;


            if (!int.TryParse(txbGuestRequestNumOfGuests.Text, out sum) && txbGuestRequestNumOfGuests.Text != "")
            {
                MessageBox.Show("קלט לא תקין", "שגיאה");
                txbGuestRequestNumOfGuests.Text = "";
                return;
            }

            foreach (var items in bl.GetGuestRequestsSumOfPeoplesByGroups())
            {
                if (items.Key == sum)
                {
                    lsvGuestRequestNumOfGuests.ItemsSource = bl.GetReadableListOfGuestRequest(items);
                    return;
                }
            }
            lsvGuestRequestNumOfGuests.ItemsSource = null;
        }

       
        private void txbHostsNumOfHostingUnits_TextChanged(object sender, TextChangedEventArgs e)
        {
            int sum = 0;


            if (!int.TryParse(txbHostsNumOfHostingUnits.Text, out sum) && txbHostsNumOfHostingUnits.Text != "")
            {
                MessageBox.Show("קלט לא תקין", "שגיאה");
                txbHostsNumOfHostingUnits.Text = "";
                return;
            }

            foreach (var items in bl.GetHostsNumOfUnitsByGroups())
            {
                if (items.Key == sum)
                {
                    lsvHostsNumOfHostingUnits.ItemsSource = bl.GetReadableListOfHosts(items);
                    return;
                }
            }
            lsvHostsNumOfHostingUnits.ItemsSource = null;
        }

        private void cmbHotingUnitArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var items in bl.GetHostingUnitsAreaByGroups())
            {
                if (items.Key == (AreaEnum)cmbHotingUnitArea.SelectedIndex)
                {
                    lsvHotingUnitArea.ItemsSource = bl.GetReadableListOfHostingUnits(items);
                    return;
                }
            }
            lsvHotingUnitArea.ItemsSource = null;
        }       
    }  
}