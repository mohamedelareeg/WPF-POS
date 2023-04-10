using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Info
    {
        Server server = new Server();
        public string ReadString(string TableName , string FieldValue , double ID)
        {
            string value;
            string readString = "SELECT " + FieldValue+ " FROM "+ TableName +" WHERE ID ="+ ID +" ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        value = reader[FieldValue].ToString();  
                        cmd.Dispose();
                        return value;
                    }
                    
                  
                }
            }
            return "";
            
        }
        //public DataTable ReadAdapter(string TableName)
        //{
            
        //    DataTable DT = new DataTable();
        //    string readString = "SELECT * FROM " + TableName + " ";
        //    using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
        //    {
        //        conn.Open();
        //        using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(readString, conn))
        //        {
        //            sQLiteDataAdapter.Fill(DT);
        //            return DT;
        //        }
                   
               
        //    }
        //}
        public List<Models.PendingSell> ReadAdapter(string TableName)
        {

            List<Models.PendingSell> DT = new List<Models.PendingSell>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(readString, conn))
                {
                    //sQLiteDataAdapter.Fill(DT);
                    return DT;
                }


            }
        }
    }
}
