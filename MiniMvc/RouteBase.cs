using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Artech.MiniMvc
{
/// <summary>
/// 路由抽象类
/// </summary>
public abstract class RouteBase
{
    public abstract RouteData GetRouteData(HttpContextBase httpContext);
}
}
