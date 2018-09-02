using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 抽象的 Action 返回的结果
/// </summary>
public abstract class ActionResult
{        
    public abstract void ExecuteResult(ControllerContext context);
}
}
