using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FinalProjectMusic.Models;
using FinalProjectMusic.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinalProjectMusic.Services
{
    public class SongService
    {
        public JObject MsgArr = new JObject();
        public async Task<ObservableCollection<Song>> LoadSongs()
        {
            //Gọi shipper
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogInPage._token);
            // Gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/songs");
            // Đọc dữ liệu response từ người nhận.
            // Debug.WriteLine(token);
            var stringContent = await response.Content.ReadAsStringAsync();
            // Debug.WriteLine(JsonConvert.DeserializeObject<List<Song>>(stringContent));
            Debug.WriteLine(response.StatusCode == HttpStatusCode.OK);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // var stringContent = await response.Content.ReadAsStringAsync();
            //     Debug.WriteLine(JsonConvert.DeserializeObject<List<Song>>(stringContent));
            return JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringContent);
            }

            return null;
        }

        public async Task<ObservableCollection<Song>> LoadMySongs()
        {
            //Gọi shipper
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + LogInPage._token);
            // Gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/songs/get-mine");
            // Đọc dữ liệu response từ người nhận.
            // Debug.WriteLine(token);
            var stringContent = await response.Content.ReadAsStringAsync();
            // Debug.WriteLine(JsonConvert.DeserializeObject<List<Song>>(stringContent));
            Debug.WriteLine(response.StatusCode == HttpStatusCode.OK);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // var stringContent = await response.Content.ReadAsStringAsync();
                //     Debug.WriteLine(JsonConvert.DeserializeObject<List<Song>>(stringContent));
                return JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringContent);
            }

            return null;
        }

        public async Task<JObject> RegisterSong(Song song)
        {
            // Convert C# Object to JSON
            var SongJson = JsonConvert.SerializeObject(song);
            // Debug.WriteLine(SongJson);
            // Packaging and Tagging
            HttpContent contentToSend = new StringContent(SongJson, Encoding.UTF8, "application/json");

            // Send
            var uploadedSong = await SubmitData(contentToSend);

            return MsgArr;
        }

        private async Task<Song> SubmitData(HttpContent contentToSend)
        {
            string token = LogInPage._token;
            // await need to be in an async function
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + (string) token);
            // send it now
            var response = await httpClient.PostAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/songs",
                contentToSend);
            // wait for a success/failure response, get response as string
            var stringContent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.Created)
            {
                MsgArr["code"] = 200;
                MsgArr["success"] = "Uploaded song successfully!";
                // success -> get object of type Member
                return JsonConvert.DeserializeObject<Song>(stringContent);
                // Debug.WriteLine(JsonObject.Parse(stringContent)["id"]);
                // Debug.WriteLine(returnSong.id);
            }
            MsgArr = JObject.Parse(stringContent);
            return null;
        }
    }
}
