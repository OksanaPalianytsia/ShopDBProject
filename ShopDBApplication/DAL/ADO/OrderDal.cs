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
    public class OrderDal : IOrderDal
    {
        private string _connStr;
        public OrderDal(string connStr)
        {
            this._connStr = connStr;
        }
        public void CreateOrder(OrderDTO order)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO [SHOP].[dbo].[Order](ProductID, Quantity)  VALUES(@oProductID, @oQuantity)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@oProductID", order.ProductID);
                comm.Parameters.AddWithValue("@oQuantity", order.Quantity);

                int rowsAffected = comm.ExecuteNonQuery();
                Console.WriteLine($"New order ID = {order.OrderID}");

                conn.Close();
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "DELETE [SHOP].[dbo].[Order] WHERE OrderID = @oOrderId";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@oOrderId", orderId);

                int rowsDeleted = comm.ExecuteNonQuery();
                Console.WriteLine($"Deleted row = {rowsDeleted}");

                conn.Close();
            }
        }

        public List<OrderDTO> GetAllOrders()
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {                
                conn.Open();

                comm.CommandText = "select * from [SHOP].[dbo].[Order]";

                SqlDataReader reader = comm.ExecuteReader();

                var orders = new List<OrderDTO>();

                while (reader.Read())
                {
                    orders.Add(new OrderDTO
                    {
                        OrderID = (int)reader["OrderID"],
                        ProductID = (int)reader["ProductID"],
                        Quantity = (int)reader["Quantity"],
                        RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString()),
                        RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString())
                    });
                }
                return orders;
            }
        }

        public OrderDTO GetOrderById(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT * FROM [SHOP].[dbo].[Order] WHERE OrderID = @oOrderID";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@oOrderID", orderId);

                SqlDataReader reader = comm.ExecuteReader();
                var order = new OrderDTO();
                while (reader.Read())
                {
                    order.OrderID = (int)reader["OrderID"];
                    order.ProductID = (int)reader["ProductID"];
                    order.Quantity = (int)reader["Quantity"];
                    order.RowInsertTime = DateTime.Parse(reader["RowInsertTime"].ToString());
                    order.RowUpdateTime = DateTime.Parse(reader["RowUpdateTime"].ToString());
                }


                conn.Close();
                return order;
            }
        }

        public void UpdateOrder(OrderDTO order)
        {
            using (SqlConnection conn = new SqlConnection(this._connStr))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "UPDATE [SHOP].[dbo].[Order] SET ProductID = @oProductID, Quantity = @oQuantity, RowUpdateTime = GETUTCDATE()  WHERE OrderID = @oOrderID";

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@oProductID", order.ProductID);
                comm.Parameters.AddWithValue("@oQuantity", order.Quantity);
                comm.Parameters.AddWithValue("@oOrderID", order.OrderID);

                int rowUpdated = comm.ExecuteNonQuery();
                Console.WriteLine($"Updated row = {rowUpdated}");

                conn.Close();
            }
        }
    }
}
