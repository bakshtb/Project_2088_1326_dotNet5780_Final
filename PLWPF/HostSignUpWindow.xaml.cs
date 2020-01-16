using System;
using System.Collections.Generic;
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
        public HostSignUpWindow()
        {
            InitializeComponent();

            host = new Host();

            this.HostSignUpGrid.DataContext = host;

            cmbBankBranch.ItemsSource = MainWindow.bl.getListBankBranchs();

            
        }

        private void btnSignUp1_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                if(cmbBankBranch.SelectedIndex >= 0)
                    host.BankBranchDetails = MainWindow.bl.getListBankBranchs()[cmbBankBranch.SelectedIndex];
                else 
                    throw new Exception("נא לבחור סניף בנק");

                MainWindow.bl.addHost(host);

                MessageBox.Show(host.ToString());

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
