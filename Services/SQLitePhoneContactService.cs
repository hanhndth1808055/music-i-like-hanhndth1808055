using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectMusic.Models;

namespace FinalProjectMusic.Services
{
    public class SQLitePhoneContactService
    {
        private PhoneContactModel phoneContactModel;

        public SQLitePhoneContactService()
        {
            phoneContactModel = new PhoneContactModel();
        }

        public void Create(PhoneContact phoneContact)
        {
            phoneContactModel.Save(phoneContact);
        }

        public PhoneContact Search(string name)
        {
            return phoneContactModel.GetDetail(name);
        }

        public ObservableCollection<PhoneContact> ListContacts()
        {
            return phoneContactModel.GetList();
        }
    }
}
