using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel;

namespace Artech.MiniMvc
{
    /// <summary>
    /// 一个 Key-Value 集合，Key 表示路由名称，Value 表
    /// 示路由（包含 URL Pattern、路由处理程序等等）。
    /// 可以简单的理解它用于保存 Global.asax 中注册的值。
    /// </summary>
    public class RouteDictionary: Dictionary<string, RouteBase>
    {
        

        /// <summary>
        /// 下面是从当前 HttpContextBase 中获取 URL，把这个 URL 和应用程序启动（
        /// Application_Start ）中注册的静态路由表（RouteTable）相比较（也称匹配），
        /// 第一个匹配的 RouteData 就立即返回，如果都没有匹配，则返回 NULL。
        /// </summary>
        /// <param name="httpContext">当前请求上下文</param>
        /// <returns></returns>
        public RouteData GetRouteData(HttpContextBase httpContext)
        {
            foreach (var route in this.Values)
            {
                RouteData routeData = route.GetRouteData(httpContext);
                if (null != routeData)
                {
                    return routeData;
                }
            }
            return null;
        }
    }
}
