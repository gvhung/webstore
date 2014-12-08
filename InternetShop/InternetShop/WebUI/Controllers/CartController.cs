using System;
using System.Linq;
using System.Web.Mvc;
using BuisnessLogicLayer;
using Entities;
using WebMatrix.WebData;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IBuisnessLogicLayer<Product, int> productManager;
        private IBuisnessLogicLayer<Order, int> orderManager;

        public CartController(IBuisnessLogicLayer<Product, int> prodManager, IBuisnessLogicLayer<Order, int> ordManager)
        {
            productManager = prodManager;
            orderManager = ordManager;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Product product = productManager.ReadAll().FirstOrDefault(p => p.Id == Id);
            if (product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Product product = productManager.ReadAll().FirstOrDefault(p => p.Id == Id);
            if (product!=null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult ClearCart(Cart cart, string returnUrl)
        {
            cart.Clear();
            return RedirectToAction("Index", new {returnUrl});
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        //добавление заказа в базу
        [HttpPost]
        [Authorize(Roles = "Client")]
        public ViewResult Index(Cart cart)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                orderManager.Create(new Order
                {
                    ClientLogin = WebSecurity.CurrentUserName,
                    OrderProduct = cart.Lines.Select(cartLine => new OrderProduct {ProductId = cartLine.Product.Id, Quantity = cartLine.Quantity}).ToList(),
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.New.ToString()
                });
                cart.Clear();
                return View("Completed");
            }
            return View("Index");
        }
    }
}
