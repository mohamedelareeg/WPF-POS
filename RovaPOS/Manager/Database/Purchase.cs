using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Purchase
    {
        Server server = new Server();

        //private int id;
        //private string seller;
        //private string phonenumber;
        //private string datex;
        //private double cost;
        //private double paid;
        //private double remain;
        //private double tax;
        public void InsertPurchases(string TableName, string Seller, string Phonenumber , string Datex, double Cost, double Paid, double Remain, double Tax)
        {
            string insertString = "insert into " + TableName + "(Seller ,Phonenumber ,Datex ,Cost ,Paid ,Remain ,Tax ) VALUES (@seller , @phonenumber , @datex , @cost , @paid ,@remain ,@tax )";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertString, conn))
                {
                    cmd.Parameters.AddWithValue("@seller", Seller);
                    cmd.Parameters.AddWithValue("@phonenumber", Phonenumber);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@cost", Cost);
                    cmd.Parameters.AddWithValue("@paid", Paid);
                    cmd.Parameters.AddWithValue("@remain", Remain);
                    cmd.Parameters.AddWithValue("@tax", Tax);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        public List<Models.Purchase> ReadPurchase_Range(string TableName, string FieldName, string FieldValue, string FieldValue2)
        {

            List<Models.Purchase> goods = new List<Models.Purchase>();
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
                        var goods_List = new Models.Purchase();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Seller = reader["Seller"].ToString();
                        goods_List.Phonenumber = reader["Phonenumber"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Paid = Convert.ToDouble(reader["Paid"]);
                        goods_List.Remain = Convert.ToDouble(reader["Remain"]);
                        goods_List.Tax = Convert.ToDouble(reader["Tax"]);
                        //goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Purchase> ReadPurchase(string TableName)
        {

            List<Models.Purchase> goods = new List<Models.Purchase>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Purchase();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Seller = reader["Seller"].ToString();
                        goods_List.Phonenumber = reader["Phonenumber"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Paid = Convert.ToDouble(reader["Paid"]);
                        goods_List.Remain = Convert.ToDouble(reader["Remain"]);
                        goods_List.Tax = Convert.ToDouble(reader["Tax"]);
                        //goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public string ReadBillnumber(string TableName, string Fieldname)
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
                        returned_value = reader["ID"].ToString();

                    }
                    return returned_value;
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
      
        public bool UpdatePurchase(string TableName, int ID, string Seller, string Phonenumber, string Datex, double Cost, double Paid, double Remain, double Tax)
        {
            string UpdateString = "UPDATE " + TableName + " SET (ID ,Seller ,Phonenumber ,Datex ,Cost ,Paid ,Remain ,Tax) = (@id ,@seller , @phonenumber , @datex , @cost , @paid ,@remain ,@tax) WHERE ID =@id";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(UpdateString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@seller", Seller);
                    cmd.Parameters.AddWithValue("@phonenumber", Phonenumber);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@cost", Cost);
                    cmd.Parameters.AddWithValue("@paid", Paid);
                    cmd.Parameters.AddWithValue("@remain", Remain);
                    cmd.Parameters.AddWithValue("@tax", Tax);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }
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

    }
}
