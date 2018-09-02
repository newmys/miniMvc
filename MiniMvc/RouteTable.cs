using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 路由表，用于保存应用程序启动时，被注册的路由集合。
/// 有一个静态成员
/// </summary>
public class RouteTable
{
    public static RouteDictionary Routes { get; private set; }
    static RouteTable()
    {
        Routes = new RouteDictionary();
    }
}
}