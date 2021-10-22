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
    public class CategoryDalTests
    {
        IEntityDal<CategoryDTO> temp;
        [OneTimeSetUp]
        // 
        public void SetupTest()
        {
            temp = new CategoryDal(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
        }
        public List<CategoryDTO> GetItemsBySqlForTest()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
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

        public void InsertItemBySqlForTest(CategoryDTO category)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Category(Name)  VALUES(@pName)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pName", category.Name);

                int rowsAffected = comm.ExecuteNonQuery();

                conn.Close();
            }
        }
        public void DeleteItemBySqlForTest(int categoryId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Category WHERE CategoryID = @cCategoryID AND NOT EXISTS (SELECT * FROM [SHOP_for_tests].[dbo].[Product] WHERE [Product].CategoryID = Category.CategoryID) AND NOT EXISTS (SELECT * FROM [SHOP_for_tests].[dbo].[Contract] WHERE [Contract].CategoryID = Category.CategoryID)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cCategoryID", categoryId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }


        [Test]
        public void GetAllCategoriesTest()
        {
            var random = new Random();
            CategoryDTO prov_for_test = new CategoryDTO
            {
                CategoryID = random.Next(),
                Name = "Jewelry",
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };
            InsertItemBySqlForTest(prov_for_test);
            var categories = temp.GetAllItems();
            DeleteItemBySqlForTest(categories[categories.Count() - 1].CategoryID);
        }

        [Test]
        public void UpdateCategoryTest()
        {
            var categories = GetItemsBySqlForTest();

            DateTime expected = categories[categories.Count() - 1].RowUpdateTime;
            temp.UpdateItem(categories[categories.Count() - 1]);

            categories = GetItemsBySqlForTest();
            DateTime actual = categories[categories.Count() - 1].RowUpdateTime;

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void CreateCategoryTest()
        {
            var random = new Random();
            var categories = GetItemsBySqlForTest();
            CategoryDTO cat_for_test = new CategoryDTO
            {

                CategoryID = random.Next(),
                Name = "Technique",
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            Console.WriteLine(categories[categories.Count() - 1].Name);

            temp.CreateItem(cat_for_test);
            categories = GetItemsBySqlForTest();

            Console.WriteLine(categories[categories.Count() - 1].Name);

            Assert.IsTrue(categories.Any(m => m.Name == cat_for_test.Name));
        }

        [Test]
        public void DeleteProductTest()
        {
            var categories = GetItemsBySqlForTest();

            CategoryDTO expected_category = categories[categories.Count() - 1];
            temp.DeleteItem(categories[categories.Count() - 1].CategoryID);

            categories = GetItemsBySqlForTest();
            CategoryDTO actual_category = categories[categories.Count() - 1];

            Console.WriteLine(expected_category.Name);
            Console.WriteLine(actual_category.Name);

            Assert.AreNotEqual(expected_category, actual_category);
        }


        [Test]
        public void GetCreatedItemByID()
        {
            var random = new Random();
            CategoryDTO cat_for_test = new CategoryDTO
            {

                CategoryID = random.Next(),
                Name = "Plants",
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            InsertItemBySqlForTest(cat_for_test);

            var categories = GetItemsBySqlForTest();
            CategoryDTO actual_category = temp.GetItemById(categories[categories.Count() - 1].CategoryID);
            DeleteItemBySqlForTest(categories[categories.Count() - 1].CategoryID);

            Assert.AreEqual(cat_for_test.Name, actual_category.Name);
        }

    }
}
