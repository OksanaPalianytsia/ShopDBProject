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
    public class ProviderDalTests
    {
        IEntityDal<ProviderDTO> temp;
        [OneTimeSetUp]
        // 
        public void SetupTest()
        {
            temp = new ProviderDal(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
        }
        public List<ProviderDTO> GetItemsBySqlForTest()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Provider";

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

        public void InsertItemBySqlForTest(ProviderDTO provider)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Provider(Name)  VALUES(@pName)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pName", provider.Name);

                int rowsAffected = comm.ExecuteNonQuery();

                conn.Close();
            }
        }
        public void DeleteItemBySqlForTest(int providerId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Provider WHERE ProviderID = @pProviderID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@pProviderID", providerId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }


        [Test]
        public void GetAllProvidersTest()
        {
            var random = new Random();
            ProviderDTO prov_for_test = new ProviderDTO
            {
                ProviderID = random.Next(),
                Name = "Orest",
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };
            InsertItemBySqlForTest(prov_for_test);
            var providers = temp.GetAllItems();
            DeleteItemBySqlForTest(providers[providers.Count() - 1].ProviderID);
        }

        [Test]
        public void UpdateProviderTest()
        {
            var providers = GetItemsBySqlForTest();

            DateTime expected = providers[providers.Count() - 1].RowUpdateTime;
            temp.UpdateItem(providers[providers.Count() - 1]);


            providers = GetItemsBySqlForTest();
            DateTime actual = providers[providers.Count() - 1].RowUpdateTime;

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void CreateProviderTest()
        {
            var random = new Random();
            var providers= GetItemsBySqlForTest();
            ProviderDTO prov_for_test = new ProviderDTO
            {

                ProviderID = random.Next(),
                Name = "Oleksandr",
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            Console.WriteLine(providers[providers.Count() - 1].Name);

            temp.CreateItem(prov_for_test);
            providers = GetItemsBySqlForTest();

            Console.WriteLine(providers[providers.Count() - 1].Name);

            Assert.IsTrue(providers.Any(m => m.Name == prov_for_test.Name));
        }

        [Test]
        public void DeleteProductTest()
        {
            var providers = GetItemsBySqlForTest();

            ProviderDTO expected_provider = providers[providers.Count() - 1];
            temp.DeleteItem(providers[providers.Count() - 1].ProviderID);

            providers = GetItemsBySqlForTest();
            ProviderDTO actual_provider = providers[providers.Count() - 1];

            Console.WriteLine(expected_provider.Name);
            Console.WriteLine(actual_provider.Name);

            Assert.AreNotEqual(expected_provider, actual_provider);
        }


        [Test]
        public void GetCreatedItemByID()
        {
            var random = new Random();
            ProviderDTO prov_for_test = new ProviderDTO
            {

                ProviderID = random.Next(),
                Name = "Khrystyna",
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            InsertItemBySqlForTest(prov_for_test);

            var products = GetItemsBySqlForTest();
            ProviderDTO actual_provider = temp.GetItemById(products[products.Count() - 1].ProviderID);
            DeleteItemBySqlForTest(products[products.Count() - 1].ProviderID);

            Assert.AreEqual(prov_for_test.Name, actual_provider.Name);
        }

    }
}
