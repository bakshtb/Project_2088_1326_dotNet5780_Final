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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostMailPassWindow.xaml
    /// </summary>
    public partial class HostMailPassWindow : Window
    {
        public HostMailPassWindow()
        {
            InitializeComponent();
        }

        public string Prompt()
        {
            HostMailPassWindow PassDialog = new HostMailPassWindow();
            PassDialog.ShowDialog();
                return PassDialog.psbHostMailPass.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void psbHostMailPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Close();
        }
    }
}