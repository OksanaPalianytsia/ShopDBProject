using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IOrderDal
    {
        List<OrderDTO> GetAllOrders();
        OrderDTO GetOrderById(int orderId);
        void UpdateOrder(OrderDTO order);
        void CreateOrder(OrderDTO order);
        void DeleteOrder(int orderId);
    }
}
