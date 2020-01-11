using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Models;
using FinalProjectMusic.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewSongPage : Page
    {
        private Dictionary<string, string> _errorMsgDictionary = new Dictionary<string, string>();
        private SongService _service = new SongService();
        private FileHandleService _fileHandleService = new FileHandleService();
        private StorageFile mp3Song;
        public AddNewSongPage()
        {
            this.InitializeComponent();
        }

        private async void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (LogInPage._token == null)
            {
                return;
            }

            this.Reset_ErrorMsg();
            var m = new Song
            {
                name = SongName.Text,
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

            if (_errorMsgDictionary.Count == 0)
            {
               var msgArr = await _service.RegisterSong(m);

               if (msgArr != null)
               {
                   if (msgArr.ContainsKey("success"))
                   {
                       ResponseMsg.Text = JsonConvert.SerializeObject(msgArr);
                   }

                   if (msgArr.ContainsKey("message"))
                   {
                       ResponseMsg.Text = (string) msgArr.Property("message");
                   }
                   if (msgArr.ContainsKey("error"))
                   {
                       foreach (var _error in msgArr.Property("error"))
                       {
                           ResponseMsg.Text += _error.Value<string>();
                       }
                       ResponseMsg.Text = (string)msgArr.Property("message");
                   }
               }
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
            SongName.Text = "";
            Singer.Text = "";
            Author.Text = "";
            Thumbnail.Text = "";
            Link.Text = "";
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LogInPage._token == null)
            {
                this.Frame.Navigate(typeof(LogInPage));
            }
        }

        private async void UploadMP3(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".mp3");

            mp3Song = await openPicker.PickSingleFileAsync();
            if (mp3Song != null)
            {
                // Application now has read/write access to the picked file
                // FileContent.Text = "Picked file: " + mp3Song.Name;
            }
            else
            {
                // FileContent.Text = "Operation cancelled.";
            }
            
            if (this.mp3Song == null)
            {
                return;
            }

            HttpUploadFile("https://2-dot-backup-server-002.appspot.com/upload-my-file-handle", "myFile", "multipart/form-data");
        }

        private async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            
            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.mp3Song.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                string imageUrl = reader2.ReadToEnd();
                Debug.WriteLine("result " + imageUrl);
                // ImageControl.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                // FileContent.Text = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}