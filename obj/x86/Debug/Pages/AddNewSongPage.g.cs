﻿#pragma checksum "C:\Users\LaptopAZ.vn\source\repos\FinalProjectMusic\Pages\AddNewSongPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9573345E7FE08D9899F9DB2B2DD34DDB"
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
    partial class AddNewSongPage : 
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
            case 2: // Pages\AddNewSongPage.xaml line 11
                {
                    global::Windows.UI.Xaml.Controls.StackPanel element2 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                    ((global::Windows.UI.Xaml.Controls.StackPanel)element2).Loaded += this.FrameworkElement_OnLoaded;
                }
                break;
            case 3: // Pages\AddNewSongPage.xaml line 70
                {
                    this.ResponseMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Pages\AddNewSongPage.xaml line 67
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Submit_OnClick;
                }
                break;
            case 5: // Pages\AddNewSongPage.xaml line 68
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.Reset_OnClick;
                }
                break;
            case 6: // Pages\AddNewSongPage.xaml line 30
                {
                    this.SongName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // Pages\AddNewSongPage.xaml line 32
                {
                    this.NameErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // Pages\AddNewSongPage.xaml line 37
                {
                    this.Singer = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // Pages\AddNewSongPage.xaml line 39
                {
                    this.SingerErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // Pages\AddNewSongPage.xaml line 44
                {
                    this.Author = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // Pages\AddNewSongPage.xaml line 46
                {
                    this.AuthorErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Pages\AddNewSongPage.xaml line 51
                {
                    this.Thumbnail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // Pages\AddNewSongPage.xaml line 53
                {
                    this.ThumbnailErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // Pages\AddNewSongPage.xaml line 64
                {
                    this.LinkErrorMsg = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15: // Pages\AddNewSongPage.xaml line 59
                {
                    global::Windows.UI.Xaml.Controls.Button element15 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element15).Click += this.UploadMP3;
                }
                break;
            case 16: // Pages\AddNewSongPage.xaml line 60
                {
                    this.Link = (global::Windows.UI.Xaml.Controls.TextBox)(target);
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

