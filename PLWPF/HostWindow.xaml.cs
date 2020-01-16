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
        Host host;
        public HostWindow()
        {
            InitializeComponent();

            txbID.Focus();

            host = new Host();
            
        }
        

        private void signIn()
        {
            try
            {
                host.HostKey = int.Parse(txbID.Text);
                MainWindow.bl.GetHost(host.HostKey);
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
