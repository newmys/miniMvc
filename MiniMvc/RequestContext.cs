using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Artech.MiniMvc
{
/// <summary>
/// 当前请求上下文，用于 MVC 调用链中的传递
/// </summary>
public class RequestContext
{
    public virtual HttpContextBase HttpContext { get; set; }
    public virtual RouteData RouteData { get; set; }
}
}
