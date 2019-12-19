using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private int _selectedGender = 0;
        private string _birthday;
        private Dictionary<string, string> _errorMsgDictionary = new Dictionary<string, string>();

        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private void ChooseGender(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton selectedRadioButton)) return;

            switch (selectedRadioButton.Tag)
            {
                case "Male":
                    _selectedGender = 1;
                    break;
                case "Female":
                    _selectedGender = 0;
                    break;
                case "Other":
                    _selectedGender = 2;
                    break;
            }
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Reset_ErrorMsg();
            Member m = new Member
            {
                firstName = FirstName.Text,
                lastName = LastName.Text,
                password = Password.Password,
                address = Address.Text,
                phone = Phone.Text,
                avatar = Avatar.Text,
                gender = _selectedGender,
                email = Email.Text,
                birthday = _birthday
            };

            _errorMsgDictionary = m.Validate();
            if (_errorMsgDictionary.ContainsKey("InvalidFirstName") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidFirstName"]))
            {
                FirstNameErrorMsg.Text = _errorMsgDictionary["InvalidFirstName"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidLastName") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidLastName"]))
            {
                LastNameErrorMsg.Text = _errorMsgDictionary["InvalidLastName"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidPassword") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidPassword"]))
            {
                PasswordErrorMsg.Text = _errorMsgDictionary["InvalidPassword"];
            }

            if (Password.Password != ConfirmPassword.Password)
            {
                ConfirmPasswordErrorMsg.Text = "Password confirmation does not match!";
            }

            if (_errorMsgDictionary.ContainsKey("InvalidAddress") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidAddress"]))
            {
                AddressErrorMsg.Text = _errorMsgDictionary["InvalidAddress"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidPhone") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidPhone"]))
            {
                PhoneErrorMsg.Text = _errorMsgDictionary["InvalidPhone"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidEmail") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidEmail"]))
            {
                EmailErrorMsg.Text = _errorMsgDictionary["InvalidEmail"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidBirthday") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidBirthday"]))
            {
                BirthdayErrorMsg.Text = _errorMsgDictionary["InvalidBirthday"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidAvatar") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidAvatar"]))
            {
                AvatarErrorMsg.Text = _errorMsgDictionary["InvalidAvatar"];
            }
        }

        private void Reset_ErrorMsg()
        {
            if (FirstNameErrorMsg != null)
            {
                FirstNameErrorMsg.Text = "";
            }

            if (LastNameErrorMsg != null)
            {
                LastNameErrorMsg.Text = "";
            }

            if (PasswordErrorMsg != null)
            {
                PasswordErrorMsg.Text = "";
            }

            if (ConfirmPasswordErrorMsg != null)
            {
                ConfirmPasswordErrorMsg.Text = "";
            }

            if (AddressErrorMsg != null)
            {
                AddressErrorMsg.Text = "";
            }

            if (PhoneErrorMsg != null)
            {
                PhoneErrorMsg.Text = "";
            }

            if (AvatarErrorMsg != null)
            {
                AvatarErrorMsg.Text = "";
            }

            if (EmailErrorMsg != null)
            {
                EmailErrorMsg.Text = "";
            }

            if (BirthdayErrorMsg != null)
            {
                BirthdayErrorMsg.Text = "";
            }
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            this.Reset_ErrorMsg();
            FirstName.Text = "";
            LastName.Text = "";
            Password.Password = "";
            ConfirmPassword.Password = "";
            Address.Text = "";
            Phone.Text = "";
            Avatar.Text = "";
            OtherGender.IsChecked = true;
            Email.Text = "";
            Birthday.Date = null;
        }

        private void Select_Birthday(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date.HasValue)
            {
                _birthday = $"{sender.Date.Value.Date:yyyy-MM-dd}";
            }
        }
    }
}