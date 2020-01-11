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
    public sealed partial class AddContactPage : Page
    {
        private SQLitePhoneContactService _service;
        public AddContactPage()
        {
            this.InitializeComponent();
            this._service = new SQLitePhoneContactService();
        }

        private void AddContact_Clicked(object sender, RoutedEventArgs e)
        {
            PhoneContact contact = new PhoneContact();
            contact.Name = Name.Text;
            contact.PhoneNumber = PhoneNumber.Text;
            _service.Create(contact);
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FinalPracticalExam));
        }
    }
}
