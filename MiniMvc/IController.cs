using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 控制器接口
/// </summary>
public interface IController
{
    void Execute(RequestContext requestContext);
}
}
