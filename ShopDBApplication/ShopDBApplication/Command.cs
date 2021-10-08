using DAL.ADO;
using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDBApplication
{
    public class Command
    {
        public string connstr = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
        public void GetProducts()
        {            
            var productDal = new ProductDal(connstr);
            var products = productDal.GetAllProducts();
            foreach (var item in products)
            {
                Console.WriteLine($"{item.ProductID}\t{item.Name}\t{item.CategoryID}\t{item.Price}");
            }
        }
        public void GetProviders()
        {
            var providerDal = new ProviderDal(connstr);
            var providers = providerDal.GetAllProviders();
            foreach (var item in providers)
            {
                Console.WriteLine($"{item.ProviderID}\t{item.Name}");
            }

        }
        public void GetOrders()
        {
            var orderDal = new OrderDal(connstr);
            var orders = orderDal.GetAllOrders();
            foreach (var item in orders)
            {
                Console.WriteLine($"{item.OrderID}\t{item.ProductID}\t{item.Quantity}");
            }

        }
        public void GetCategories()
        {
            var categoryDal = new CategoryDal(connstr);
            var categories = categoryDal.GetAllCategories();
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.CategoryID}\t{item.Name}");
            }

        }
        public void GetContracts()
        {
            var contractDal = new ContractDal(connstr);
            var contracts = contractDal.GetAllContracts();
            foreach (var item in contracts)
            {
                Console.WriteLine($"{item.ContractID}\t{item.CategoryID}\t{item.ProviderID}");
            }

        }


        public void CreateProduct()
        {
            var random = new Random();
            int pProductID = random.Next();
            Console.WriteLine("NAME of product = ");
            string pName = Console.ReadLine();
            Console.WriteLine("Category_ID of product = ");
            int CategoryID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("PRICE of product = ");
            int pPrice = Int32.Parse(Console.ReadLine());
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            ProductDTO new_product = new ProductDTO
            {
                ProductID = pProductID,
                Name = pName,
                CategoryID = CategoryID,
                Price = pPrice,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime,
            };
            var prod = new ProductDal(connstr);
            prod.CreateProduct(new_product);
        }
        public void CreateCategory()
        {
            var random = new Random();
            int cCategoryID = random.Next();
            Console.WriteLine("NAME of Category = ");
            string cName = Console.ReadLine();
            DateTime cRowInsertTime = DateTime.UtcNow;
            DateTime cRowUpdateTime = DateTime.UtcNow;

            CategoryDTO new_category = new CategoryDTO
            {
                CategoryID = cCategoryID,
                Name = cName,
                RowInsertTime = cRowInsertTime,
                RowUpdateTime = cRowUpdateTime,
            };
            var cat = new CategoryDal(connstr);
            cat.CreateCategory(new_category);
        }
        public void CreateProvider()
        {
            var random = new Random();
            int pProviderID = random.Next();
            Console.WriteLine("NAME of provider = ");
            string pName = Console.ReadLine();
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            ProviderDTO new_provider = new ProviderDTO
            {
                ProviderID = pProviderID,
                Name = pName,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime,
            };
            var prov = new ProviderDal(connstr);
            prov.CreateProvider(new_provider);
        }
        public void CreateContract()
        {
            var random = new Random();
            int cContractID = random.Next();
            Console.WriteLine("Category_ID of contract = ");
            int cCategoryID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Provider_ID of contract = ");
            int cProviderID = Int32.Parse(Console.ReadLine());
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            ContractDTO new_contract = new ContractDTO
            {
                ContractID = cContractID,
                CategoryID = cCategoryID,
                ProviderID = cProviderID,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime,
            };
            var cont = new ContractDal(connstr);
            cont.CreateContract(new_contract);
        }
        public void CreateOrder()
        {
            var random = new Random();
            int oOrderID = random.Next();
            Console.WriteLine("Product_ID of order = ");
            int oProductID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Quantity of order = ");
            int oQuantity = Int32.Parse(Console.ReadLine());
            DateTime oRowInsertTime = DateTime.UtcNow;
            DateTime oRowUpdateTime = DateTime.UtcNow;

            OrderDTO new_order = new OrderDTO
            {
                OrderID = oOrderID,
                ProductID = oProductID,
                Quantity = oQuantity,
                RowInsertTime = oRowInsertTime,
                RowUpdateTime = oRowUpdateTime,
            };
            var ord = new OrderDal(connstr);
            ord.CreateOrder(new_order);
        }


        public void DeleteProduct()
        {
            Console.WriteLine("PRODUCTID to delete = ");
            int pProductID = Int32.Parse(Console.ReadLine());

            var prod = new ProductDal(connstr);
            prod.DeleteProduct(pProductID);

        }
        public void DeleteCategory()
        {
            Console.WriteLine("Category_ID to delete = ");
            int cCategoryID = Int32.Parse(Console.ReadLine());

            var cat = new CategoryDal(connstr);
            cat.DeleteCategory(cCategoryID);

        }
        public void DeleteProvider()
        {
            Console.WriteLine("Provider_ID to delete = ");
            int pProviderID = Int32.Parse(Console.ReadLine());

            var prov = new ProviderDal(connstr);
            prov.DeleteProvider(pProviderID);

        }
        public void DeleteContract()
        {
            Console.WriteLine("Contract_ID to delete = ");
            int cContractID = Int32.Parse(Console.ReadLine());

            var cont = new ContractDal(connstr);
            cont.DeleteContract(cContractID);

        }
        public void DeleteOrder()
        {
            Console.WriteLine("Order_ID to delete = ");
            int oOrderID = Int32.Parse(Console.ReadLine());

            var cont = new OrderDal(connstr);
            cont.DeleteOrder(oOrderID);

        }


        public void UpdateProduct()
        {
            Console.WriteLine("Product_ID of product = ");
            int pProductID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Name of product = ");
            string pName = Console.ReadLine();
            Console.WriteLine("New Category_ID of product = ");
            int pCategoryID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Price of product = ");
            int pPrice = Int32.Parse(Console.ReadLine());
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            ProductDTO new_product = new ProductDTO
            {
                ProductID = pProductID,
                Name = pName,
                CategoryID = pCategoryID,
                Price = pPrice,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime
            };
            var prod = new ProductDal(connstr);
            prod.UpdateProduct(new_product);

        }
        public void UpdateCategory()
        {
            Console.WriteLine("Category_ID of category = ");
            int cCategoryID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Name of category = ");
            string cName = Console.ReadLine();
            DateTime cRowInsertTime = DateTime.UtcNow;
            DateTime cRowUpdateTime = DateTime.UtcNow;

            CategoryDTO new_category = new CategoryDTO
            {
                CategoryID = cCategoryID,
                Name = cName,
                RowInsertTime = cRowInsertTime,
                RowUpdateTime = cRowUpdateTime
            };
            var cat = new CategoryDal(connstr);
            cat.UpdateCategory(new_category);

        }
        public void UpdateProvider()
        {
            Console.WriteLine("Provider_ID of provider = ");
            int pProviderID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Name of provider = ");
            string pName = Console.ReadLine();
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            ProviderDTO new_provider = new ProviderDTO
            {
                ProviderID = pProviderID,
                Name = pName,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime
            };
            var prov = new ProviderDal(connstr);
            prov.UpdateProvider(new_provider);

        }
        public void UpdateContract()
        {
            Console.WriteLine("Contract_ID of contract = ");
            int cContractID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Category_ID of contract = ");
            int cCategoryID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Provider_ID of contract = ");
            int cProviderID = Int32.Parse(Console.ReadLine());
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            ContractDTO new_contract = new ContractDTO
            {
                ContractID = cContractID,
                CategoryID = cCategoryID,
                ProviderID = cProviderID,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime
            };
            var cont = new ContractDal(connstr);
            cont.UpdateContract(new_contract);

        }
        public void UpdateOrder()
        {
            Console.WriteLine("Order_ID of order = ");
            int oOrderID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Product_ID of order = ");
            int oProductID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("New Quantity of order = ");
            int oQuantity = Int32.Parse(Console.ReadLine());
            DateTime pRowInsertTime = DateTime.UtcNow;
            DateTime pRowUpdateTime = DateTime.UtcNow;

            OrderDTO new_order = new OrderDTO
            {
                OrderID = oOrderID,
                ProductID = oProductID,
                Quantity = oQuantity,
                RowInsertTime = pRowInsertTime,
                RowUpdateTime = pRowUpdateTime
            };
            var ord = new OrderDal(connstr);
            ord.UpdateOrder(new_order);

        }


        public void GetProductById() 
        {
            Console.WriteLine("Input PRODUCT_ID of product = ");
            string data = Console.ReadLine();
            int pProductID = Convert.ToInt32(data);

            ProductDal prod = new ProductDal(connstr);
            var result_prod = prod.GetProductById(pProductID);
            Console.WriteLine($"{result_prod.ProductID}\t{result_prod.Name}\t{result_prod.CategoryID}\t{result_prod.Price}");

        }

        public void GetCategoryById()
        {
            Console.WriteLine("Input Category_ID of category = ");
            string data = Console.ReadLine();
            int cCategoryID = Convert.ToInt32(data);

            CategoryDal cat = new CategoryDal(connstr);
            var result_cat = cat.GetCategoryById(cCategoryID);
            Console.WriteLine($"{result_cat.CategoryID}\t{result_cat.Name}");

        }
        public void GetProviderById()
        {
            Console.WriteLine("Input Provider_ID of provider = ");
            string data = Console.ReadLine();
            int pProviderID = Convert.ToInt32(data);

            ProviderDal prov = new ProviderDal(connstr);
            var result_prov = prov.GetProviderById(pProviderID);
            Console.WriteLine($"{result_prov.ProviderID}\t{result_prov.Name}");

        }
        public void GetContractById()
        {
            Console.WriteLine("Input Contract_ID of contract = ");
            string data = Console.ReadLine();
            int cContractID = Convert.ToInt32(data);

            ContractDal cont = new ContractDal(connstr);
            var result_cont = cont.GetContractById(cContractID);
            Console.WriteLine($"{result_cont.ContractID}\t{result_cont.CategoryID}\t{result_cont.ProviderID}");

        }

        public void GetOrderById()
        {
            Console.WriteLine("Input Order_ID of order = ");
            string data = Console.ReadLine();
            int oOrderID = Convert.ToInt32(data);

            OrderDal ord = new OrderDal(connstr);
            var result_ord = ord.GetOrderById(oOrderID);
            Console.WriteLine($"{result_ord.OrderID}\t{result_ord.ProductID}\t{result_ord.Quantity}");

        }

    }
}
