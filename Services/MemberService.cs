using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinalProjectMusic.Models;
using Newtonsoft.Json;

namespace FinalProjectMusic.Services
{
    public class MemberService
    {
        private static readonly string MemberInformationApiUrl = "https://2-dot-backup-server-002.appspot.com/_api/v2/members/information";

        public async Task<Member> GetMemberInformation(string token)
        {
            //Gọi shipper
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            // Gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.GetAsync(MemberInformationApiUrl);
            // Đọc dữ liệu response từ người nhận.
            // Debug.WriteLine(token);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Member>(stringContent);
            }

            return null;
        }
    }
}
