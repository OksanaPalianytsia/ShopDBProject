using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDBApplication
{
    class Menu
    {
        Command tmp = new Command();
        public void ShowMenu() 
        {
            while (true)
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("--------------------");
                Console.WriteLine("[1] Product options");
                Console.WriteLine("[2] Category options");
                Console.WriteLine("[3] Provider options");
                Console.WriteLine("[4] Contract options");
                Console.WriteLine("[5] Order options");
                Console.WriteLine("[0] Exit the program");
                Console.WriteLine("--------------------\n");
                Console.WriteLine("Please select an option from 0-5\n");

                string choice = Console.ReadLine();
                int number;
                bool result = Int32.TryParse(choice, out number);
                if (result)
                {
                    SubMenu(number);
                }
                else
                {
                    Console.WriteLine("Incorrect choice!!!");
                }
            }
        }
        public void SubMenu(int ShowMenuChoice)
        {
            switch (ShowMenuChoice)
            {
                case 1:
                    Console.WriteLine("\nOptions");
                    Console.WriteLine("[1] Create product");
                    Console.WriteLine("[2] Delete product");
                    Console.WriteLine("[3] Get all products");
                    Console.WriteLine("[4] Get product by ID");
                    Console.WriteLine("[5] Update product");
                    Console.WriteLine("[6] Return Main Menu");
                    Console.WriteLine("[0] Exit the program");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 0-6\n");
                    break;

                case 2:
                    Console.WriteLine("\nOptions");
                    Console.WriteLine("[1] Create category");
                    Console.WriteLine("[2] Delete category");
                    Console.WriteLine("[3] Get all categories");
                    Console.WriteLine("[4] Get category by ID");
                    Console.WriteLine("[5] Update category");
                    Console.WriteLine("[6] Return Main Menu");
                    Console.WriteLine("[0] Exit the program");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 0-6\n");
                    break;

                case 3:
                    Console.WriteLine("\nOptions");
                    Console.WriteLine("[1] Create provider");
                    Console.WriteLine("[2] Delete provider");
                    Console.WriteLine("[3] Get all providers");
                    Console.WriteLine("[4] Get provider by ID");
                    Console.WriteLine("[5] Update provider");
                    Console.WriteLine("[6] Return Main Menu");
                    Console.WriteLine("[0] Exit the program");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 0-6\n");
                    break;
                case 4:
                    Console.WriteLine("\nOptions");
                    Console.WriteLine("[1] Create contract");
                    Console.WriteLine("[2] Delete contract");
                    Console.WriteLine("[3] Get all contracts");
                    Console.WriteLine("[4] Get contract by ID");
                    Console.WriteLine("[5] Update contract");
                    Console.WriteLine("[6] Return Main Menu");
                    Console.WriteLine("[0] Exit the program");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 0-6\n");
                    break;
                case 5:
                    Console.WriteLine("\nOptions");
                    Console.WriteLine("[1] Create order");
                    Console.WriteLine("[2] Delete order");
                    Console.WriteLine("[3] Get all orders");
                    Console.WriteLine("[4] Get order by ID");
                    Console.WriteLine("[5] Update order");
                    Console.WriteLine("[6] Return Main Menu");
                    Console.WriteLine("[0] Exit the program");
                    Console.WriteLine("--------------------\n");
                    Console.WriteLine("Please select an option from 0-6\n");
                    break;

                case 0:
                    Environment.Exit(0);
                    break;
                default: break;
            }
            string choice = Console.ReadLine();
            int number;
            bool result = Int32.TryParse(choice, out number);
            if (result)
            {
                Action(ShowMenuChoice, number);
            }
            else
            {
                Console.WriteLine("Incorrect choice!!!");
            }
        }
        public void Action(int menu, int choice)
        {
            switch (menu)
            {
                case 1:
                    switch (choice)
                    {
                        case 1:
                            tmp.CreateProduct(); break;
                        case 2:
                            tmp.DeleteProduct(); break;
                        case 3:
                            tmp.GetProducts(); break;
                        case 4:
                            tmp.GetProductById(); break;
                        case 5:
                            tmp.UpdateProduct(); break;
                        case 6:
                            Console.Clear(); ShowMenu(); break;
                        case 0:
                            Environment.Exit(0); break;
                    }
                    break;

                case 2:
                    switch (choice)
                    {
                        case 1:
                            tmp.CreateCategory(); break;
                        case 2:
                            tmp.DeleteCategory(); break;
                        case 3:
                            tmp.GetCategories(); break;
                        case 4:
                            tmp.GetCategoryById(); break;
                        case 5:
                            tmp.UpdateCategory(); break;
                        case 6:
                            Console.Clear(); ShowMenu(); break;
                        case 0:
                            Environment.Exit(0); break;
                    }
                    break;

                case 3:
                    switch (choice)
                    {
                        case 1:
                            tmp.CreateProvider(); break;
                        case 2:
                            tmp.DeleteProvider(); break;
                        case 3:
                            tmp.GetProviders(); break;
                        case 4:
                            tmp.GetProviderById(); break;
                        case 5:
                            tmp.UpdateProvider(); break;
                        case 6:
                            Console.Clear(); ShowMenu(); break;
                        case 0:
                            Environment.Exit(0); break;
                    }
                    break;

                case 4:
                    switch (choice)
                    {
                        case 1:
                            tmp.CreateContract(); break;
                        case 2:
                            tmp.DeleteContract(); break;
                        case 3:
                            tmp.GetContracts(); break;
                        case 4:
                            tmp.GetContractById(); break;
                        case 5:
                            tmp.UpdateContract(); break;
                        case 6:
                            Console.Clear(); ShowMenu(); break;
                        case 0:
                            Environment.Exit(0); break;
                    }
                    break;

                case 5:
                    switch (choice)
                    {
                        case 1:
                            tmp.CreateOrder(); break;
                        case 2:
                            tmp.DeleteOrder(); break;
                        case 3:
                            tmp.GetOrders(); break;
                        case 4:
                            tmp.GetOrderById(); break;
                        case 5:
                            tmp.UpdateOrder(); break;
                        case 6:
                            Console.Clear(); ShowMenu(); break;
                        case 0:
                            Environment.Exit(0); break;
                    }
                    break;

                case 0:
                    Environment.Exit(0); break;                    
            }
            SubMenu(menu);
        }
    }
}
