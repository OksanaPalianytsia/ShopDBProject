using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.ADO
{
    public class ProviderDal : IEntityDal <ProviderDTO>
    {
        private string _connStr;
        public ProviderDal(string connStr)
        {
            this._connStr = connStr;
        }

        public void CreateItem(ProviderDTO provider)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Provider(Name)  VALUES(@pName)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pName", provider.Name);

                int rowsAffected = comm.ExecuteNonQuery();
                Console.WriteLine($"New provider created!");

                conn.Close();
            }
        }

        public void DeleteItem(int providerId)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Provider WHERE ProviderID = @pProviderID AND NOT EXISTS (SELECT * FROM [SHOP].[dbo].[Contract] WHERE [Contract].ProviderID = Provider.ProviderID)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProviderID", providerId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }

        public List<ProviderDTO> GetAllItems()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Provider";
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                var providers = new List<ProviderDTO>();

                while (reader.Read())
                {
                    providers.Add(new ProviderDTO
                    {
                        ProviderID = (int)reader["ProviderID"],
                        Name = reader["Name"].ToString(),
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                conn.Close();
                return providers;
            }
        }

        public ProviderDTO GetItemById(int providerId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Provider WHERE ProviderID = @pProviderID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProviderID", providerId);

                SqlDataReader reader = comm.ExecuteReader();
                var provider = new ProviderDTO();
                while (reader.Read())
                {
                    provider.ProviderID = (int)reader["ProviderID"];
                    provider.Name = reader["Name"].ToString();
                    provider.RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString());
                    provider.RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString());
                }


                conn.Close();
                return provider;
            }
        }

        public void UpdateItem(ProviderDTO provider)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "UPDATE Provider SET Name = @pName, RowUpdateTime = GETUTCDATE()  WHERE ProviderID = @pProviderID";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProviderID", provider.ProviderID);
                comm.Parameters.AddWithValue("@pName", provider.Name);

                int rowUpdated = comm.ExecuteNonQuery();
                Console.WriteLine($"Updated row = {rowUpdated}");

                conn.Close();
            }
        }
    }
}
