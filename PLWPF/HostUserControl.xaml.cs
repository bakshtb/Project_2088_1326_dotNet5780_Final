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
    public partial class HostUserControl : UserControl
    {
        public HostUserControl(HostingUnit hostingUnit)
        {
            InitializeComponent();

            UserControlGird.DataContext = hostingUnit;

            tbxHost.Text = hostingUnit.ToString();
            txbName.Text = hostingUnit.HostingUnitName;

            calendar.DisplayDateStart = DateTime.Now;
            calendar.DisplayDateEnd = DateTime.Now.AddMonths(11);
            calendar.DisplayDate = DateTime.Now;

            //for (int i = 0; i < 12; i++)
            //{
            //    for (int j = 0; j < 31; j++)
            //    {
            //        if (hostingUnit.Diary[i,j] == false)
            //        {
            //            calendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(DateTime.Now.Year , j+1 , i+1)));
            //        }


            //    }
            //}
            foreach (DateTime d in hostingUnit.AllDates)
            {
                calendar.BlackoutDates.Add(new CalendarDateRange(d));
            }
        }
    }
}
