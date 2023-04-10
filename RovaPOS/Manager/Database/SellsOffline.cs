using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class SellsOffline
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
        public void InsertSellsOffline(string TableName, string Name, string Category, double Quantity, double Cost, double Price, string Type, string Time, string Datex, string Barcode ,int Billnumber, double Earned ,string Returned , string Details)
        {
            string insertString = "insert into " + TableName + "(Name ,Category ,Quantity ,Cost ,Price ,Type  , Time ,  Datex ,Barcode ,Billnumber , Earned ,Returned , Details) VALUES (@name , @category , @quantity , @cost , @price ,@type ,@time , @datex ,@barcode , @billnumber ,@earned ,@returned , @details)";
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
                    cmd.Parameters.AddWithValue("@returned", Returned);
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
        public List<Models.SellsOffline> ReadSellsOffline_Range(string TableName, string FieldName, string FieldValue , string FieldValue2)
        {

            List<Models.SellsOffline> goods = new List<Models.SellsOffline>();
            string readString = "SELECT * FROM " + TableName + "  WHERE " + FieldName + " BETWEEN @fieldvalue AND @fieldvalue2 ";
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
                        var goods_List = new Models.SellsOffline();
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
                        goods_List.Returned = reader["Returned"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.SellsOffline> ReadSellsOffline(string TableName)
        {

            List<Models.SellsOffline> goods = new List<Models.SellsOffline>();
            string readString = "SELECT * FROM " + TableName + "";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                   
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.SellsOffline();
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
                        goods_List.Returned = reader["Returned"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.SellsOffline> ReadSellsOffline(string TableName, string FieldName, string FieldValue)
        {

            List<Models.SellsOffline> goods = new List<Models.SellsOffline>();
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
                        var goods_List = new Models.SellsOffline();
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
                        goods_List.Returned = reader["Returned"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.SellsOffline> ReadSellsOffline_Like(string TableName, string FieldName, string FieldValue)
        {

            List<Models.SellsOffline> goods = new List<Models.SellsOffline>();
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
                        var goods_List = new Models.SellsOffline();
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
                        goods_List.Returned = reader["Returned"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.SellsOffline> ReadAllSellsOfflineQuantity(string TableName)
        {

            List<Models.SellsOffline> goods = new List<Models.SellsOffline>();
            string readString = "SELECT * FROM " + TableName + " ORDER BY Quantity DESC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.SellsOffline();
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
                        goods_List.Returned = reader["Returned"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.SellsOffline> ReadPendingSell(string TableName)
        {

            List<Models.SellsOffline> goods = new List<Models.SellsOffline>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.SellsOffline();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Time = reader["Time"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Billnumber = Convert.ToInt32(reader["Billnumber"]);
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        goods_List.Returned = reader["Returned"].ToString();
                        goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);
                      
                    }
                    return goods;
                }
            }
        }

        public bool UpdateReturned(string TableName, string Barcode, string Billnumber, string Returned)
        {
            string updateString = "UPDATE " + TableName + " SET (Returned ) = (@returned) WHERE Barcode =@barcode AND Billnumber =@billnumber ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(updateString, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.Parameters.AddWithValue("@billnumber", Billnumber);
                    cmd.Parameters.AddWithValue("@returned", Returned);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }
        }
        public bool RemoveGoods(string TableName, string Barcode, string Billnumber)
        {
            string RemoveString = "DELETE FROM " + TableName + " WHERE Barcode =@barcode AND Billnumber =@billnumber";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(RemoveString, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.Parameters.AddWithValue("@billnumber", Billnumber);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }
        }
        public void UpdateReturnedQuantity(string TableName, string Barcode,double Q_S)
        {
            double Quantity;
            string Type;
            string readString = "SELECT * FROM " + TableName + " WHERE Barcode =@barcode";
            string updateString = "UPDATE " + TableName + " SET (Quantity ) = (@quantity) WHERE Barcode =@barcode ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmdR = new SQLiteCommand(readString, conn))
                {
                    cmdR.Parameters.AddWithValue("@barcode", Barcode);

                    IDataReader reader = cmdR.ExecuteReader();
                    while (reader.Read())
                    {
                        Quantity = Convert.ToDouble(reader["Quantity"]);
                        Type = reader["Type"].ToString();
                        if (Type.Length != 2)
                        {
                            if(Quantity <= 0)
                            {
                                using (SQLiteCommand cmdU = new SQLiteCommand(updateString, conn))
                                {
                                    cmdU.Parameters.AddWithValue("@quantity", Q_S);
                                    cmdU.Parameters.AddWithValue("@barcode", Barcode);
                                   
                                    cmdU.ExecuteNonQuery();
                                    cmdU.Dispose();
                                   
                                }
                            }
                            else if (Quantity > 0)
                            {
                                using (SQLiteCommand cmdU = new SQLiteCommand(updateString, conn))
                                {
                                    cmdU.Parameters.AddWithValue("@quantity", Quantity + Q_S);
                                    cmdU.Parameters.AddWithValue("@barcode", Barcode);
                                
                                    cmdU.ExecuteNonQuery();
                                    cmdU.Dispose();
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

    }
   
}
