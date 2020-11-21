using GuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBook.Controllers
{
    [Authorize(Roles="管理员")]
    public class AdminController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        public ActionResult checkIndex(int type = 0)
        {
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
            if (type == 0)
            {
                gb = gb.FindAll(s => s.isPass == false);
            }
            ViewData["type"] = type;
            return View(gb);
        }
        public ActionResult CheckMessage(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }
        [HttpPost,ActionName("CheckMessage")]
        public ActionResult CheckMessage1(int id)
        {
            var gb = db.Guestbooks.Find(id);
            gb.isPass = true;
            db.SaveChanges();
            return RedirectToAction("CheckIndex", new { target = "fc" });
        }
    }
}