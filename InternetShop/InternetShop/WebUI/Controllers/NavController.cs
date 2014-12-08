using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BuisnessLogicLayer;
using Entities;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IBuisnessLogicLayer<Product, int> productManager;

        public NavController(IBuisnessLogicLayer<Product, int> manager)
        {
            productManager = manager;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = productManager.ReadAll()
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}
