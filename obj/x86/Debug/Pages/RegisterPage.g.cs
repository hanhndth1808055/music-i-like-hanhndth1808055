﻿#pragma checksum "C:\Users\LaptopAZ.vn\source\repos\FinalProjectMusic\Pages\RegisterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2C6F59A6C804ED4527CD56E2C6138003"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProjectMusic.Pages
{
    partial class RegisterPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\RegisterPage.xaml line 106
                {
                    global::Windows.UI.Xaml.Controls.Button element2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element2).Click += this.Submit_OnClick;
                }
                break;
            case 3: // Pages\RegisterPage.xaml line 107
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Reset_OnClick;
                }
                break;
            case 4: // Pages\RegisterPage.xaml line 35
                {
                    this.FirstName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Pages\RegisterPage.xaml line 37
                {
                    this.FirstNameErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Pages\RegisterPage.xaml line 42
                {
                    this.LastName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // Pages\RegisterPage.xaml line 44
                {
                    this.LastNameErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // Pages\RegisterPage.xaml line 49
                {
                    this.Password = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 9: // Pages\RegisterPage.xaml line 51
                {
                    this.PasswordErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // Pages\RegisterPage.xaml line 56
                {
                    this.ConfirmPassword = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 11: // Pages\RegisterPage.xaml line 58
                {
                    this.ConfirmPasswordErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Pages\RegisterPage.xaml line 63
                {
                    this.Address = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // Pages\RegisterPage.xaml line 65
                {
                    this.AddressErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // Pages\RegisterPage.xaml line 70
                {
                    this.Phone = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 15: // Pages\RegisterPage.xaml line 72
                {
                    this.PhoneErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16: // Pages\RegisterPage.xaml line 77
                {
                    this.Avatar = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 17: // Pages\RegisterPage.xaml line 79
                {
                    this.AvatarErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 18: // Pages\RegisterPage.xaml line 94
                {
                    this.Email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 19: // Pages\RegisterPage.xaml line 96
                {
                    this.EmailErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 20: // Pages\RegisterPage.xaml line 101
                {
                    this.Birthday = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                    ((global::Windows.UI.Xaml.Controls.CalendarDatePicker)this.Birthday).DateChanged += this.Select_Birthday;
                }
                break;
            case 21: // Pages\RegisterPage.xaml line 103
                {
                    this.BirthdayErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 22: // Pages\RegisterPage.xaml line 85
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element22 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element22).Checked += this.ChooseGender;
                }
                break;
            case 23: // Pages\RegisterPage.xaml line 86
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element23 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element23).Checked += this.ChooseGender;
                }
                break;
            case 24: // Pages\RegisterPage.xaml line 87
                {
                    this.OtherGender = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.OtherGender).Checked += this.ChooseGender;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

