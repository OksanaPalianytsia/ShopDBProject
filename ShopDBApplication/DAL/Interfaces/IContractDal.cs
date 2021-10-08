using DTO;
using System.Collections.Generic;
namespace DAL.Interfaces
{
    public interface IContractDal
    {
        List<ContractDTO> GetAllContracts();
        ContractDTO GetContractById(int contractId);
        void UpdateContract(ContractDTO contract);
        void CreateContract(ContractDTO contract);
        void DeleteContract(int contractId);
    }
}
