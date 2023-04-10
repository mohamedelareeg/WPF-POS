using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Returned
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
        public void InsertReturned(string TableName, string Name, string Category, double Quantity, double Cost, double Price, string Type, string Time, string Datex, string Barcode ,int Billnumber, double Earned , string Details)
        {
            string insertString = "insert into " + TableName + "(Name ,Category ,Quantity ,Cost ,Price ,Type  , Time ,  Datex ,Barcode ,Billnumber , Earned  , Details) VALUES (@name , @category , @quantity , @cost , @price ,@type ,@time , @datex ,@barcode , @billnumber ,@earned  , @details)";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertString, conn))
                {
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@category", Category);
                    cmd.Parameters.AddWithValue("@quantity", Quantity);
                    cmd.Parameters.AddWithValue("@cost", Cost);
                    cmd.Parameters.AddWithValue("@price", Price);
                    cmd.Parameters.AddWithValue("@type", Type);
                    cmd.Parameters.AddWithValue("@time", Time);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.Parameters.AddWithValue("@billnumber", Billnumber);
                    cmd.Parameters.AddWithValue("@earned", Earned);
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
        public List<Models.Returned> ReadReturned_Range(string TableName, string FieldName, string FieldValue, string FieldValue2)
        {

            List<Models.Returned> goods = new List<Models.Returned>();
            string readString = "SELECT * FROM " + TableName + " WHERE " + FieldName + " BETWEEN @fieldvalue AND @fieldvalue2 ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@fieldvalue", FieldValue);
                    cmd.Parameters.AddWithValue("@fieldvalue2", FieldValue2);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Returned();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Time = reader["Time"].ToString();
                        goods_List.Billnumber = Convert.ToInt32(reader["Billnumber"]);

                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Returned> ReadReturned(string TableName, string FieldName, string FieldValue)
        {

            List<Models.Returned> goods = new List<Models.Returned>();
            string readString = "SELECT * FROM " + TableName + " Where " + FieldName + "=@fieldvalue ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@fieldvalue", FieldValue);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Returned();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Time = reader["Time"].ToString();
                        goods_List.Billnumber = Convert.ToInt32(reader["Billnumber"]);
                       
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Returned> ReadReturned(string TableName)
        {

            List<Models.Returned> goods = new List<Models.Returned>();
            string readString = "SELECT * FROM " + TableName + "";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Returned();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Time = reader["Time"].ToString();
                        goods_List.Billnumber = Convert.ToInt32(reader["Billnumber"]);

                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Returned> ReadReturned_Like(string TableName, string FieldName, string FieldValue)
        {

            List<Models.Returned> goods = new List<Models.Returned>();
            string readString = "SELECT * FROM " + TableName + " Where " + FieldName + " like @fieldvalue || '%' ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@fieldvalue", FieldValue);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Returned();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Time = reader["Time"].ToString();
                        goods_List.Billnumber = Convert.ToInt32(reader["Billnumber"]);
                      
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Returned> ReadAllReturnedQuantity(string TableName)
        {

            List<Models.Returned> goods = new List<Models.Returned>();
            string readString = "SELECT * FROM " + TableName + " ORDER BY Quantity DESC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Returned();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Time = reader["Time"].ToString();
                        goods_List.Billnumber = Convert.ToInt32(reader["Billnumber"]);
                      
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }

       
    }
}
