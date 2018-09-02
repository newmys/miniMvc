using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Artech.MiniMvc
{
/// <summary>
/// Url 路由 Module
/// </summary>
public class UrlRoutingModule : IHttpModule
{
    public void Dispose()
    { 
        
    }
    public void Init(HttpApplication context)
    {
        context.PostResolveRequestCache += OnPostResolveRequestCache;
        // 关于这个事件，官方摘要如下：
        // 在 ASP.NET 跳过当前事件处理程序的执行并允许缓存模块满足来自缓存的请求时发生。
    }
    protected virtual void OnPostResolveRequestCache(object sender, EventArgs e)
    {
        // 下面是得到当前请求的 Http 上下文
        HttpContext currentHttpContext = ((HttpApplication)(sender)).Context; 
        
        // 下面是一个包装类，把 HttpContext 转换成 HttpContextBase。如果你问我为什么
        // HttpApplication.Context 返回的是 HttpContext，而不是 HttpContextBase，我
        // 只能说一句：历史遗留问题
        HttpContextWrapper httpContext = new HttpContextWrapper(currentHttpContext);

        // 下面是从当前 HttpContextBase 中获取 URL，把这个 URL 和应用程序启动（
        // Application_Start ）中注册的静态路由表（RouteTable）相比较（也称匹配），
        // 第一个匹配的 RouteData 就立即返回，如果都没有匹配，则返回 NULL。
        RouteData routeData = RouteTable.Routes.GetRouteData(httpContext);
        if (null == routeData)
        {
            return;
        }
        RequestContext requestContext = new RequestContext 
        { 
            RouteData = routeData, 
            HttpContext = httpContext 
        };
        IHttpHandler handler = routeData.RouteHandler.GetHttpHandler(requestContext);
        httpContext.RemapHandler(handler); // 用于为请求指定处理程序。
    }
}
}