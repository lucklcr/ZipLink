using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipLink.Models;

namespace ZipLink.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();




            //using (Database database = new Database())
            //    {
            //    Link link = new Link();
            //    link.Description = "teste";
            //    link.Enabled = true;
            //    link.LastVisit = null;
            //    link.Url = "https://google.com";
            //    link.Visits = 0;

            //    database.Links.Add(link);
            //    database.SaveChanges();

            //    int id = link.Id;
            //}

            //    return View();
        }
    }
}