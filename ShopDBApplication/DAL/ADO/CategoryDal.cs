using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.ADO
{
    public class CategoryDal : IEntityDal<CategoryDTO>
    {
        private string _connStr;
        public CategoryDal(string connStr)
        {
            this._connStr = connStr;
        }

        public void CreateItem(CategoryDTO category)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Category(Name)  VALUES(@cName)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cName", category.Name);;

                int rowsAffected = comm.ExecuteNonQuery();
                Console.WriteLine($"New category ID created!");

                conn.Close();
            }
        }

        public void DeleteItem(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Category WHERE CategoryID = @cCategoryID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cCategoryID", categoryId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }

        public List<CategoryDTO> GetAllItems()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Category";                

                SqlDataReader reader = comm.ExecuteReader();

                var categories = new List<CategoryDTO>();

                while (reader.Read())
                {
                    categories.Add(new CategoryDTO
                    {
                        CategoryID = (int)reader["CategoryID"],
                        Name = reader["Name"].ToString(),
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                conn.Close();
                return categories;
            }
        }

        public CategoryDTO GetItemById(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Category WHERE CategoryID = @cCategoryID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cCategoryID", categoryId);

                SqlDataReader reader = comm.ExecuteReader();
                var category = new CategoryDTO();
                while (reader.Read())
                {
                    category.CategoryID = (int)reader["CategoryID"];
                    category.Name = reader["Name"].ToString();
                    category.RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString());
                    category.RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString());
                }


                conn.Close();
                return category;
            }
        }

        public void UpdateItem(CategoryDTO category)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "UPDATE Category SET Name = @cName, RowUpdateTime = GETUTCDATE()  WHERE CategoryID = @cCategoryID";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cCategoryID", category.CategoryID);
                comm.Parameters.AddWithValue("@cName", category.Name);

                int rowUpdated = comm.ExecuteNonQuery();
                Console.WriteLine($"Updated row = {rowUpdated}");

                conn.Close();
            }

        }
    }
}
