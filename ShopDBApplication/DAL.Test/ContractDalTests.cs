using DAL.ADO;
using DAL.Interfaces;
using DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Test
{
    [TestFixture]
    public class ContractDalTests
    {
        IEntityDal<ContractDTO> temp;
        [OneTimeSetUp]
        // 
        public void SetupTest()
        {
            temp = new ContractDal(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
        }
        public List<ContractDTO> GetItemsBySqlForTest()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = "select * from Contract";
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();

                var contracts = new List<ContractDTO>();

                while (reader.Read())
                {
                    contracts.Add(new ContractDTO
                    {
                        ContractID = (int)reader["ContractID"],
                        CategoryID = (int)reader["CategoryID"],
                        ProviderID = (int)reader["ProviderID"],
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                conn.Close();
                return contracts;
            }
        }

        public void InsertItemBySqlForTest(ContractDTO contract)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Contract(CategoryID, ProviderID)  VALUES(@cCategoryID, @cProviderID)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cCategoryID", contract.CategoryID);
                comm.Parameters.AddWithValue("@cProviderID", contract.ProviderID);

                int rowsAffected = comm.ExecuteNonQuery();

                conn.Close();
            }
        }
        public void DeleteItemBySqlForTest(int contractId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE Contract WHERE ContractID = @cContractId";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cContractId", contractId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }


        [Test]
        public void GetAllContractsTest()
        {
            var random = new Random();
            ContractDTO contract_for_test = new ContractDTO
            {
                ContractID = random.Next(),
                CategoryID = 6,
                ProviderID = 9,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };
            InsertItemBySqlForTest(contract_for_test);
            var contracts = temp.GetAllItems();
            DeleteItemBySqlForTest(contracts[contracts.Count() - 1].ContractID);
        }

        [Test]
        public void UpdateContractTest()
        {
            var contracts = GetItemsBySqlForTest();

            DateTime expected = contracts[contracts.Count() - 1].RowUpdateTime;
            temp.UpdateItem(contracts[contracts.Count() - 1]);

            contracts = GetItemsBySqlForTest();
            DateTime actual = contracts[contracts.Count() - 1].RowUpdateTime;

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void CreateContractTest()
        {
            var random = new Random();
            var contracts = GetItemsBySqlForTest();
            ContractDTO Contract_for_test = new ContractDTO
            {
                ContractID = random.Next(),
                CategoryID = 8,
                ProviderID = 9,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            Console.WriteLine(contracts[contracts.Count() - 1].ProviderID);

            temp.CreateItem(Contract_for_test);
            contracts = GetItemsBySqlForTest();

            Console.WriteLine(contracts[contracts.Count() - 1].ProviderID);

            Assert.IsTrue(contracts.Any(m => m.ProviderID == Contract_for_test.ProviderID));
        }

        [Test]
        public void DeleteContractTest()
        {
            var contracts = GetItemsBySqlForTest();

            ContractDTO expected_contract = contracts[contracts.Count() - 1];
            temp.DeleteItem(contracts[contracts.Count() - 1].ContractID);

            contracts = GetItemsBySqlForTest();
            ContractDTO actual_contract = contracts[contracts.Count() - 1];

            Console.WriteLine(expected_contract.ProviderID);
            Console.WriteLine(actual_contract.ProviderID);

            Assert.AreNotEqual(expected_contract, actual_contract);
        }


        [Test]
        public void GetCreatedItemByID()
        {
            var random = new Random();
            ContractDTO contract_for_test = new ContractDTO
            {
                ContractID = random.Next(),
                CategoryID = 9,
                ProviderID = 3,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            InsertItemBySqlForTest(contract_for_test);

            var contracts = GetItemsBySqlForTest();
            ContractDTO actual_Contract = temp.GetItemById(contracts[contracts.Count() - 1].ContractID);
            DeleteItemBySqlForTest(contracts[contracts.Count() - 1].ContractID);

            Assert.AreEqual(contract_for_test.ProviderID, actual_Contract.ProviderID);
        }

    }
}
