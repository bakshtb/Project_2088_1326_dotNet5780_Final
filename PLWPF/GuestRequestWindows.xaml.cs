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


        public static BL.IBL bl;

        public GuestRequestWindow()
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();

            Refresh();

            guestRequest = new GuestRequest();
            this.GuestRequestGrid.DataContext = guestRequest;

            cmbArea.ItemsSource = HebrewEnum.getListStrings<AreaEnum>();
            cmbChildrensAttractions.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            cmbGarden.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            cmbJacuzzi.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            cmbPool.ItemsSource = HebrewEnum.getListStrings<optionsEnum>();
            cmbType.ItemsSource = HebrewEnum.getListStrings<GuestReqTypeEnum>();

            dpEntryDate.DisplayDateStart = DateTime.Now;
            dpEntryDate.DisplayDateEnd = DateTime.Now.AddMonths(11);
            dpEntryDate.DisplayDate = DateTime.Now;

            dpReleaseDate.DisplayDateStart = DateTime.Now;
            dpReleaseDate.DisplayDateEnd = DateTime.Now.AddMonths(11);
            dpReleaseDate.DisplayDate = DateTime.Now;
        }

        public void Refresh()
        {
            lblErrorAdults.Visibility = Visibility.Hidden;
            lblErrorArea.Visibility = Visibility.Hidden;
            lblErrorattractions.Visibility = Visibility.Hidden;
            lblErrorChildrens.Visibility = Visibility.Hidden;
            lblErrorFamilyName.Visibility = Visibility.Hidden;
            lblErrorGarden.Visibility = Visibility.Hidden;
            lblErrorMail.Visibility = Visibility.Hidden;
            lblErrorName.Visibility = Visibility.Hidden;
            lblErrorPool.Visibility = Visibility.Hidden;
            lblErrorType.Visibility = Visibility.Hidden;
            lblErrorJacuzzi.Visibility = Visibility.Hidden;
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
            if (bl.isNotDigit(txbAdults.Text))
            {
                lblErrorAdults.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (bl.isNotDigit(txbCildrens.Text))
            {
                lblErrorChildrens.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbArea.SelectedIndex < 0)
            {
                lblErrorArea.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbChildrensAttractions.SelectedIndex < 0)
            {
                lblErrorattractions.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbGarden.SelectedIndex < 0)
            {
                lblErrorGarden.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbJacuzzi.SelectedIndex < 0)
            {
                lblErrorJacuzzi.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbPool.SelectedIndex < 0)
            {
                lblErrorPool.Visibility = Visibility.Visible;
                isReturn = true;
            }
            if (cmbType.SelectedIndex < 0)
            {
                lblErrorType.Visibility = Visibility.Visible;
                isReturn = true;
            }

            if (isReturn)
                return;

            try
            {
                bl.addGuestReq(guestRequest);
                MessageBox.Show("בקשתך נקלטה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.RtlReading);                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading);
            }
        }
    }
}
