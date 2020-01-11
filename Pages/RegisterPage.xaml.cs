using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
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
using SQLitePCL;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private StorageFile photo;
        private int _selectedGender = 0;
        private string _birthday;
        private Dictionary<string, string> _errorMsgDictionary = new Dictionary<string, string>();
        private SQLiteMemberService _service;
        public RegisterPage()
        {
            this.InitializeComponent();
            this._service = new SQLiteMemberService();
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
            // var conn = new SQLiteConnection("sqlitepcldemo.db");
            // var sql = @"Create table if not exists Customers (Id integer primary key AUTOINCREMENT, Name varchar(140));";
            // var statement = conn.Prepare(sql);
            // statement.Step();
            
            this.Reset_ErrorMsg();
            Member member = new Member
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
            
            _errorMsgDictionary = member.Validate();
            if (_errorMsgDictionary.Count > 0)
            {
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
            
            if (_errorMsgDictionary.Count == 0)
            {
                this._service.Create(member);
                // // Convert C# Object to JSON
                // var memberJson = JsonConvert.SerializeObject(member);
                // Debug.WriteLine(memberJson);
                // // Packaging and Tagging
                // HttpContent contentToSend = new StringContent(memberJson, Encoding.UTF8, "application/json");
                // // Send
                // SubmitData(contentToSend);
            }
        }

        private async void SubmitData(HttpContent contentToSend)
        {
            // await need to be in an async function
            HttpClient httpClient = new HttpClient();

            // send it now
            var response = await httpClient.PostAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/members",
                contentToSend);

            // wait for a success/failure response, get response as string
            var stringContent = await response.Content.ReadAsStringAsync();

            // success -> get object of type Member
            var returnMember = JsonConvert.DeserializeObject<Member>(stringContent);
            // Debug.WriteLine(JsonObject.Parse(stringContent)["id"]);
            // Debug.WriteLine(returnMember.id);
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

        private async void ShowCamera(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var uploadUrl = client.GetAsync("https://2-dot-backup-server-003.appspot.com/get-upload-token").Result
                .Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Upload URL: " + uploadUrl);

            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(300, 300);
            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                return;
            }

            HttpUploadFile(uploadUrl, "myFile", "image/png");
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
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
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
                // Debug.WriteLine(imageUrl);
                ImageControl.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                Avatar.Text = imageUrl;
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