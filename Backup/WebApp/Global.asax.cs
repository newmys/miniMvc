using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Artech.MiniMvc;

namespace WebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // 下面注册我们项目需要匹配的路由规则。ASP.NET Route 在接收到请求后，会把请求的
            // URL 和下面我们注册的路由规则相比较（可以理解为正则表达式匹配的原理）, 最先
            // 匹配的规则（即 Route），就由该 Route 的 RouteHandler 来处理。所以注册路由
            // 很关键。
            RouteTable.Routes.Add("default_html", new RegexRoute { Url = "{controller}/{action}.html" });

            // 注意：RegexRoute 是本人扩展的，目的是替换原先的 Route 的匹配规则，以及增加一些
            // 默认值（controller 和 action）的实现。
            RouteTable.Routes.Add("default", new RegexRoute { Url = "{controller}/{action}", Defaults = new { controller = "Home", action = "Index" } });

            // 下面是设置控制器工厂，MVC 内部仅仅只有一个实现了 IControllerFactory 的工厂
            ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory());

            // 下面是给控制器工厂添加默认的命名空间，以便 MVC 在找控制器时查询速度会更快。
            ControllerBuilder.Current.DefaultNamespaces.Add("WebApp");
        }
    }
}