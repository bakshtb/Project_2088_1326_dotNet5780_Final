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
using System.Collections;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
        public GuestRequestWindow()
        {
            InitializeComponent();
            cmbArea.ItemsSource = EnumToString.ToFriendlyString();
        }

          private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
