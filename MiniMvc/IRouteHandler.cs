using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Artech.MiniMvc
{
/// <summary>
/// 路由处理程序的接口
/// </summary>
public interface IRouteHandler
{
    IHttpHandler GetHttpHandler(RequestContext requestContext);
}
}