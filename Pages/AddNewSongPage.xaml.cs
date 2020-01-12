using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

            // _errorMsgDictionary = m.Validate();
            // if (_errorMsgDictionary.ContainsKey("InvalidName") &&
            //     !string.IsNullOrEmpty(_errorMsgDictionary["InvalidName"]))
            // {
            //     NameErrorMsg.Text = _errorMsgDictionary["InvalidName"];
            // }
            //
            // if (_errorMsgDictionary.ContainsKey("InvalidSinger") &&
            //     !string.IsNullOrEmpty(_errorMsgDictionary["InvalidSinger"]))
            // {
            //     SingerErrorMsg.Text = _errorMsgDictionary["InvalidSinger"];
            // }
            //
            // if (_errorMsgDictionary.ContainsKey("InvalidAuthor") &&
            //     !string.IsNullOrEmpty(_errorMsgDictionary["InvalidAuthor"]))
            // {
            //     AuthorErrorMsg.Text = _errorMsgDictionary["InvalidAuthor"];
            // }
            //
            // if (_errorMsgDictionary.ContainsKey("InvalidThumbnail") &&
            //     !string.IsNullOrEmpty(_errorMsgDictionary["InvalidThumbnail"]))
            // {
            //     ThumbnailErrorMsg.Text = _errorMsgDictionary["InvalidThumbnail"];
            // }
            //
            // if (_errorMsgDictionary.ContainsKey("InvalidLink") &&
            //     !string.IsNullOrEmpty(_errorMsgDictionary["InvalidLink"]))
            // {
            //     LinkErrorMsg.Text = _errorMsgDictionary["InvalidLink"];
            // }
            //
            // if (_errorMsgDictionary.Count == 0)
            // {
               var msgArr = await _service.RegisterSong(m);

               if (msgArr != null)
               {
                   if (msgArr.ContainsKey("success"))
                   {
                       ResponseMsg.Text = "Add song successfully!";
                   }

                   if (msgArr.ContainsKey("message"))
                   {
                       ResponseMsg.Text = (string) msgArr.Property("message");
                   }
                   if (msgArr.ContainsKey("error"))
                   {
                       ResponseMsg.Text = "Add song unsuccessfully!";
                   }
               // }
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
                LinkErrorMsg.Text = "Picked file: " + mp3Song.Name;
            }
            else
            {
                LinkErrorMsg.Text = "Operation cancelled.";
            }
            
            if (this.mp3Song == null)
            {
                return;
            }

            // GoPost();
            HttpUploadFile("https://2-dot-backup-server-002.appspot.com/upload-file-handle", "myFile", "multipart/form-data");
        }

        // public async void GoPost()
        // {
        //     string baseUrl = "https://2-dot-backup-server-002.appspot.com/upload-my-file-handle";
        //
        //     Dictionary<string, string> parameters = new Dictionary<string, string>();
        //
        //     parameters.Add("cloudName", "dcykikkmq");
        //     parameters.Add("apiKey", "469518162414619");
        //     parameters.Add("apiSecret", "hgaiLKeG5joLPgtG0HCBIPs-YXY");
        //
        //     HttpClient client = new HttpClient();
        //     client.DefaultRequestHeaders.Add("Authorization", "Basic " + LogInPage._token);
        //     // client.BaseAddress = new Uri(baseUrl);
        //     MultipartFormDataContent form = new MultipartFormDataContent();
        //     HttpContent content = new StringContent("myFile");
        //     HttpContent DictionaryItems = new FormUrlEncodedContent(parameters);
        //    
        //
        //     form.Add(DictionaryItems);
        //     // form.Add(content, "myFile");
        //     var stream = await mp3Song.OpenStreamForReadAsync();
        //     content = new StreamContent(stream);
        //     content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //     {
        //         Name = "myFile",
        //         FileName = mp3Song.Name
        //     };
        //     form.Add(content);
        //
        //     HttpResponseMessage response = null;
        //
        //     try
        //     {
        //         response = (client.PostAsync(baseUrl, form)).Result;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        //
        //     var k = response.Content.ReadAsStringAsync().Result;
        //     Debug.WriteLine("Result here " + k);
        // }
        private async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
        
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            
            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);
        
            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\nAuthorization: {3}\r\n\r\n", paramName, "path_file", contentType, LogInPage._token);
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
                string response = reader2.ReadToEnd();
                Debug.WriteLine("result " + response);
                Link.Text = (string) JObject.Parse(response)["secure_url"];
                // ImageControl.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                // FileContent.Text = imageUrl;
            }
            catch (Exception ex)
            {
                LinkErrorMsg.Text = "Error uploading file!";
                // Debug.WriteLine("Error uploading file", ex.StackTrace);
                // Debug.WriteLine("Error uploading file", ex.InnerException);
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