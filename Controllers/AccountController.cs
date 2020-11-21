using GuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuestBook.Controllers
{
    public class AccountController : Controller
    {
        GBSDBContext db = new GBSDBContext();
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.Where(a => a.Name == user.Name && a.Password == user.Password).FirstOrDefault();
                if (dbUser!=null) {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1, dbUser.UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20),
                        false, dbUser.SRole.ToString()) ;
                    //将票据加密 
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    //将加密后的票据保存为cookie 
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    authCookie.HttpOnly = true;  //客户端的js不能访问cookie
                   //使用加入了userdata的新cookie 
                    Response.Cookies.Add(authCookie);

                    if (dbUser.SRole.ToString() == "管理员")
                    {
                        return RedirectToAction("checkIndex", "Admin");
                    }
                    else if (dbUser.SRole.ToString() == "普通用户")
                    {
                       return RedirectToAction("AllWord", "User");
                    }
                }
            }
            ModelState.AddModelError("", "用户名或密码错误");
            return View(user);
        }

        public ActionResult Logout()
        {
            string encCookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value; //获取请求中附带的cookie
            FormsAuthenticationTicket c = FormsAuthentication.Decrypt(encCookie);//对cookie 解码
            string UserId = c.Name;
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1, UserId, DateTime.Now, System.DateTime.Now.AddMonths(-1),
                        false,"普通用户");
            //将票据加密 
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //将加密后的票据保存为cookie 
            System.Web.HttpCookie authCookie = new System.Web.HttpCookie
                (FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.HttpOnly = true;  //客户端的js不能访问cookie
                                         //使用加入了userdata的新cookie 
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

            return RedirectToAction("Index","Home");
        }
    }
}