using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities;

namespace Repository
{
    public class OrderRepository: BaseRepository<Order>
    {
        public override Order Read(int id)
        {
            using (var context = new ShopContext())
            {
                return (from x in context.Orders
                    .Include("OrderProduct")
                    select x).FirstOrDefaultAsync(x => x.Id == id).Result;
            }
        }

        public override IEnumerable<Order> ReadAll()
        {
            using (var context = new ShopContext())
            {
                return (from x in context.Orders
                    .Include("OrderProduct")
                        select x).ToListAsync().Result;
            }
        }

        public override void Update(Order entity)
        {
            using (var context = new ShopContext())
            {
                Order order = context.Orders.Find(entity.Id);
                if (order != null)
                {
                    order.Status = "Processed";
                    context.SaveChanges();
                }
            }
        }
    }
}
