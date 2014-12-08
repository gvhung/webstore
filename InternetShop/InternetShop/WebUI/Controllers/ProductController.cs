using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using BinaryAnalysis.UnidecodeSharp;
using BuisnessLogicLayer;
using Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IBuisnessLogicLayer<Product, int> productManager;
        public int PageSize = 6;

        public ProductController(IBuisnessLogicLayer<Product, int> manager)
        {
            productManager = manager;
        }

        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult List(string category, int page = 1)
        {
            Thread.Sleep(1000);
            var model = new ProductListViewModel
            {
                Products =
                    ((List<Product>) productManager.ReadAll()).Where(p => category == null || p.Category.Unidecode() == category)
                        .OrderBy(p => p.Price)
                        .Skip((page - 1)*PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = ((List<Product>)productManager.ReadAll()).Count(p => category == null || p.Category.Unidecode() == category)
                },
                CurrentCategory = category
            };
            return PartialView(model);
        }

        public ViewResult ViewProduct(int Id)
        {
            return View(productManager.ReadAll().FirstOrDefault(p => p.Id == Id));
        }
    }
}
