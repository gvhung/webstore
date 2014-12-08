using System.Collections.Generic;
using Entities;
using Repository;

namespace BuisnessLogicLayer
{
    public class ProductManager : IBuisnessLogicLayer<Product, int>
    {
        private ProductRepository productRepository;

        public ProductManager()
        {
            productRepository = new ProductRepository();
        }

        public Product Read(int Id)
        {
            return productRepository.Read(Id);
        }

        public IEnumerable<Product> ReadAll()
        {
            return productRepository.ReadAll();
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
        }

        public void Delete(int Id)
        {
            productRepository.Delete(Id);
        }

        public void Create(Product entity)
        {
            return;
            //productRepository.Create(entity);
        }
    }
}