using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace FinalProjectMusic.Models
{
    public class PhoneContactModel
    {
        public bool Save(PhoneContact phoneContact)
        {
            var sqlConnection = SQLiteHelperF.CreateInstance().SQLiteConnection;
            var sqlCommandString = "insert into PhoneContacts (Name, PhoneNumber) values (?,?)";
            using (var stt = sqlConnection.Prepare(sqlCommandString))
            {
                stt.Bind(1, phoneContact.Name);
                stt.Bind(2, phoneContact.PhoneNumber);
                var result = stt.Step();
                return result == SQLiteResult.OK;
            }
        }

        public ObservableCollection<PhoneContact> GetList()
        {
            ObservableCollection<PhoneContact> list = new ObservableCollection<PhoneContact>();
            var sqlConnection = SQLiteHelperF.CreateInstance().SQLiteConnection;
            var sqlCommandString = "select * from PhoneContacts";
            using (var stt = sqlConnection.Prepare(sqlCommandString))
            {
                while (SQLiteResult.ROW == stt.Step())
                {
                    var id = stt[0].ToString();
                    var _name = (string)stt["Name"];
                    var _phoneNumber = (string)stt["PhoneNumber"];
                    var contact = new PhoneContact()
                    {
                        Id = Int32.Parse(id),
                        Name = _name,
                        PhoneNumber = _phoneNumber
                    };
                    list.Add(contact);
                }
            }
            return list;
        }

        public PhoneContact GetDetail(string name)
        {
            var sqlConnection = SQLiteHelperF.CreateInstance().SQLiteConnection;
            var sqlCommandString = "SELECT * FROM PhoneContacts WHERE Name LIKE '%"+name+"%'";
            using (var stt = sqlConnection.Prepare(sqlCommandString))
            {
                if (SQLiteResult.ROW == stt.Step())
                {
                    var id = stt[0].ToString();
                    var _name = (string)stt["Name"];
                    var _phoneNumber = (string)stt["PhoneNumber"];
                    var contact = new PhoneContact()
                    {
                        Id = Int32.Parse(id),
                        Name = _name,
                        PhoneNumber = _phoneNumber
                    };
                    return contact;
                }
                return null;
            }
        }

        public PhoneContact Update(PhoneContact phoneContact)
        {
            return new PhoneContact();
        }

        public bool Delete(int id)
        {
            return false;
        }
    }
}
