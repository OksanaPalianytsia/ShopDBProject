using DAL.ADO;
using DAL.Interfaces;
using DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Test
{
    [TestFixture]
    public class ProductDalTests
    {
        IEntityDal<ProductDTO> temp;
        [OneTimeSetUp]
        // 
        public void SetupTest()
        {
            temp = new ProductDal(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
        }
        public List<ProductDTO> GetItemsBySqlForTest()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
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

        public void InsertItemBySqlForTest(ProductDTO product)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Product(Name, CategoryID, Price)  VALUES(@pName, @pCategoryID, @pPrice)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pName", product.Name);
                comm.Parameters.AddWithValue("@pCategoryID", product.CategoryID);
                comm.Parameters.AddWithValue("@pPrice", product.Price);

                int rowsAffected = comm.ExecuteNonQuery();

                conn.Close();
            }
        }
        public void DeleteItemBySqlForTest(int productId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Product WHERE ProductID = @pProductID AND NOT EXISTS (SELECT * FROM [SHOP_for_tests].[dbo].[Order] WHERE [Order].ProductID = Product.ProductID)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProductID", productId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }


        [Test]
        public void GetAllProductsTest() 
        {
            var random = new Random();
            ProductDTO prod_for_test = new ProductDTO
            {
                ProductID = random.Next(),
                Name = "Skirt",
                CategoryID = 4,
                Price = 450,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };
            InsertItemBySqlForTest(prod_for_test);
            var products = temp.GetAllItems();
            DeleteItemBySqlForTest(products[products.Count() - 1].ProductID);
        }
        
        [Test]
        public void UpdateProductTest()
        {
            var products = GetItemsBySqlForTest();

            DateTime expected = products[products.Count() - 1].RowUpdateTime;
            temp.UpdateItem(products[products.Count() - 1]);


            products = GetItemsBySqlForTest();
            DateTime actual = products[products.Count() - 1].RowUpdateTime;

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void CreateProductTest()
        {
            var random = new Random();
            var products = GetItemsBySqlForTest();
            ProductDTO prod_for_test = new ProductDTO
            {                
                 
                ProductID = random.Next(),
                Name = "Butter",
                CategoryID = 1,
                Price = 45,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            Console.WriteLine(products[products.Count() - 1].Name);

            temp.CreateItem(prod_for_test);
            products = GetItemsBySqlForTest();

            Console.WriteLine(products[products.Count() - 1].Name);

            Assert.IsTrue(products.Any(m => m.Name == prod_for_test.Name));
        }

        [Test]
        public void DeleteProductTest()
        {
            var products = GetItemsBySqlForTest();     

            ProductDTO expected_product = products[products.Count() - 1];
            temp.DeleteItem(products[products.Count() - 1].ProductID);

            products = GetItemsBySqlForTest();
            ProductDTO actual_product = products[products.Count() - 1];

            Console.WriteLine(expected_product.Name);
            Console.WriteLine(actual_product.Name);

            Assert.AreNotEqual(expected_product, actual_product);
        }


        [Test]
        public void GetCreatedItemByID()
        {
            var random = new Random();
            ProductDTO prod_for_test = new ProductDTO
            {

                ProductID = random.Next(),
                Name = "Orange",
                CategoryID = 1,
                Price = 52,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            InsertItemBySqlForTest(prod_for_test);

            var products = GetItemsBySqlForTest();
            ProductDTO actual_product = temp.GetItemById(products[products.Count() - 1].ProductID);
            DeleteItemBySqlForTest(products[products.Count() - 1].ProductID);

            Assert.AreEqual(prod_for_test.Name, actual_product.Name);
        }

    }
}
