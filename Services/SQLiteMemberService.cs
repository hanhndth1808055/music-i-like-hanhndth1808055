using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectMusic.Models;

namespace FinalProjectMusic.Services
{
    class SQLiteMemberService : IMemberService
    {
        private MemberModel memberModel;

        public SQLiteMemberService()
        {
            memberModel = new MemberModel();
        }

        public void Create(Member member)
        {
            memberModel.Save(member);
        }
    }
}
