using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProductDal
    {
        List<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int productId);
        void UpdateProduct(ProductDTO product);
        void CreateProduct(ProductDTO product);
        void DeleteProduct(int productId);
    }
}
