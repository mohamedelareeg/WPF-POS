using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovaPOS.Manager.Database
{
    public class Expense
    {
        Server server = new Server();

        //private int id;
        //private string name;
        //private string owner;
        //private string datex;
        //private double cost;
        //private string details;

        public void InsertExpenses(string TableName, string Name, string Owner , string Datex, double Cost, string Details)
        {
            string insertString = "insert into " + TableName + "(Name ,Owner ,Datex ,Cost ,Details  ) VALUES (@name , @owner , @datex , @cost , @details )";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(insertString, conn))
                {
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@owner", Owner);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@cost", Cost);
                    cmd.Parameters.AddWithValue("@details", Details);
                    
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        public List<Models.Expenses> ReadExpenses(string TableName)
        {

            List<Models.Expenses> goods = new List<Models.Expenses>();
            string readString = "SELECT * FROM " + TableName + " ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(readString, conn))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Name ,Owner ,Datex ,Cost ,Details  ) VALUES (@name , @owner , @datex , @cost , @details
                        var goods_List = new Models.Expenses();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Owner = reader["Owner"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Details = reader["Details"].ToString();

                        //goods_List.Details = reader["Details"].ToString();
                        goods.Add(goods_List);

                    }
                    return goods;
                }
            }
        }
        public List<Models.Expenses> ReadExpenses_Range(string TableName, string FieldName, string FieldValue, string FieldValue2)
        {

            List<Models.Expenses> goods = new List<Models.Expenses>();
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
                        //Name ,Owner ,Datex ,Cost ,Details  ) VALUES (@name , @owner , @datex , @cost , @details
                        var goods_List = new Models.Expenses();
                        goods_List.Id = Convert.ToInt32(reader["ID"]);
                        goods_List.Name = reader["Name"].ToString();
                        goods_List.Owner = reader["Owner"].ToString();
                        goods_List.Datex = reader["Datex"].ToString();
                        goods_List.Cost = Convert.ToDouble(reader["Cost"]);
                        goods_List.Details = reader["Details"].ToString();

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
      
        public bool UpdateExpenses(string TableName, int ID, string Name, string Owner, string Datex, double Cost, string Details)
        {
            //Name ,Owner ,Datex ,Cost ,Details  ) VALUES (@name , @owner , @datex , @cost , @details
            string UpdateString = "UPDATE " + TableName + " SET (ID ,Name ,Owner ,Datex ,Cost ,Details) = (@id ,@name , @owner , @datex , @cost , @details) WHERE ID =@id";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(UpdateString, conn))
                {
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@owner", Owner);
                    cmd.Parameters.AddWithValue("@datex", Datex);
                    cmd.Parameters.AddWithValue("@cost", Cost);
                    cmd.Parameters.AddWithValue("@details", Details);
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
