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
    public class OrderDalTests
    {
        IEntityDal<OrderDTO> temp;
        [OneTimeSetUp]
        // 
        public void SetupTest()
        {
            temp = new OrderDal(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
        }
        public List<OrderDTO> GetItemsBySqlForTest()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
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
                conn.Close();
                return orders;
            }
        }

        public void InsertItemBySqlForTest(OrderDTO order)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "INSERT INTO [SHOP].[dbo].[Order](ProductID, Quantity)  VALUES(@oProductID, @oQuantity)";
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@oProductID", order.ProductID);
                comm.Parameters.AddWithValue("@oQuantity", order.Quantity);

                int rowsAffected = comm.ExecuteNonQuery();

                conn.Close();
            }
        }
        public void DeleteItemBySqlForTest(int orderId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopTest"].ConnectionString);
            using (conn)
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


        [Test]
        public void GetAllOrdersTest()
        {
            var random = new Random();
            OrderDTO order_for_test = new OrderDTO
            {
                OrderID = random.Next(),
                ProductID = 8,
                Quantity = 2,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };
            InsertItemBySqlForTest(order_for_test);
            var orders = temp.GetAllItems();
            DeleteItemBySqlForTest(orders[orders.Count() - 1].OrderID);
        }

        [Test]
        public void UpdateOrderTest()
        {
            var orders = GetItemsBySqlForTest();

            DateTime expected = orders[orders.Count() - 1].RowUpdateTime;
            temp.UpdateItem(orders[orders.Count() - 1]);


            orders = GetItemsBySqlForTest();
            DateTime actual = orders[orders.Count() - 1].RowUpdateTime;

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void CreateOrderTest()
        {
            var random = new Random();
            var orders = GetItemsBySqlForTest();
            OrderDTO order_for_test = new OrderDTO
            {
                OrderID = random.Next(),
                ProductID = 22,
                Quantity = 2,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            Console.WriteLine(orders[orders.Count() - 1].ProductID);

            temp.CreateItem(order_for_test);
            orders = GetItemsBySqlForTest();

            Console.WriteLine(orders[orders.Count() - 1].ProductID);

            Assert.IsTrue(orders.Any(m => m.ProductID == order_for_test.ProductID));
        }

        [Test]
        public void DeleteOrderTest()
        {
            var orders = GetItemsBySqlForTest();

            OrderDTO expected_order = orders[orders.Count() - 1];
            temp.DeleteItem(orders[orders.Count() - 1].OrderID);

            orders = GetItemsBySqlForTest();
            OrderDTO actual_order = orders[orders.Count() - 1];

            Console.WriteLine(expected_order.ProductID);
            Console.WriteLine(actual_order.ProductID);

            Assert.AreNotEqual(expected_order, actual_order);
        }


        [Test]
        public void GetCreatedItemByID()
        {
            var random = new Random();
            OrderDTO order_for_test = new OrderDTO
            {
                OrderID = random.Next(),
                ProductID = 22,
                Quantity = 2,
                RowInsertTime = DateTime.UtcNow,
                RowUpdateTime = DateTime.UtcNow
            };

            InsertItemBySqlForTest(order_for_test);

            var orders = GetItemsBySqlForTest();
            OrderDTO actual_order = temp.GetItemById(orders[orders.Count() - 1].OrderID);
            DeleteItemBySqlForTest(orders[orders.Count() - 1].OrderID);

            Assert.AreEqual(order_for_test.ProductID, actual_order.ProductID);
        }

    }
}