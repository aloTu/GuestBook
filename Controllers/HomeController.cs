using GuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBook.Controllers
{
    public class HomeController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("AllWord", "User");
            }
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
            gb = gb.FindAll(s => s.isPass == true);
            return View(gb);
        }
    }
}