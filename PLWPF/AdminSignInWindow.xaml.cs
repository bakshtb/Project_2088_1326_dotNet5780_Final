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
    /// Interaction logic for AdminSignInWindow.xaml
    /// </summary>
    public partial class AdminSignInWindow : Window
    {
        public AdminSignInWindow()
        {
            InitializeComponent();
            psbPass.Focus();
        }

        private void AdminSignInButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            signIn();
        }

        private void psbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                signIn();
        }

        private void signIn()
        {
            if (psbPass.Password == MainWindow.bl.getAdminPass())
            {
                Window Admin = new AdminWindow();
                this.Hide();
                Admin.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("הסיסמה לא נכונה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                psbPass.Password = "";
            }
                
        }
    }
}
