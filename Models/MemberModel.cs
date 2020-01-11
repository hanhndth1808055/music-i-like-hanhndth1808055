using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace FinalProjectMusic.Models
{
    public class MemberModel
    {
        public bool Save(Member member)
        {
            var sqlConnection = SQLiteHelper.CreateInstance().SQLiteConnection;
            var sqlCommandString = "insert into Members (Name, Username, Password) values (?,?,?)";
            using (var stt = sqlConnection.Prepare(sqlCommandString))
            {
                stt.Bind(1, member.lastName);
                stt.Bind(2, member.firstName);
                stt.Bind(3, "123");
                var result = stt.Step();
                return result == SQLiteResult.OK;
            }
        }

        public List<Member> GetList()
        {
            return new List<Member>();
        }

        public Member GetDetail(int id)
        {
            return new Member();
        }

        public Member Update(Member member)
        {
            return new Member();
        }

        public bool Delete(int id)
        {
            return false;
        }
    }
}
