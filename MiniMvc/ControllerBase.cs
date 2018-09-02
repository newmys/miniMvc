using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 控制器基类
/// </summary>
public abstract class ControllerBase: IController
{
    protected IActionInvoker ActionInvoker { get; set; }
    public ControllerBase()
    {
        this.ActionInvoker = new ControllerActionInvoker();
    }
    public void Execute(RequestContext requestContext)
    {
        ControllerContext context = new ControllerContext { RequestContext = requestContext, Controller = this };
        string actionName = requestContext.RouteData.ActionName;
        // 下面是激活 Action，准备开始调用 Action
        this.ActionInvoker.InvokeAction(context, actionName);
    }
}
}
