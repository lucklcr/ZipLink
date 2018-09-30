using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ZipLink.Models;

//Reescrever a tela de list (botões de GO, remover, habilitar e desabilitar.)
//Quando usar alguma função utilizaro redirect para a view list.
//reescrever a tela add
//retirar a lógica de list da tela GO utilizando somente para redirecionar.

namespace ZipLink.Controllers
{
    public class LinksController : Controller
    {
        public ActionResult List(string filtro)
        {
            using (Database database = new Database())
            {
                if (filtro == null)
                {
                    var links = database.Links.OrderBy(x => x.Description).ToList();
                    return View(links);
                }

                else
                {
                    var links = database.Links.Where(x => x.Description.Contains(filtro) || x.Url.Contains(filtro))
                        .OrderBy(x => x.Description).ToList();
                    return View(links);
                }
            }

        }
        public ActionResult Add(string description, string url)
        {
            using (Database database = new Database())
            {
                if (description == null || url == null)
                {
                    return View();
                }
                else
                {
                    if (IsUrlValid(url))
                    {
                        Link link = new Link();
                        link.Description = description;
                        link.Url = url;
                        link.Enabled = true;
                        link.LastVisit = null;
                        link.Visits = 0;


                        database.Links.Add(link);
                        database.SaveChanges();

                        TempData["adicionado"] = url;
                        return RedirectToAction("Add", "Links");
                    }
                    else
                    {
                        TempData["Invalido"] = url;
                        return View();
                    }

                }
            }
        }
        public ActionResult Go(string filtro, string id)
        {

            using (Database database = new Database())
            {
                var links = database.Links.OrderBy(x => x.Description).ToList();
                var links2 = database.Links.Where(x => x.Description.Contains(filtro) || x.Url.Contains(filtro))
                    .OrderBy(x => x.Description).ToList();

                if (id == null && (filtro == null || filtro == ""))
                {
                    return View(links);
                }

                else if (filtro != null)
                {
                    return View(links2);
                }

                else
                {
                    Link link = new Link();

                    link = database.Links.Find(int.Parse(id));

                    if (link == null)
                    {
                        TempData["Inexistente"] = id;
                        return View(links);
                    }
                    else
                    {
                        link.LastVisit = DateTime.Now;
                        link.Visits += 1;

                        database.SaveChanges();

                        return Redirect(database.Links.Find(int.Parse(id)).Url);
                    }
                }
            }
        }
        public ActionResult Remove(string filtro, string id)
        {
            using (Database database = new Database())
            {
                var links = database.Links.OrderBy(x => x.Description).ToList();
                var links2 = database.Links.Where(x => x.Description.Contains(filtro) || x.Url.Contains(filtro))
                    .OrderBy(x => x.Description).ToList();

                if (id == null && (filtro == null || filtro == ""))
                {

                    return View(links);
                }
                else if(filtro != null)
                {
                    return View(links2);
                }
                else
                {
                    Link link = new Link();
                    link = database.Links.Find(int.Parse(id));
                    var temp = link.Description.ToString();
                    database.Links.Remove(link);

                    database.SaveChanges();

                    TempData["Excluido"] = temp;

                    var linksNew = database.Links.OrderBy(x => x.Description).ToList();
                    return View(linksNew);
                }
            }
        }

        private bool IsUrlValid(string url)
        {

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
        
        public ActionResult Teste()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Teste(Link link)
        {
            if (ModelState.IsValid)
            {
                // add 
                return RedirectToAction("List");
            }
            else
            {
                return View(link);
            }
        }
    }
}