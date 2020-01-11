using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace FinalProjectMusic.Models
{
    public class SQLiteHelper
    {
        private static SQLiteHelper _instance;

        public SQLiteConnection SQLiteConnection { get; set; }

        private static string DatabaseName = "hellosqlite.db";

        public static SQLiteHelper CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new SQLiteHelper();
            }
            return _instance;
        }

        public SQLiteHelper()
        {
            SQLiteConnection = new SQLiteConnection(DatabaseName);
            CreateTables();
        }

        private void CreateTables()
        {
            CreateMemberTable();
        }

        private void CreateMemberTable()
        {
            var sql = @"Create table if not exists Members (Id long primary key AUTOINCREMENT, firstName varchar(200), lastName varchar(50), Password varchar(200))";
            using (var statement = SQLiteConnection.Prepare(sql))
            {
                statement.Step();
            }
        }
    }
}
