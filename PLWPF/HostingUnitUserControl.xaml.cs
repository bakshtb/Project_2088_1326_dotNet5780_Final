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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostUserControl.xaml
    /// </summary>
    public partial class HostingUnitUserControl : UserControl
    {
        public HostingUnitUserControl(HostingUnit hostingUnit , bool isWriteHostdetails = false)
        {
            InitializeComponent();

            UserControlGird.DataContext = hostingUnit;

            txbName.Text = hostingUnit.HostingUnitName;

            tbxHostingUnit.Text = hostingUnit.ToString();

            calendar.Margin = new Thickness(161, 0, 0, 0);

            if (isWriteHostdetails)
            {
                tbxHost.Text =  hostingUnit.Owner.ToString();
                
                calendar.Margin = new Thickness(322, 0, 0, 0);
            }



            calendar.DisplayDateStart = DateTime.Now.AddMonths(-1);
            calendar.DisplayDateEnd = DateTime.Now.AddMonths(11);
            calendar.DisplayDate = DateTime.Now;

            foreach (DateTime d in hostingUnit.AllDates)
            {
                calendar.BlackoutDates.Add(new CalendarDateRange(d));
            }
        }
    }
}
