using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;


namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
         
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new department name");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }


            var productRepo = new DapperProductRepository(conn);

            Console.WriteLine("Type a new product name");

            var newProductName = Console.ReadLine();

            Console.WriteLine("Type the new product's price");

            var newProductPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("What is the new product's category ID?");

            var newCategoryID = int.Parse(Console.ReadLine());

            productRepo.CreateProduct(newProductName, newProductPrice, newCategoryID);

            var productList = productRepo.GetAllProducts();

            foreach(var product in productList)
            {
                Console.WriteLine(product.Name);
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.CategoryID);
                Console.WriteLine(product.OnSale);
                Console.WriteLine(product.StockLevel);
            }


        }
    }
}
