using System;
using BuisnessLogicLayer;
using Entities;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var productManager = new ProductManager();

            //productManager.Add(new Product
            //{
            //    Name = "name",
            //    Category = "cat",
            //    Description = "desc",
            //    Price = 10,
            //    Weight = null
            //});

            foreach (Product product in productManager.ReadAll())
            {
                Console.WriteLine("Product: {0}", product.Name);
            }
        }
    }
}
