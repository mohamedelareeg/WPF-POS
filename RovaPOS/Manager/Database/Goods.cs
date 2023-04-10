using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Goods
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
        public void InsertGoods(string TableName, string Name, string Category, double Quantity, double Cost, double Price, string Type, string Barcode, double Earned, string Datex, string Datee)
        {
            string insertString = "insert into " + TableName + "(Name ,Category ,Quantity ,Cost ,Price ,Type ,Barcode , Earned , Datex , Datee) VALUES (@name , @category , @quantity , @cost , @price ,@type ,@barcode ,@earned ,@datex , @datee)";
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
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.Parameters.AddWithValue("@earned", Earned);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@datee", Datee);
                   
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
        public List<Models.Goods> ReadGoodsPic(string TableName)
        {

            List<Models.Goods> goods = new List<Models.Goods>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Goods();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                      
                        goods.Add(goods_List);
                      
                    }
                    return goods;
                }
            }
        }
        public List<Models.Goods> ReadGoodsPic(string TableName , string Category)
        {

            List<Models.Goods> goods = new List<Models.Goods>();
            string readString = "SELECT * FROM " + TableName + " Where Category=@category ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@category", Category);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Goods();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        //goods_List.Type = reader["Type"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Goods> ReadGoodsPic(string TableName, string FieldName , string FieldValue)
        {

            List<Models.Goods> goods = new List<Models.Goods>();
            string readString = "SELECT * FROM " + TableName + " Where "+ FieldName + "=@fieldvalue ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@fieldvalue", FieldValue);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Goods();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        //goods_List.Type = reader["Type"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Goods> ReadGoodsPic_Like(string TableName, string FieldName, string FieldValue)
        {

            List<Models.Goods> goods = new List<Models.Goods>();
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
                        var goods_List = new Models.Goods();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        //goods_List.Type = reader["Type"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Goods> ReadAllGoodsQuantity(string TableName)
        {

            List<Models.Goods> goods = new List<Models.Goods>();
            string readString = "SELECT * FROM " + TableName + " ORDER BY Quantity ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Goods();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Goods> ReadAllGoodsPic(string TableName)
        {

            List<Models.Goods> goods = new List<Models.Goods>();
            string readString = "SELECT * FROM " + TableName + " ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                   
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Goods();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.GoodsR> ReadGoodsRPic(string TableName, string Category)
        {

            List<Models.GoodsR> goods = new List<Models.GoodsR>();
            string readString = "SELECT * FROM " + TableName + " Where Category=@category ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@category", Category);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.GoodsR();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        //goods_List.Type = reader["Type"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}

                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.GoodsR> ReadAllGoodsRPic(string TableName)
        {

            List<Models.GoodsR> goods = new List<Models.GoodsR>();
            string readString = "SELECT * FROM " + TableName + " ORDER BY Name ASC";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.GoodsR();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Category = reader["Category"].ToString();
                        goods_List.Quantity = Convert.ToDouble(reader["Quantity"]);
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Price = Convert.ToDouble(reader["Price"]);
                        goods_List.Type = reader["Type"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Datee = reader["Datee"].ToString();
                        goods_List.Barcode = reader["Barcode"].ToString();
                        goods_List.Earned = Convert.ToDouble(reader["Earned"]);
                        //goods_List.Details = reader["Details"].ToString();
                        //if (!Convert.IsDBNull(reader["Image"]))
                        //{
                        //    goods_List.Image = (byte[])(reader["Image"]); //TODO
                        //}
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public double ReadGoodsQuantity(string TableName ,string Barcode)
        {

            double _quantity = 0;
            string readString = "SELECT * FROM " + TableName + " WHERE Barcode =@barcode";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _quantity = Convert.ToDouble(reader["Quantity"]);
                    }
                    return _quantity;
                }
            }
        }
        //Name ,Category ,Quantity ,Cost ,Price ,Type , Image
        public bool UpdateGoods(string TableName , int ID , string Name , string Category ,double Quantity , double Cost , double Price , string Type , string Barcode , double Earned )
        {
            string UpdateString = "UPDATE " + TableName + " SET (ID ,Name ,Category ,Quantity ,Cost ,Price ,Type  ,Earned  ) = (@id ,@name , @category , @quantity , @cost , @price ,@type  ,@earned  ) WHERE Barcode =@barcode";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(UpdateString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@category", Category);
                    cmd.Parameters.AddWithValue("@quantity", Quantity);
                    cmd.Parameters.AddWithValue("@cost", Cost);
                    cmd.Parameters.AddWithValue("@price", Price);
                    cmd.Parameters.AddWithValue("@type", Type);
                  
                    cmd.Parameters.AddWithValue("@earned", Earned);
                    
                 
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }
        }
        public bool UpdateGoodCount(string TableName  , string Barcode, double Quantity)
        {
        
            string UpdateString = "UPDATE " + TableName + " SET (Quantity) = (@quantity) WHERE Barcode =@barcode";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(UpdateString, conn))
                {
                    cmd.Parameters.AddWithValue("@quantity", Quantity);
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }

          
        }
        public bool RemoveGoods(string TableName, string Barcode)
        {
            string RemoveString = "DELETE FROM " + TableName + " WHERE Barcode =@barcode";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(RemoveString, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }
        }
    }
}
