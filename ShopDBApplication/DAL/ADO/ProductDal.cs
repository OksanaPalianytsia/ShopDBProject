using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.ADO
{
    public class ProductDal : IEntityDal<ProductDTO>
    {
        public string _connStr;

        public ProductDal(string connstr)
        {
            this._connStr = connstr;
        }


        public void CreateItem(ProductDTO product)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Product(Name, CategoryID, Price)  VALUES(@pName, @pCategoryID, @pPrice)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pName", product.Name);
                comm.Parameters.AddWithValue("@pCategoryID", product.CategoryID);
                comm.Parameters.AddWithValue("@pPrice", product.Price);

                int rowsAffected = comm.ExecuteNonQuery();
                Console.WriteLine($"New product created!");

                conn.Close();
            }
        }

        public void DeleteItem(int productId)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Product WHERE ProductID = @pProductID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProductID", productId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }

        public List<ProductDTO> GetAllItems()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Product";                

                SqlDataReader reader = comm.ExecuteReader();

                var products = new List<ProductDTO>();

                while (reader.Read())
                {
                    products.Add(new ProductDTO
                    {
                        ProductID = (int)reader["ProductID"],
                        Name = reader["Name"].ToString(),
                        CategoryID = (int)reader["CategoryID"],
                        Price = (int)reader["Price"],
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                conn.Close();
                return products;
            }
        }

        public ProductDTO GetItemById(int productId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Product WHERE ProductID = @pProductID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProductID", productId);

                SqlDataReader reader = comm.ExecuteReader();
                var product = new ProductDTO();
                while (reader.Read()) 
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.Name = reader["Name"].ToString();
                    product.CategoryID = (int)reader["CategoryID"];
                    product.Price = (int)reader["Price"];
                    product.RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString());
                    product.RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString());
                }
                

                conn.Close();                
                return product;
            }
        }

        public void UpdateItem(ProductDTO product)
        {
            ProductDTO product_new = product;
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "UPDATE Product SET Name = @pName, CategoryID = @pCategoryID, Price = @pPrice, RowUpdateTime = GETUTCDATE()  WHERE ProductID = @pProductID";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProductID", product_new.ProductID);
                comm.Parameters.AddWithValue("@pName", product_new.Name);
                comm.Parameters.AddWithValue("@pCategoryID", product_new.CategoryID);
                comm.Parameters.AddWithValue("@pPrice", product_new.Price);

                int rowUpdated = comm.ExecuteNonQuery();
                Console.WriteLine($"Updated row = {rowUpdated}");

                conn.Close();
            }
            
        }
    }
}
