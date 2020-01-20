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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostSignInWindow.xaml
    /// </summary>
    public partial class HostSignInWindow : Window
    {
        Host host;
        HostingUnit hostingUnit;
        Order order;
        long Hostkey;

        IEnumerable<Order> listOrders;
        IEnumerable<HostingUnit> listHostingUnits;

        public HostSignInWindow(long key)
        {
            InitializeComponent();

            Hostkey = key;

            refreshHost();

            order = new Order();

            cmbHostingUnitArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();
            cmbOrderStatus.ItemsSource = HebrewEnum.getListStrings<OrderStatusEnum>();

            refreshHostingUnits();

            refreshOrder();
        }

        private void refreshHost()
        {
            host = MainWindow.bl.GetHost(Hostkey);

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

            cmbUpdateHostBranch.ItemsSource = MainWindow.bl.getListBankBranchs();

            int i = 0;
            foreach (var item in MainWindow.bl.getListBankBranchs())
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


            listHostingUnits = MainWindow.bl.GetHostingUnitsOfHost(Hostkey);

            cmbUpdateHostingUnit.ItemsSource = listHostingUnits;
            cmbDeleteHostingUnit.ItemsSource = listHostingUnits;


            HostingUnitListGrid.Children.Clear();
            HostingUnitListGrid.RowDefinitions.Clear();

            int i = 0;
            foreach (var host in listHostingUnits)
            {
                HostUserControl h = new HostUserControl(host);
                HostingUnitListGrid.Children.Add(h);

                var rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                HostingUnitListGrid.RowDefinitions.Add(rowDefinition);

                Grid.SetRow(h, i++);
            }

        }

        private void refreshOrder()
        {
            listOrders = MainWindow.bl.getOrdersOfHost(host);
            lbxOrderGuestRequest.ItemsSource = MainWindow.bl.getListGuestRequest();
            lbxOrderHostingUnit.ItemsSource = MainWindow.bl.GetHostingUnitsOfHost(Hostkey);
            cmbOrderUpdate.ItemsSource = listOrders;
            cmbOrderDelete.ItemsSource = listOrders;
            lsvOrders.ItemsSource = listOrders;
            cmbOrderDelete.ItemsSource = listOrders;
        }


        private void btnAddHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            hostingUnit.Owner = host;
            HostingUnitAddGrid.DataContext = hostingUnit;

            MainWindow.bl.addHostingUnit(hostingUnit);
            refreshHostingUnits();
        }

        private void cmbUpdateHostingUnit_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbUpdateHostingUnit.SelectedIndex != -1)
            {
                hostingUnit = (HostingUnit)cmbUpdateHostingUnit.SelectedItem;
                HostingUnitUpdateGrid.DataContext = hostingUnit;

                txbHostingUnitUpdateArea.Text = HebrewEnum.Area(hostingUnit.Area);
            }
        }

        private void btnUpdateHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.bl.updateHostingUnit(hostingUnit);
                refreshHostingUnits();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void btnDeleteHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hostingUnit = (HostingUnit)cmbDeleteHostingUnit.SelectedItem;

                MainWindow.bl.deleteHostingUnit(hostingUnit);
                refreshHostingUnits();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void btnHostUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                host.BankBranchDetails = (BankBranch)cmbUpdateHostBranch.SelectedItem;
                MainWindow.bl.updateHost(host);
                refreshHost();
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
                MainWindow.bl.deleteHost(host.HostKey);
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
                if (lbxOrderGuestRequest.SelectedIndex >= 0 && lbxOrderHostingUnit.SelectedIndex >= 0)
                {
                    order.GuestRequestKey = ((GuestRequest)lbxOrderGuestRequest.SelectedItem).GuestRequestKey;
                    order.HostingUnitKey = ((HostingUnit)lbxOrderHostingUnit.SelectedItem).HostingUnitKey;
                    

                    MainWindow.bl.addOrder(order);

                    refreshOrder();
                }

                else
                    throw new Exception("נא לבחור סניף בנק");

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
                order = MainWindow.bl.GetOrder(((Order)cmbOrderUpdate.SelectedItem).OrderKey);
                order.Status = (OrderStatusEnum)cmbOrderStatus.SelectedIndex;
                MainWindow.bl.updateOrder(order);
                refreshOrder();
                cmbOrderStatus.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }          
        }

        private void cmbOrderUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbOrderUpdate.SelectedIndex != -1)
            {
                cmbOrderStatus.SelectedItem = HebrewEnum.OrderStatus(((Order)cmbOrderUpdate.SelectedItem).Status);
            }
        }

        private void btnOrderDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.bl.deleteOrder(((Order)cmbOrderDelete.SelectedItem).OrderKey);
                refreshOrder();
                cmbOrderDelete.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }
    }
}
