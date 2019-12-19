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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        private Dictionary<string, string> _errorMsgDictionary = new Dictionary<string, string>();
        public LogInPage()
        {
            this.InitializeComponent();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                EmailErrorMsg.Text = "Please Insert Your Email Address!";
            }

            if (string.IsNullOrEmpty(Password.Password))
            {
                EmailErrorMsg.Text = "Please Insert Your Email Address!";
            }
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
