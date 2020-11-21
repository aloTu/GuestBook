using GuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBook.Controllers
{
    public class UserController : Controller
    {
        GBSDBContext db = new GBSDBContext();

        // GET: User
        [Authorize]
        public ActionResult AllWord(int type = 1)
        {
            string authCookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value; //获取请求中附带的cookie
            FormsAuthenticationTicket c = FormsAuthentication.Decrypt(authCookie);//对cookie 解码
            int UserId = int.Parse(c.Name); // c.Name 记录了 UserID  c.UserData记录了用户类型用以验证
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
            gb = gb.FindAll(s => s.isPass == true);
            if (type == 0)
            {
                gb = gb.FindAll(s => s.UserId == UserId);
            }
            ViewData["SRole"] = c.UserData;
            ViewData["type"] = type;
            return View(gb);
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guestbook gb)
        {
            if (ModelState.IsValid)
            {
                gb.UserId = (int)Session["UserId"];
                gb.isPass = false;
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("AllWord");
            }
            return View();
        }
        [Authorize]
        public ActionResult delete(int id)
        {
            var gb = db.Guestbooks.Find(id);
            return View(gb);
        }
        [HttpPost,ActionName("delete")]
        public ActionResult deleteConfirmed(int id)
        {
            var gb = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("AllWord");
        }
    }
}