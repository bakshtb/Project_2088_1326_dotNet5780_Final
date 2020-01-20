using System;
using System.Windows;
using System.Windows.Controls;
using BE;
using System.Collections.Generic;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
        GuestRequest guestRequest;

        public GuestRequestWindow()
        {
            InitializeComponent();           

            guestRequest = new GuestRequest();
            this.GuestRequestGrid.DataContext = guestRequest;

            lsvArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();
            lsvChildrensAttractions.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            lsvGarden.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            lsvJacuzzi.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            lsvPool.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            lsvType.ItemsSource = HebrewEnum.getListStrings<GuestReqTypeEnum>();

            dpEntryDate.DisplayDateStart = DateTime.Now;
            dpEntryDate.DisplayDateEnd = DateTime.Now.AddMonths(11);
            dpEntryDate.DisplayDate = DateTime.Now;

            dpReleaseDate.DisplayDateStart = DateTime.Now;
            dpReleaseDate.DisplayDateEnd = DateTime.Now.AddMonths(11);
            dpReleaseDate.DisplayDate = DateTime.Now;
        }

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.bl.addGuestReq(guestRequest);
                MessageBox.Show("בקשתך נקלטה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.RtlReading);
                MessageBox.Show(guestRequest.ToString());
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }
    }
}
