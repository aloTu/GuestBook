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
        public ActionResult AllWord(string type = "全部")
        {
            string encCookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value; //获取请求中附带的cookie
            FormsAuthenticationTicket c = FormsAuthentication.Decrypt(encCookie);//对cookie 解码
            int UserId = int.Parse(c.Name); // c.Name 记录了 UserID  c.UserData记录了用户类型用以验证
            var gb = db.Guestbooks.OrderByDescending(g => g.CreatedOn).ToList();
            string msgNumbers = gb.FindAll(s => s.isPass == false).Count.ToString();
            if (type == "我的" )
            {
                gb = gb.FindAll(s => s.UserId == UserId);
            }
            if(type=="待处理" && c.UserData=="管理员")
            {
                gb = gb.FindAll(s => s.isPass == false);
            }else if (type == "我的")
            {
                gb = gb.FindAll(s => s.isPass == true && s.UserId == UserId);
            }
            else
            {
                gb = gb.FindAll(s => s.isPass == true);
            }
            ViewData["msg-numbers"] = msgNumbers;
            ViewData["SRole"] = c.UserData;//将 用户类型传入
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
            string encCookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value; //获取请求中附带的cookie
            FormsAuthenticationTicket c = FormsAuthentication.Decrypt(encCookie);//对cookie 解码
            string SRole = c.UserData;//从cookie中获取用户类型

            if (ModelState.IsValid)
            {
                gb.UserId = int.Parse(c.Name);
                gb.isPass = SRole == "管理员";//管理员默认通过审核
                db.Guestbooks.Add(gb);
                db.SaveChanges();
                return RedirectToAction("AllWord");
            }
            return View();
        }
        [Authorize]
        public ActionResult delete(int id)
        {
            string encCookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value; //获取请求中附带的cookie
            FormsAuthenticationTicket c = FormsAuthentication.Decrypt(encCookie);//对cookie 解码
            int userId = int.Parse(c.Name);
            var gb = db.Guestbooks.Find(id);

            if (userId == gb.UserId || c.UserData == "管理员")
            {
                db.Guestbooks.Remove(gb);
                db.SaveChanges();
                return RedirectToAction("AllWord");
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles ="管理员")]
        [HttpGet]
        public ActionResult checkMessage(int id)
        {
            var gb = db.Guestbooks.Find(id);
            gb.isPass = true;
            db.SaveChanges();
            return RedirectToAction("AllWord");
        }
    }
}