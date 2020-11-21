using GuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuestBook.Controllers
{
    public class HomeController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
            gb = gb.FindAll(s => s.isPass == true);
            return View(gb);
        }
    }
}