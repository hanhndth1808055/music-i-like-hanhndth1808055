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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewSongPage : Page
    {
        private Dictionary<string, string> _errorMsgDictionary = new Dictionary<string, string>();

        public AddNewSongPage()
        {
            this.InitializeComponent();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Reset_ErrorMsg();
            var m = new Song
            {
                name = Name.Text,
                singer = Singer.Text,
                author = Author.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text
            };

            _errorMsgDictionary = m.Validate();
            if (_errorMsgDictionary.ContainsKey("InvalidName") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidName"]))
            {
                NameErrorMsg.Text = _errorMsgDictionary["InvalidName"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidSinger") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidSinger"]))
            {
                SingerErrorMsg.Text = _errorMsgDictionary["InvalidSinger"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidAuthor") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidAuthor"]))
            {
                AuthorErrorMsg.Text = _errorMsgDictionary["InvalidAuthor"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidThumbnail") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidThumbnail"]))
            {
                ThumbnailErrorMsg.Text = _errorMsgDictionary["InvalidThumbnail"];
            }

            if (_errorMsgDictionary.ContainsKey("InvalidLink") &&
                !string.IsNullOrEmpty(_errorMsgDictionary["InvalidLink"]))
            {
                LinkErrorMsg.Text = _errorMsgDictionary["InvalidLink"];
            }
        }

        private void Reset_ErrorMsg()
        {
            if (NameErrorMsg != null)
            {
                NameErrorMsg.Text = "";
            }

            if (SingerErrorMsg != null)
            {
                SingerErrorMsg.Text = "";
            }

            if (AuthorErrorMsg != null)
            {
                AuthorErrorMsg.Text = "";
            }

            if (ThumbnailErrorMsg != null)
            {
                ThumbnailErrorMsg.Text = "";
            }

            if (LinkErrorMsg != null)
            {
                LinkErrorMsg.Text = "";
            }
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            this.Reset_ErrorMsg();
            Name.Text = "";
            Singer.Text = "";
            Author.Text = "";
            Thumbnail.Text = "";
            Link.Text = "";
        }
    }
}