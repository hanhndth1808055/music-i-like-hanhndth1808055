using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyProfilePage : Page
    {
        private MemberService _service = new MemberService();
        public MyProfilePage()
        {
            this.InitializeComponent();
            this.GetMemberInformation();

        }

        public async void GetMemberInformation()
        {
            // Debug.WriteLine(LogInPage._token);
            var member = await this._service.GetMemberInformation(LogInPage._token);
            if (member != null)
            {
                FirstName.Text = member.firstName;
                LastName.Text = member.lastName;
                Phone.Text = member.phone;
                Address.Text = member.address;
                if (member.introduction != null)
                {
                    Introduction.Text = member.introduction;
                }
                Gender.Text = member.gender == 1 ? "Male" : (member.gender == 0 ? "Female" : "Other");
                Birthday.Text = typeof(member.birthday);
                Email.Text = member.email;
                Salt.Text = member.salt;
                CreatedAt.Text = member.createdAt;
            }
            Avatar.Source = new BitmapImage(new Uri(member.avatar, UriKind.Absolute)); ;
        }
    }
}
