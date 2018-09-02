using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Artech.MiniMvc
{
/// <summary>
/// 默认的 Action 调用器的实现
/// </summary>
public class ControllerActionInvoker : IActionInvoker
{
    /// <summary>
    /// 模型绑定实现
    /// </summary>
    public IModelBinder ModelBinder { get; private set; }

    public ControllerActionInvoker()
    {
        this.ModelBinder = new DefaultModelBinder();
    }
    public void InvokeAction(ControllerContext controllerContext, string actionName)
    {
        // 下面是根据当前路由中的 Action 名字，反射当前获取的 Controller 的类型，并找到 Action Method
        MethodInfo method = controllerContext.Controller.GetType().GetMethods().First(m => string.Compare(actionName, m.Name, true) == 0);
        List<object> parameters = new List<object>();
        foreach (ParameterInfo parameter in method.GetParameters())
        {
            parameters.Add(this.ModelBinder.BindModel(controllerContext, parameter.Name, parameter.ParameterType));
        }
        ActionResult actionResult = method.Invoke(controllerContext.Controller, parameters.ToArray()) as ActionResult;
        if (actionResult != null)
        {
            actionResult.ExecuteResult(controllerContext);
        }
    }
}
}
