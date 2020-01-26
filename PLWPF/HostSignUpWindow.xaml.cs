using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostSignUpWindow.xaml
    /// </summary>
    public partial class HostSignUpWindow : Window
    {
        Host host;
        int temp;
        public static BL.IBL bl;
        public HostSignUpWindow()
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();

            host = new Host();

            this.HostSignUpGrid.DataContext = host;

            cmbBankBranch.ItemsSource = MainWindow.bl.getListBankBranchs();

            Refresh();
        }

        public void Refresh()
        {            
            lblErrorFamilyName.Visibility = Visibility.Hidden;            
            lblErrorMail.Visibility = Visibility.Hidden;
            lblErrorName.Visibility = Visibility.Hidden;
            lblErrorBank.Visibility = Visibility.Hidden;
            lblErrorBranch.Visibility = Visibility.Hidden;
            lblErrorColectionClearens.Visibility = Visibility.Hidden;
            lblErrorID.Visibility = Visibility.Hidden;
            lblErrorPhone.Visibility = Visibility.Hidden;
        }


        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            Refresh();
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
            if (bl.isNotDigit(txbID.Text))
            {
                lblErrorID.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (bl.isNotDigit(txbPhone.Text))
            {
                lblErrorPhone.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (rbtnNo.IsChecked == false & rbtnYes.IsChecked == false)
            {
                lblErrorColectionClearens.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbBankBranch.SelectedIndex < 0)
            {
                lblErrorBranch.Visibility = Visibility.Visible;
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
                if (cmbBankBranch.SelectedIndex >= 0)
                    host.BankBranchDetails = MainWindow.bl.getListBankBranchs()[cmbBankBranch.SelectedIndex];
                else
                    throw new Exception("נא לבחור סניף בנק");

                MainWindow.bl.addHost(host);

                Window HostSignIn = new HostSignInWindow(host.HostKey);
                this.Hide();
                HostSignIn.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה");
            }
        }
    }
}
