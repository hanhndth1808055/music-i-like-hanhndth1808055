using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using FinalProjectMusic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinalProjectMusic.Services
{
    public class AuthenticationService
    {
        private string LOGIN_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members/authentication";
        private string CONTENT_TYPE = "application/json";

        public async Task<string> LoginTask(string email, string password)
        {
            JObject loginInfo = new JObject();
            loginInfo["email"] = email;
            loginInfo["password"] = password;
            // Convert C# Object to JSON
            var loginJson = JsonConvert.SerializeObject(loginInfo);
            // Debug.WriteLine(memberJson);
            // Packaging and Tagging
            HttpContent contentToSend = new StringContent(loginJson, Encoding.UTF8, CONTENT_TYPE);

            // await need to be in an async function
            HttpClient httpClient = new HttpClient();

            // send it now
            var response = await httpClient.PostAsync(LOGIN_API_URL, contentToSend);

            // wait for a success/failure response, get response as string
            var stringContent = await response.Content.ReadAsStringAsync();

            // success -> get object of type Member
            // var returnMember = JsonConvert.DeserializeObject<Member>(stringContent);
            // Debug.WriteLine(JsonObject.Parse(stringContent)["token"]);
            // Debug.WriteLine(stringContent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var token = (string)JObject.Parse(stringContent)["token"];
                await FileHandleService.WriteToFile("token.txt", token);
                return token;
            }

            return null;
        }
    }
}
