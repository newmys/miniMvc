using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 模型绑定规则，当准备开始调用 Action 时，
/// 就会触发，即在 ControllerActionInvoker 中。
/// </summary>
public interface IModelBinder
{
    object BindModel(ControllerContext controllerContext, string modelName, Type modelType);
}
}
