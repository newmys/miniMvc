using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
/// <summary>
/// 路由数据，一般用于保存当前 HttpContext 下
/// 获取的数据。
/// </summary>
public class RouteData
{
    public IDictionary<string, object> Values { get; private set; }
    public IDictionary<string, object> DataTokens { get; private set; }
    public IRouteHandler RouteHandler { get;  set; }
    public RouteBase Route { get; set; }

    public RouteData()
    {
        this.Values = new Dictionary<string, object>();
        this.DataTokens = new Dictionary<string, object>();
        this.DataTokens.Add("namespaces", new List<string>());
    }
    /// <summary>
    /// 获取当前匹配的 Controller
    /// </summary>
    public string Controller
    {
        get
        {
            object controllerName = string.Empty;
            this.Values.TryGetValue("controller", out controllerName);
            return controllerName.ToString();
        }
    }

    /// <summary>
    /// 获取当前匹配的 Action
    /// </summary>
    public string ActionName
    {
        get
        {
            object actionName = string.Empty;
            this.Values.TryGetValue("action", out actionName);
            return actionName.ToString();
        }
    } 
    public IEnumerable<string> Namespaces
    {
        get
        {
            return (IEnumerable<string>)this.DataTokens["namespaces"];
        }
    } 
}
}
