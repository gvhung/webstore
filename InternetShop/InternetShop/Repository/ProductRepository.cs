using Entities;

namespace Repository
{
    public class ProductRepository:BaseRepository<Product>
    {
        public override void Update(Product product)
        {
            if (product.Id == 0)
            {
                BaseRepository<Product> repository = new ProductRepository();
                repository.Create(product);
            }
            using (var context = new ShopContext())
            {
                Product dbEntry = context.Products.Find(product.Id);
                if (dbEntry!=null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Weight = product.Weight;
                    context.SaveChanges();
                }
            }
        }
    }
}
