using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;
namespace GuestBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthorizeRequest(object sender, System.EventArgs e)
        {
            HttpApplication App = (HttpApplication)sender;
            HttpContext Ctx = App.Context; //获取本次Http请求相关的HttpContext对象
            if (Ctx.Request.IsAuthenticated == true) //验证过的用户才进行role的处理
            {
                FormsIdentity Id = (FormsIdentity)Ctx.User.Identity;
                FormsAuthenticationTicket Ticket = Id.Ticket; //取得身份验证票
                string[] Roles = Ticket.UserData.Split(','); //将身份验证票中的role数据转成字符串数组
                Ctx.User = new GenericPrincipal(Id, Roles); //将原有的Identity加上角色信息新建一个GenericPrincipal表示当前用户,这样当前用户就拥有了role信息
            }
        }
    }
}
