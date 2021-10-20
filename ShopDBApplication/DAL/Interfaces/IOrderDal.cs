using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IOrderDal
    {
        List<ContractDTO> GetAllOrders();
        ContractDTO GetOrderById(int orderId);
        void UpdateOrder(ContractDTO order);
        void CreateOrder(ContractDTO order);
        void DeleteOrder(int orderId);
    }
}
