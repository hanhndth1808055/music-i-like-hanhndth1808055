using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProjectMusic.Models
{
    public class Song
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string singer { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string link { get; set; }
        public long memberId { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string status { get; set; }


        public Dictionary<string, string> Validate()
        {
            Dictionary<string, string> ErrorMsgDictionary = new Dictionary<string, string>();
            
            ErrorMsgDictionary.Clear();

            if (String.IsNullOrEmpty(name))
            {
                ErrorMsgDictionary["InvalidName"] = "Empty Song Name!";
            }

            if (String.IsNullOrEmpty(singer))
            {
                ErrorMsgDictionary["InvalidSinger"] = "Empty Singer Name!";
            }

            if (String.IsNullOrEmpty(author))
            {
                ErrorMsgDictionary["InvalidAuthor"] = "Empty Author Name!";
            }

            Regex thumbnailRegex = new Regex(@"^(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)$");

            if (!thumbnailRegex.IsMatch(thumbnail))
            {
                ErrorMsgDictionary["InvalidThumbnail"] = "Link Thumbnail Is Invalid!";
            }

            // Regex linkRegex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
            //
            // if (!linkRegex.IsMatch(link))
            // {
            //     ErrorMsgDictionary["InvalidLink"] = "Link Audio Is Invalid!";
            // }

            return ErrorMsgDictionary;
        }
    }
}