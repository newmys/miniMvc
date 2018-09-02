using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artech.MiniMvc
{
public class ControllerBuilder
{
    // 静态字段。该字段在下面的静态构造函数中实例化
    public static ControllerBuilder Current { get; private set; }

    private Func<IControllerFactory> factoryThunk;

    public HashSet<string> DefaultNamespaces { get; private set; }

    static ControllerBuilder()
    {
        Current = new ControllerBuilder();
    }

    public ControllerBuilder()
    {
        this.DefaultNamespaces = new HashSet<string>();
    }
    
    public IControllerFactory GetControllerFactory()
    {
        return factoryThunk();
    }

    public void SetControllerFactory(IControllerFactory controllerFactory)
    {
        factoryThunk = () => controllerFactory;
    }
    
}
}
