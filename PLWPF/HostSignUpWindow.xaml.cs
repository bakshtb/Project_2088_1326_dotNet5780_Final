using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostSignUpWindow.xaml
    /// </summary>
    public partial class HostSignUpWindow : Window
    {
        Host host;
        public static BL.IBL bl;
        public HostSignUpWindow()
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();

            host = new Host();

            this.HostSignUpGrid.DataContext = host;

            cmbBank.ItemsSource = bl.getListBanks();
            

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
            lblErrorBankNumber.Visibility = Visibility.Hidden;
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
            if (!bl.IsValidEmail(txbMail.Text.ToString()))
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
            if (cmbBank.SelectedIndex < 0)
            {
                lblErrorBank.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbBankBranch.SelectedIndex < 0)
            {
                lblErrorBranch.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (bl.isNotDigit(txbBank.Text))
            {
                lblErrorBankNumber.Visibility = Visibility.Visible;
                isReturn = true;
            }

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

                bl.addHost(host);

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

        private void cmbBankBranch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (cmbBank.SelectedIndex < 0)
            {
                MessageBox.Show("נא לבחור סניף בנק", "שגיאה");
            }
            else
            {
                string input = ((TextBox)e.OriginalSource).Text;

                var list = bl.getListBankBranchs(((BankBranch)cmbBank.SelectedItem).BankNumber);

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

            var list = bl.getListBanks();
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
                cmbBankBranch.ItemsSource = bl.getListBankBranchs(((BankBranch)cmbBank.SelectedItem).BankNumber);
        }
    }
}