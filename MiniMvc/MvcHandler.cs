using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Artech.MiniMvc
{
/// <summary>
/// Mvc 处理程序
/// </summary>
public class MvcHandler: IHttpHandler
{
    public bool IsReusable
    {
        get{return false;}
    }
    public RequestContext RequestContext { get; private set; }
    public MvcHandler(RequestContext requestContext)
    {
        this.RequestContext = requestContext;
    }
    public void ProcessRequest(HttpContext context)
    {
        // 下面是从当前请求上下文中获取控制器的名称
        string controllerName = this.RequestContext.RouteData.Controller;
        // 下面是得到 MVC 注册的控制器工厂
        IControllerFactory controllerFactory = ControllerBuilder.Current.GetControllerFactory();
        // 下面是由控制器工厂生产出控制器
        IController controller = controllerFactory.CreateController(this.RequestContext, controllerName);
        // 执行控制器，以及控制器里面的 Action
        controller.Execute(this.RequestContext);
    }
}
}
