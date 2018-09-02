using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 控制器工厂接口
/// </summary>
public interface IControllerFactory
{
    /// <summary>
    /// 创建控制器，根据当前上下文（路由）中匹配的控制器名称，找到控制器
    /// </summary>
    /// <param name="requestContext"></param>
    /// <param name="controllerName"></param>
    /// <returns></returns>
    IController CreateController(RequestContext requestContext, string controllerName);
}
}
