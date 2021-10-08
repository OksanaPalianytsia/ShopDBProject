using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ADO
{
    public class ContractDal : IContractDal
    {
        private string _connStr;
        public ContractDal(string connStr)
        {
            this._connStr = connStr;
        }

        public void CreateContract(ContractDTO contract)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO Contract(CategoryID, ProviderID)  VALUES(@cCategoryID, @cProviderID)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cCategoryID", contract.CategoryID);
                comm.Parameters.AddWithValue("@cProviderID", contract.ProviderID);

                int rowsAffected = comm.ExecuteNonQuery();
                Console.WriteLine($"New contract ID = {contract.ContractID}");

                conn.Close();
            }
        }

        public void DeleteContract(int contractId)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
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

        public List<ContractDTO> GetAllContracts()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
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
                return contracts;
            }
        }

        public ContractDTO GetContractById(int contractId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM Contract WHERE ContractID = @cContractID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cContractID", contractId);

                SqlDataReader reader = comm.ExecuteReader();
                var contract = new ContractDTO();
                while (reader.Read())
                {
                    contract.ContractID = (int)reader["ContractID"];
                    contract.CategoryID = (int)reader["CategoryID"];
                    contract.ProviderID = (int)reader["ProviderID"];
                    contract.RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString());
                    contract.RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString());
                }


                conn.Close();
                return contract;
            }
        }

        public void UpdateContract(ContractDTO contract)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "UPDATE Contract SET CategoryID = @cCategoryID, ProviderID = @cProviderID, RowUpdateTime = GETUTCDATE()  WHERE ContractID = @cContractID";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cContractID", contract.ContractID);
                comm.Parameters.AddWithValue("@cCategoryID", contract.CategoryID);
                comm.Parameters.AddWithValue("@cProviderID", contract.ProviderID);

                int rowUpdated = comm.ExecuteNonQuery();
                Console.WriteLine($"Updated row = {rowUpdated}");

                conn.Close();
            }
        }
    }
}
