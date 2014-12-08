using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BuisnessLogicLayer;
using Entities;
using WebUI.Models.ForActivation;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IBuisnessLogicLayer<Product, int> productManager;
        private IBuisnessLogicLayer<Order, int> orderManager;
        public int PageSize = 6;

        public AdminController(IBuisnessLogicLayer<Product, int> manager, IBuisnessLogicLayer<Order, int> ordManager)
        {
            productManager = manager;
            orderManager = ordManager;
        }

        public ViewResult Index()
        {
            return View(productManager.ReadAll());
        }

        public ViewResult Edit(int Id)
        {
            Product product = productManager.ReadAll().FirstOrDefault(x => x.Id == Id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productManager.Update(product);
                TempData["message"] = string.Format("{0} был успешно сохранен.", product.Name);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            productManager.Delete(Id);
            TempData["message"] = string.Format("Товар успешно удален.");
            return RedirectToAction("Index");
        }


        public ViewResult UserList()
        {
            var userNames = Roles.GetUsersInRole(SiteRole.Client.ToString());
            var clientUsers = new List<UserProfile>();
            using (var dbContext = new UserContext())
            {
                foreach (string userName in userNames)
                {
                    clientUsers.Add(dbContext.UserProfiles.FirstOrDefault(u => u.UserName == userName));
                }
            }
            return View(clientUsers);
        }


        //получение списка заказов
        public ActionResult OrderList()
        {
            return View(GetOrderList(OrderStatus.New.ToString()));
        }

        public ActionResult PrecessedOrderList()
        {
            return View(GetOrderList(OrderStatus.Processed.ToString()));
        }

        public ActionResult ProcessedOrder(List<OrderSummary> orders, int Id)
        {
            var order = orderManager.ReadAll().FirstOrDefault(x => x.Id == Id);
            if (ModelState.IsValid)
            {
                orderManager.Update(order);
                TempData["message"] = string.Format("{0} заказ был успешно обработан.", order.Id);
                return RedirectToAction("OrderList");
            }
            return RedirectToAction("OrderList");
        }

        
        private List<OrderSummary> GetOrderList(string statusString)
        {
            List<OrderSummary> orderList = new List<OrderSummary>();
            var orders = orderManager.ReadAll().Where(x => x.Status == statusString);
            foreach (Order order in orders)
            {
                OrderSummary orderSummary = new OrderSummary();
                orderSummary.OrderId = order.Id;
                orderSummary.OrderDate = order.OrderDate;
                using (var dbContext = new UserContext())
                {
                    orderSummary.UserProfile = dbContext.UserProfiles.FirstOrDefault(u => u.UserName == order.ClientLogin);
                }
                foreach (OrderProduct product in order.OrderProduct)
                {
                    orderSummary.Products.Add(productManager.ReadAll().FirstOrDefault(x => x.Id == product.ProductId), product.Quantity);
                }
                orderList.Add(orderSummary);
            }
            return orderList;
        }


        public ActionResult GetUser(int userid)
        {
            UserProfile profile = new UserProfile();
            using (var dbContext = new UserContext())
            {
                profile = dbContext.UserProfiles.FirstOrDefault(u => u.UserId == userid);
            }
            return View(profile);
        }
    }
}
