using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Bills
    {

        Server server = new Server();

        //        CREATE TABLE `goods` (
        //	`ID`	INTEGER PRIMARY KEY AUTOINCREMENT,
        //	`Name`	TEXT,
        //	`Category`	TEXT,
        //	`Quantity`	REAL,
        //	`Cost`	REAL,
        //	`Price`	REAL,
        //	`Type`	TEXT,
        //	`Barcode`	TEXT,
        //	`Earned`	REAL,
        //	`Datex`	TEXT,
        //	`Datee`	TEXT,
        //	`Image`	BLOB
        //);
        //string billnumber, double billcost, string time, string datex, string ownername, string ownerid, string ownernumber, double paid, double remain, double earned, double tax, double discount
        public void InsertBills(string TableName,int ID, int Billnumber, double Billcost, string Time, string Datex, string Ownername, string Ownerid, string Ownernumber, double Paid, double Remain, double Earned, double Tax, double Discount, string Details)
        {
            string insertString = "insert into " + TableName + "(ID ,Billnumber ,Billcost ,Time ,Datex ,Ownername ,Ownerid  , Ownernumber ,  Paid ,Remain ,Earned , Tax ,Discount , Details) VALUES (@id ,@billnumber , @billcost , @time , @datex , @ownername ,@ownerid ,@pwnernumber , @paid ,@remain , @earned ,@tax ,@discount , @details)";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@billnumber", Billnumber);
                    cmd.Parameters.AddWithValue("@billcost", Billcost);
                    cmd.Parameters.AddWithValue("@time", Time);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@ownername", Ownername);
                    cmd.Parameters.AddWithValue("@ownerid", Ownerid);
                    cmd.Parameters.AddWithValue("@pwnernumber", Ownernumber);
                    cmd.Parameters.AddWithValue("@paid", Paid);
                    cmd.Parameters.AddWithValue("@remain", Remain);
                    cmd.Parameters.AddWithValue("@earned", Earned);
                    cmd.Parameters.AddWithValue("@tax", Tax);
                    cmd.Parameters.AddWithValue("@discount", Discount);
                    cmd.Parameters.AddWithValue("@details", Details);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
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
        public DataTable ReadAdapter(string TableName)
        {
            
            DataTable DT = new DataTable();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(readString, conn))
                {
                    sQLiteDataAdapter.Fill(DT);
                    return DT;
                }
                   
               
            }
        }
        public string ReadBillnumber(string TableName , string Fieldname)
        {
            string returned_value = "";
            string readString = "SELECT * FROM " + TableName + " ORDER BY " + Fieldname + " DESC LIMIT 1";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        returned_value = reader["billnumber"].ToString();
                       
                    }
                    return returned_value;
                }
            }
        }
        public List<Models.Bills> ReadBills(string TableName)
        {

            List<Models.Bills> bills = new List<Models.Bills>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //string billnumber, double billcost, string time, string datex, string ownername, string ownerid, string ownernumber, double paid, double remain, double earned, double tax, double discount
                        var goods_List = new Models.Bills();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Billnumber = Convert.ToInt32(reader["billnumber"]);
                        goods_List.Billcost = Convert.ToDouble(reader["billcost"]);
                        goods_List.Time = reader["time"].ToString();
                        goods_List.Datex = reader["datex"].ToString();
                        goods_List.Ownername = reader["ownername"].ToString();
                        goods_List.Ownerid = reader["ownerid"].ToString();
                        goods_List.Ownernumber = reader["ownernumber"].ToString();
                        goods_List.Paid = Convert.ToDouble(reader["paid"]);
                        goods_List.Remain = Convert.ToDouble(reader["remain"]);
                        goods_List.Earned = Convert.ToDouble(reader["earned"]);
                        goods_List.Tax = Convert.ToDouble(reader["tax"]);
                        goods_List.Discount = Convert.ToDouble(reader["discount"]);
                        goods_List.Details = reader["Details"].ToString();
                        bills.Add(goods_List);
                      
                    }
                    return bills;
                }
            }
        }
      
    }
}
