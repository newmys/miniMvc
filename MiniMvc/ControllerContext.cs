using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 控制器上下文，用于在调用链中传递。
/// 比如传递给 ActionInvoker。
/// </summary>
public class ControllerContext
{
    public ControllerBase Controller { get; set; }
    public RequestContext RequestContext { get; set; }
}
}
