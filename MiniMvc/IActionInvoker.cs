using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 定义调用 Action 的规则，可以理解为 Action 调用器的接口
/// </summary>
public interface IActionInvoker
{
    void InvokeAction(ControllerContext controllerContext, string actionName);
}
}