using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProviderDal
    {
        List<ProviderDTO> GetAllProviders();
        ProviderDTO GetProviderById(int providerId);
        void UpdateProvider(ProviderDTO provider);
        void CreateProvider(ProviderDTO provider);
        void DeleteProvider(int providerId);
    }
}
