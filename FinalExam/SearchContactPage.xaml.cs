using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Models;
using FinalProjectMusic.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.FinalExam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchContactPage : Page
    {
        private SQLitePhoneContactService _service;
        public SearchContactPage()
        {
            this.InitializeComponent();
            this._service = new SQLitePhoneContactService();
        }

        private void SearchContact_Clicked(object sender, RoutedEventArgs e)
        {
            if (Name.Text != null)
            {
               PhoneContact contact = _service.Search(Name.Text);
               if (contact != null)
               {
                   PhoneNumber.Text = contact.PhoneNumber;
               }
               else
               {
                   PhoneNumber.Text = "Contact not found.";
               }
            }
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FinalPracticalExam));
        }
    }
}
