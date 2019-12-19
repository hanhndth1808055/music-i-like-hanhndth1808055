using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProjectMusic.Models
{
    class Member
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string avatar { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
        public string birthday { get; set; }

        public Dictionary<string, string> Validate()
        {
            Dictionary<string, string> ErrorMsgDictionary = new Dictionary<string, string>();

            ErrorMsgDictionary.Clear();

            if (String.IsNullOrEmpty(firstName))
            {
                ErrorMsgDictionary["InvalidFirstName"] = "Empty First Name!";
            }

            if (String.IsNullOrEmpty(lastName))
            {
                ErrorMsgDictionary["InvalidLastName"] = "Empty Last Name!";
            }

            if (String.IsNullOrEmpty(password) || password.Length <= 6 || password.Length > 20)
            {
                ErrorMsgDictionary["InvalidPassword"] = "Empty Password!";
            }

            if (String.IsNullOrEmpty(address))
            {
                ErrorMsgDictionary["InvalidAddress"] = "Empty Address!";
            }

            Regex phoneRegex =
                new Regex(
                    @"^(0|\+84)(\s|\.)?((3[2-9])|(5[689])|(7[06-9])|(8[1-689])|(9[0-46-9]))(\d)(\s|\.)?(\d{3})(\s|\.)?(\d{3})$");
            //class Regex Repesents an immutable regular expression.  

            if (!phoneRegex.IsMatch(phone))
            {
                ErrorMsgDictionary["InvalidPhone"] = "Phone Format Is Incorrect!";
            }

            Regex emailRegex = new Regex(@"^((?!\.)(?!.*?\.\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$");

            if (!emailRegex.IsMatch(email))
            {
                ErrorMsgDictionary["InvalidEmail"] = "Email Format Is Incorrect!";
            }

            Regex avatarRegex = new Regex(@"^(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)$");

            if (!avatarRegex.IsMatch(avatar))
            {
                ErrorMsgDictionary["InvalidAvatar"] = "Link Avatar Is Invalid!";
            }

            string[] formats = {"yyyy-MM-dd"};
            if (!DateTime.TryParseExact(birthday, formats,
                System.Globalization.CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _))
            {
                ErrorMsgDictionary["InvalidBirthday"] = "Birthday Format Is Incorrect!";
            }

            return ErrorMsgDictionary;
        }
    }
}