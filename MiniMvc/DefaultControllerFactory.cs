using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Compilation;

namespace Artech.MiniMvc
{
/// <summary>
/// 默认的控制器工厂。顾名思义，工厂是用来生产物品的，
/// 对应在编程中，就是生成控制器的。
/// </summary>
public class DefaultControllerFactory : IControllerFactory
{
    private List<Type> controllerTypes = new List<Type>();
    public DefaultControllerFactory()
    {
        // 下面是在当前应用下所有引用的程序集中找到 IController 的实现类
        var allAssemblies = BuildManager.GetReferencedAssemblies();
        foreach (Assembly assembly in allAssemblies)
        {
            foreach (Type type in assembly.GetTypes().Where(type => typeof(IController).IsAssignableFrom(type)))
            {
                controllerTypes.Add(type);
            }
        }
    }
    public IController CreateController(RequestContext requestContext, string controllerName)
    {
        string typeName = controllerName + "Controller";
        List<string> namespaces = new List<string>();
        namespaces.AddRange(requestContext.RouteData.Namespaces);
        namespaces.AddRange(ControllerBuilder.Current.DefaultNamespaces);
        foreach (var ns in namespaces)
        {
            string controllerTypeName = string.Format("{0}.{1}", ns, typeName);
            Type controllerType = controllerTypes.FirstOrDefault(type => string.Compare(type.FullName, controllerTypeName, true) == 0);
            if (null != controllerType)
            {
                return (IController)Activator.CreateInstance(controllerType); 
            }
        }            
        return null;
    }
}
}