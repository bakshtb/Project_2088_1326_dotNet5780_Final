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
using System.Net.Mail;
using System.ComponentModel;

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
        GuestRequest guestRequest;
        Order order;
        long Hostkey;

        List<Order> listOrders;
        List<Order> listOrdersThatNotAddressed;
        List<Order> listOrdersToUpdate;
        List<object> ReadableListOrders;

        List<HostingUnit> listHostingUnits;
        List<object> ReadableListHostingUnits;

        List<GuestRequest> listGuestRequest;
        List<GuestRequest> listGuestRequestThatCanHandled;
        List<object> ReadableListGuestRequest;
       
        List<string> UpdateOrderStatus =new List<string> { HebrewEnum.getListStrings<OrderStatusEnum>().ElementAt(2) , HebrewEnum.getListStrings<OrderStatusEnum>().ElementAt(3) };

        bool isReturn;

        string pass = "";

        BackgroundWorker worker;

        public HostSignInWindow(long key)
        {
            bl = BL.FactoryBL.GetBL();

            order = new Order();

            InitializeComponent();

            Hostkey = key;

            refreshHost();            

            cmbHostingUnitArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();
            cmbOrderUpdateStatus.ItemsSource = UpdateOrderStatus;

            refreshHostingUnits();

            refreshGuestRequests();

            refreshOrder();            

            refreshErrorLabels();

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerSupportsCancellation = true;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(guestRequest.MailAddress);

                mail.From = new MailAddress(host.MailAddress);

                mail.Subject = "הצעת נופש ממארח";

                mail.Body = "פירטי יחידת האירוח:\n" + hostingUnit.ToString() + "\n\nפירטי מארח:" + host.ToString() + "\nיש לאשר במייל חוזר למארח בכתובת: " + host.MailAddress + "\nהצעות שלא יענו תוך 30 יום יסגרו אוטומטית";

                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.Credentials = new System.Net.NetworkCredential(host.MailAddress, pass);

                smtp.EnableSsl = true;

                try
                {
                    smtp.Send(mail);
                    e.Result = true;
                    break;
                }
                catch (Exception)
                {

                }
                System.Threading.Thread.Sleep(2000);
                e.Result = false;
            }
        }

        void showEmailError()
        { 
            if (CheckAccess())
            {
                Window window = new MailProblemWindow();
                window.ShowDialog();
            }
            else
            {
                Action action = showEmailError;
                Dispatcher.BeginInvoke(action);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result == true)
            {
                bl.updateOrder(order.OrderKey, OrderStatusEnum.mail_has_been_sent);
                refreshHost();
                refreshHostingUnits();
                refreshOrder();
                lsvOrderStatus.SelectedIndex = -1;
                MessageBox.Show("מייל נשלח בהצלחה, ללקוח נותרו 30 יום כדי לאשר.", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            else
                showEmailError();//MessageBox.Show("לא ניתן לשלוח מייל.", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
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
            lblErrorBranch.Visibility = Visibility.Hidden;
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

            cmbBank.ItemsSource = bl.getListBanks();
            cmbBankBranch.ItemsSource = bl.getListBankBranchs(host.BankBranchDetails.BankNumber);

            int i = 0;
            foreach (var item in cmbBank.ItemsSource)
            {
                if (((BankBranch)item).BankNumber == host.BankBranchDetails.BankNumber)
                    cmbBank.SelectedIndex = i;
                i++;
            }

            i = 0;
            foreach (var item in cmbBankBranch.ItemsSource)
            {
                if (((BankBranch)item).BranchNumber == host.BankBranchDetails.BranchNumber)
                    cmbBankBranch.SelectedIndex = i;
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
            listOrdersThatNotAddressed = listOrders.Where(order => order.Status == OrderStatusEnum.Not_yet_addressed).ToList();
            listOrdersToUpdate = listOrders.Where(order => order.Status == OrderStatusEnum.mail_has_been_sent).ToList();

            lsvOrderGuerstRequest.ItemsSource = bl.GetReadableListOfGuestRequest(listGuestRequestThatCanHandled);

            ReadableListOrders = bl.GetReadableListOfOrder(listOrders).ToList();

            lsvOrderStatus.ItemsSource = bl.GetReadableListOfOrder(listOrdersToUpdate);           
            lsvOrders.ItemsSource = ReadableListOrders;
            lsvSendMail.ItemsSource = bl.GetReadableListOfOrder(listOrdersThatNotAddressed).ToList();                        
        }


        private void refreshGuestRequests()
        {
            listGuestRequest = bl.getListGuestRequest().ToList();
            listGuestRequestThatCanHandled = listGuestRequest.Where(req => req.Status == GuestReqStatusEnum.not_addressed && req.EntryDate >= DateTime.Now).ToList();
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
            if (!bl.IsValidEmail(txbMail.Text.ToString()))
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
            if (cmbBankBranch.SelectedIndex < 0)
                lblErrorBranch.Visibility = Visibility.Visible;


            if (isReturn)
                return;


            try
            {
                BankBranch bankBranch = new BankBranch()
                {
                    BankNumber = ((BankBranch)cmbBank.SelectedItem).BankNumber,
                    BankName = ((BankBranch)cmbBank.SelectedItem).BankName,
                    BranchAddress = ((BankBranch)cmbBankBranch.SelectedItem).BranchAddress,
                    BranchCity = ((BankBranch)cmbBankBranch.SelectedItem).BranchCity,
                    BranchNumber = ((BankBranch)cmbBankBranch.SelectedItem).BranchNumber
                };

                host.BankBranchDetails = bankBranch;

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
                    order.GuestRequestKey = listGuestRequestThatCanHandled[lsvOrderGuerstRequest.SelectedIndex].GuestRequestKey;
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
                    order = listOrdersToUpdate[lsvOrderStatus.SelectedIndex];
                    order.Status = (OrderStatusEnum)(cmbOrderUpdateStatus.SelectedIndex+2);
                    bl.updateOrder(order);
                    refreshHost();
                    refreshHostingUnits();
                    refreshOrder();
                    lsvOrderStatus.SelectedIndex = -1;
                    MessageBox.Show("עודכן בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }       
        

        private void lsvOrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
       
        private void cmbBankBranch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (cmbBank.SelectedIndex < 0)
            {
                MessageBox.Show("נא לבחור סניף בנק", "שגיאה");
            }
            else
            {
                string input = ((TextBox)e.OriginalSource).Text;

                var list = MainWindow.bl.getListBankBranchs(((BankBranch)cmbBank.SelectedItem).BankNumber);

                if (input != "")
                    cmbBankBranch.ItemsSource = from item in list
                                                where item.ToString().Contains(input)
                                                select item;
                else
                    cmbBankBranch.ItemsSource = list;
            }
        }

        private void cmbBankBranch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cmbBank.SelectedIndex < 0)
            {
                MessageBox.Show("נא לבחור סניף בנק", "שגיאה");
            }
            else
                cmbBankBranch.IsDropDownOpen = true;
        }

        private void cmbBank_GotFocus(object sender, RoutedEventArgs e)
        {
            cmbBank.IsDropDownOpen = true;
        }

        private void cmbBank_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string input = ((TextBox)e.OriginalSource).Text;

            var list = MainWindow.bl.getListBanks();
            if (input != "")
                cmbBank.ItemsSource = from item in list
                                      where item.ToString().Contains(input)
                                      select item;
            else
                cmbBank.ItemsSource = list;
        }

        private void cmbBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBank.SelectedItem != null)
                cmbBankBranch.ItemsSource = MainWindow.bl.getListBankBranchs(((BankBranch)cmbBank.SelectedItem).BankNumber);
        }

        private void btnOrderSendMail_Click(object sender, RoutedEventArgs e)
        {
            if (lsvSendMail.SelectedIndex < 0)
            {
                MessageBox.Show("נא לבחור יחידת אירוח", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
            else
            {
                order = listOrdersThatNotAddressed[lsvSendMail.SelectedIndex];                
                hostingUnit = bl.getHostingUnitByOrder(order);
                guestRequest = bl.getGuestReqByOrder(order);

                HostMailPassWindow passWin = new HostMailPassWindow();

                pass = passWin.Prompt();

                if (worker.IsBusy != true)
                    worker.RunWorkerAsync();                
            }
        }
    }
}
