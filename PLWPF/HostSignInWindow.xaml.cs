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
        public HostSignInWindow(long key)
        {
            InitializeComponent();

            host = MainWindow.bl.GetHost(key);

            lbl.Content ="שלום "+ host.PrivateName + " " + host.FamilyName;

            IEnumerable<HostingUnit> listHostingUnits = MainWindow.bl.GetHostingUnitsOfHost(host.HostKey);
            int i = 0;
            foreach(var host in listHostingUnits)
            {
                HostUserControl h = new HostUserControl(host);
                HostGrid.Children.Add(h);

                var rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                HostGrid.RowDefinitions.Add(rowDefinition);

                Grid.SetRow(h, i++);
            }

            txbPrivetInfo.Text = host.ToString();

            txpBankInfo.Text = host.BankBranchDetails.ToString()
                        + "\nמספר חשבון בנק: " + host.BankAccountNumber
                        + "\nאישור גביה מחשבון הבנק: ";
            if (host.CollectionClearance)
                txpBankInfo.Text += "כן";
            else 
                txpBankInfo.Text += "לא";
        }
    }
}
