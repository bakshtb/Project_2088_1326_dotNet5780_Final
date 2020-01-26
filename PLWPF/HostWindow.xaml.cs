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
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {
        public static BL.IBL bl;

        Host host;
        public HostWindow()
        {
            bl = BL.FactoryBL.GetBL();

            InitializeComponent();

            txbID.Focus();

            host = new Host();

            lblErrorID.Visibility = Visibility.Hidden;
        }
        

        private void signIn()
        {
            lblErrorID.Visibility = Visibility.Hidden;
            if (txbID.Text == "" || bl.isNotDigit(txbID.Text))
            {
                lblErrorID.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                host.HostKey = int.Parse(txbID.Text);
                bl.GetHost(host.HostKey);
                Window HostSignIn = new HostSignInWindow(host.HostKey);
                this.Hide();
                HostSignIn.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }

        private void SignInButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            signIn();
        }

        private void SignUpButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window HostSignUp = new HostSignUpWindow();
            this.Hide();
            HostSignUp.ShowDialog();
            this.Close();
        }

        private void txbID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                signIn();
        }
    }
}
