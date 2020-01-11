using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        private Dictionary<string, string> _errorMsgDictionary = new Dictionary<string, string>();
        private AuthenticationService _service = new AuthenticationService();
        public static string _token = null;

        public LogInPage()
        {
            this.InitializeComponent();
        }

        private async void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Email.Text))
            {
                EmailErrorMsg.Text = "Please Insert Your Email Address!";
            }

            if (String.IsNullOrEmpty(Password.Password))
            {
                EmailErrorMsg.Text = "Please Insert Your Email Address!";
            }

            var email = Email.Text;
            var password = Password.Password;
            _token = await this._service.LoginTask(email, password);
            // Debug.WriteLine("Log in page "+ _token);
            if (_token != null)
            {
                await GetToken();
                this.Frame.Navigate(typeof(MyProfilePage));
            }
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            Email.Text = "";
            Password.Password = "";
            EmailErrorMsg.Text = "";
            PasswordErrorMsg.Text = "";
        }

        public static async Task<string> GetToken()
        {
            var result = await FileHandleService.ReadFile("token.txt");
            Debug.WriteLine("Token in file" + result);
            _token = result;
            return result;
        }
    }
}
