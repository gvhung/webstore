using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebUI.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public PartialViewResult Now()
        {
            DateTime date = DateTime.Now;
            return PartialView(date);
        }


        public PartialViewResult Old()
        {
            DateTime date = new DateTime(2014, 09, 01);
            return PartialView(date);
        }


        public PartialViewResult Map()
        {
            string str = "//api-maps.yandex.ru/services/constructor/1.0/js/?sid=sU7LwP7TjS3GeD3TSOyt8DJbXweeAhl1&width=600&height=450";
            return PartialView(str);
        }


    }
}
