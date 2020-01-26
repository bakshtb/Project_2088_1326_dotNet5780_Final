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
using System.Linq;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostSignInWindow.xaml
    /// </summary>
    public partial class HostSignInWindow : Window
    {
        public static BL.IBL bl;

        Host host;
        HostingUnit hostingUnit;
        Order order;
        long Hostkey;

        List<Order> listOrders;
        List<object> ReadableListOrders;

        List<HostingUnit> listHostingUnits;
        List<object> ReadableListHostingUnits;

        List<GuestRequest> listGuestRequest;
        List<object> ReadableListGuestRequest;

        bool isReturn;

        public HostSignInWindow(long key)
        {
            bl = BL.FactoryBL.GetBL();

            InitializeComponent();

            Hostkey = key;

            refreshHost();

            order = new Order();

            cmbHostingUnitArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();
            cmbOrderUpdateStatus.ItemsSource = HebrewEnum.getListStrings<OrderStatusEnum>();

            refreshHostingUnits();

            refreshOrder();

            refreshGuestRequests();

            refreshErrorLabels();
        }

        private void refreshErrorLabels()
        {
            lblErrorHostingUnitArea.Visibility = Visibility.Hidden;
            lblErrorHostingUnitName.Visibility = Visibility.Hidden;
            lblErrorUpdateHostingUnitName.Visibility = Visibility.Hidden;
            lblErrorUpdateHostingUnitArea.Visibility = Visibility.Hidden;
            lblErrorHostingUnitDelete.Visibility = Visibility.Hidden;
            lblErrorUpdateHostingUnitSelecta.Visibility = Visibility.Hidden;
            lblErrorName.Visibility = Visibility.Hidden;
            lblErrorFamilyName.Visibility = Visibility.Hidden;
            lblErrorMail.Visibility = Visibility.Hidden;
            lblErrorPhone.Visibility = Visibility.Hidden;
            lblErrorPhone.Visibility = Visibility.Hidden;
            lblErrorBank.Visibility = Visibility.Hidden;
        }


        private void refreshHost()
        {
            host = bl.GetHost(Hostkey);

            HostUpdateGrid.DataContext = host;

            lblHello.Content = "שלום " + host.PrivateName + " " + host.FamilyName;


            txbPrivetInfo.Text = host.ToString();
            txpBankInfo.Text = host.BankBranchDetails.ToString()
                        + "\nמספר חשבון בנק: " + host.BankAccountNumber
                        + "\nאישור גביה מחשבון הבנק: ";
            if (host.CollectionClearance)
                txpBankInfo.Text += "כן";
            else
                txpBankInfo.Text += "לא";

            cmbUpdateHostBranch.ItemsSource = bl.getListBankBranchs();

            int i = 0;
            foreach (var item in bl.getListBankBranchs())
            {
                if (item.BankNumber == host.BankBranchDetails.BankNumber && item.BranchNumber == host.BankBranchDetails.BranchNumber)
                    cmbUpdateHostBranch.SelectedIndex = i;
                i++;
            }
        }

        private void refreshHostingUnits()
        {
            hostingUnit = new HostingUnit();
            HostingUnitAddGrid.DataContext = hostingUnit;
            HostingUnitUpdateGrid.DataContext = hostingUnit;            


            listHostingUnits = bl.GetHostingUnitsOfHost(Hostkey).ToList();
            ReadableListHostingUnits = bl.GetReadableListOfHostingUnits(listHostingUnits);

            

            lsvUpdateHostingUnit.ItemsSource = ReadableListHostingUnits;
            lsvDeleteHostingUnit.ItemsSource = ReadableListHostingUnits;
            lsvOrderHostingUnit.ItemsSource = ReadableListHostingUnits;

            HostingUnitListGrid.Children.Clear();
            HostingUnitListGrid.RowDefinitions.Clear();

            int i = 0;
            foreach (var host in listHostingUnits)
            {
                HostingUnitUserControl h = new HostingUnitUserControl(host);
                HostingUnitListGrid.Children.Add(h);

                var rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                HostingUnitListGrid.RowDefinitions.Add(rowDefinition);

                Grid.SetRow(h, i++);
            }
        }

        private void refreshOrder()
        {
            listOrders = bl.getOrdersOfHost(host).ToList();

            lsvOrderGuerstRequest.ItemsSource = bl.GetReadableListOfGuestRequest();

            ReadableListOrders = bl.GetReadableListOfOrder(listOrders).ToList();

            lsvOrderStatus.ItemsSource = ReadableListOrders;
            lsvOrderDelete.ItemsSource = ReadableListOrders;
            lsvOrders.ItemsSource = ReadableListOrders;
        }


        private void refreshGuestRequests()
        {
            listGuestRequest = bl.getListGuestRequest().ToList();
            ReadableListGuestRequest = bl.GetReadableListOfGuestRequest().ToList();
        }

        private void btnAddHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            refreshErrorLabels();
            isReturn = false;
            if(txbHostingUnitName.Text == "")
            {
                lblErrorHostingUnitName.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbHostingUnitArea.SelectedIndex < 0)
            {
                lblErrorHostingUnitArea.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (isReturn)
                return;


            try
            {
                hostingUnit.Owner = host;
                HostingUnitAddGrid.DataContext = hostingUnit;
                bl.addHostingUnit(hostingUnit);
                refreshHostingUnits();
                MessageBox.Show("נוסף בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            
        }

        private void lsvUpdateHostingUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvUpdateHostingUnit.SelectedIndex != -1)
            {
                hostingUnit = listHostingUnits[lsvUpdateHostingUnit.SelectedIndex];
                HostingUnitUpdateGrid.DataContext = hostingUnit;

                cmbHostingUnitUpdateArea.Text = HebrewEnum.Area(hostingUnit.Area);
            }
        }

        private void btnUpdateHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            isReturn = false;
            refreshErrorLabels();

            if(txbHostingUnitUpdateName.Text == "")
            {
                isReturn = true;
                lblErrorUpdateHostingUnitName.Visibility = Visibility.Visible;
            }
            if (cmbHostingUnitUpdateArea.CaretIndex < 0)
            {
                isReturn = true;
                lblErrorUpdateHostingUnitArea.Visibility = Visibility.Visible;
            }
            if(lsvUpdateHostingUnit.SelectedIndex < 0)
            {
                isReturn = true;
                lblErrorUpdateHostingUnitSelecta.Visibility = Visibility.Visible;
            }

            if (isReturn)
                return;


            try
            {
                bl.updateHostingUnit(hostingUnit);
                refreshHostingUnits();
                MessageBox.Show("עודכן בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void btnDeleteHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            if(lsvDeleteHostingUnit.SelectedIndex < 0)
            {
                lblErrorHostingUnitDelete.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                hostingUnit = listHostingUnits[lsvDeleteHostingUnit.SelectedIndex];

                bl.deleteHostingUnit(hostingUnit);
                refreshHostingUnits();
                MessageBox.Show("נמחק בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void btnHostUpdate_Click(object sender, RoutedEventArgs e)
        {
            

            refreshErrorLabels();
            bool isReturn = false;
            if (txbName.Text == "" || bl.isHaveDigit(txbName.Text))
            {
                lblErrorName.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (txbFamilyName.Text == "" || bl.isHaveDigit(txbFamilyName.Text))
            {
                lblErrorFamilyName.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (!txbMail.Text.ToString().Contains("@"))
            {
                lblErrorMail.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (bl.isNotDigit(txbPhone.Text))
            {
                lblErrorPhone.Visibility = Visibility.Visible;
                isReturn = true;
            }           
            if (bl.isNotDigit(txbBank.Text))
            {
                lblErrorBank.Visibility = Visibility.Visible;
                isReturn = true;
            }
           
            if (isReturn)
                return;


            try
            {
                host.BankBranchDetails = (BankBranch)cmbUpdateHostBranch.SelectedItem;
                bl.updateHost(host);
                refreshHost();
                MessageBox.Show("עודכן בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteHost(host.HostKey);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (lsvOrderGuerstRequest.SelectedIndex >= 0 && lsvOrderHostingUnit.SelectedIndex >= 0)
                {
                    order.GuestRequestKey = listGuestRequest[lsvOrderGuerstRequest.SelectedIndex].GuestRequestKey;
                    order.HostingUnitKey = listHostingUnits[lsvOrderHostingUnit.SelectedIndex].HostingUnitKey;
                    

                    bl.addOrder(order);

                    refreshOrder();
                    MessageBox.Show("נוסף בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                }

                else
                    throw new Exception("נא לבחור יחידת אירוח ובקשת לקוח");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void btnOrderUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (lsvOrderStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("נא לבחור הזמנה לצורך העידכון!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                }
                else
                {
                    string pass = "";

                    order = listOrders[lsvOrderStatus.SelectedIndex];
                    order.Status = (OrderStatusEnum)cmbOrderUpdateStatus.SelectedIndex;

                    if (order.Status == OrderStatusEnum.mail_has_been_sent)
                    {
                        HostMailPassWindow passWin = new HostMailPassWindow();

                        pass = passWin.Prompt();
                    }

                    bl.updateOrder(order, pass);
                    refreshHost();
                    refreshHostingUnits();
                    refreshOrder();
                    lsvOrderStatus.SelectedIndex = -1;
                    MessageBox.Show("עודכן בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);

                }
            }

            catch (Exception ex)
            {
                if (ex.ToString().Contains("5.7.0 Authentication Required"))
                {
                    MailProblemWindow problemWin = new MailProblemWindow();
                    problemWin.ShowDialog();
                }
                else
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }          
        }

        private void lsvOrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvOrderStatus.SelectedIndex != -1)
            {
                cmbOrderUpdateStatus.SelectedItem = HebrewEnum.OrderStatus(listOrders[lsvOrderStatus.SelectedIndex].Status);
            }
        }

        private void btnOrderDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsvOrderDelete.SelectedIndex <0)
            {
                MessageBox.Show("נא לבחור יחידת אירוח", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            try
            {
                bl.deleteOrder(listOrders[lsvOrderDelete.SelectedIndex].OrderKey);
                refreshOrder();
                lsvOrderDelete.SelectedIndex = -1;
                MessageBox.Show("נמחק בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }
    }
}
