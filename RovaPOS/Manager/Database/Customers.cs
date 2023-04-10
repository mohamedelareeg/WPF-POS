using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Customers
    {
        Server server = new Server();

        //private int id;
        //private string ownername;
        //private string ownerid;
        //private string ownernumber;
        //private double paid;
        //private double remain;

        public void InsertCustomers(string TableName, string Ownername, string Ownerid, string Ownernumber, double Paid, double Remain)
        {
            string insertString = "insert into " + TableName + "(Ownername ,Ownerid ,Ownernumber ,Paid ,Remain ) VALUES (@ownername , @ownerid , @ownernumber  , @paid ,@remain)";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertString, conn))
                {
                    cmd.Parameters.AddWithValue("@ownername", Ownername);
                    cmd.Parameters.AddWithValue("@ownerid", Ownerid);
                    cmd.Parameters.AddWithValue("@ownernumber", Ownernumber);
                    cmd.Parameters.AddWithValue("@paid", Paid);
                    cmd.Parameters.AddWithValue("@remain", Remain);
                 
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        public List<Models.Customers> ReadCustomers_Range(string TableName, string FieldName, string FieldValue, string FieldValue2)
        {

            List<Models.Customers> goods = new List<Models.Customers>();
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
                        var goods_List = new Models.Customers();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Ownername = reader["Ownername"].ToString();
                        goods_List.Ownerid = reader["Ownerid"].ToString();
                        goods_List.Ownernumber = reader["Ownernumber"].ToString();
                      
                        goods_List.Paid = Convert.ToDouble(reader["Paid"]);
                        goods_List.Remain = Convert.ToDouble(reader["Remain"]);
                      
                        //goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Customers> ReadCustomers(string TableName)
        {

            List<Models.Customers> goods = new List<Models.Customers>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var goods_List = new Models.Customers();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Ownername = reader["Ownername"].ToString();
                        goods_List.Ownerid = reader["Ownerid"].ToString();
                        goods_List.Ownernumber = reader["Ownernumber"].ToString();

                        goods_List.Paid = Convert.ToDouble(reader["Paid"]);
                        goods_List.Remain = Convert.ToDouble(reader["Remain"]);

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
        public bool UpdateCustomers(string TableName, int ID, string Ownername, string Ownerid, string Ownernumber, double Paid, double Remain)
        {
            string UpdateString = "UPDATE " + TableName + " SET (ID ,Ownername ,Ownerid ,Ownernumber ,Paid ,Remain ) = (@id ,@ownername , @ownerid , @ownernumber  , @paid ,@remain) WHERE ID =@id";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(UpdateString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@ownername", Ownername);
                    cmd.Parameters.AddWithValue("@ownerid", Ownerid);
                    cmd.Parameters.AddWithValue("@ownernumber", Ownernumber);
                    cmd.Parameters.AddWithValue("@paid", Paid);
                    cmd.Parameters.AddWithValue("@remain", Remain);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return true;
                }
            }
        }
      

    }
}
