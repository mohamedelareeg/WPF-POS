using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RovaPOS.Manager
{
    class Server
    {
        public static string Location = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string fileName = "rovaShop.db";
        public static string fullpath = Path.Combine(Location, fileName);
        public string connectionString = String.Format("Data Source = {0}", fullpath);
        public void CreateDatabase(string createTableCommand)
        {
            string createTable = createTableCommand;
            if(!DuplicateDatabase(fullpath))
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(createTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
        }
        public void CreateTable(string TableName)
        {
            string createTable = TableName;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(createTable, conn))
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        public bool DuplicateDatabase(string fullPath)
        {
            return File.Exists(fullPath);
        }

    }
}
