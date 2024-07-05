using Microsoft.AspNetCore.Mvc.Filters;
using PerformancePressureTesting.StaticServiceLocator;

namespace PerformancePressureTesting.ResourceMoniter;


/// <summary>
/// 资源监控特性
/// </summary>
//[Dependency(ServiceLifetime.Singleton)]
public class ResourceMonitersAttribute : ActionFilterAttribute
{
    /// <summary>
    /// 1、资源监控
    /// </summary>
    private readonly ResourceMoniters resourceMoniters;

    public ResourceMonitersAttribute()
    {
        var provider = MyServiceLocator.ServiceProvider;
        resourceMoniters = provider.GetService<ResourceMoniters>();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 2、调用资源监控
        var method = context.HttpContext.Request.Method;
        var endpoin = context.HttpContext.Request.Path.Value;
        resourceMoniters.Moniter(method, endpoin);
        base.OnActionExecuting(context);
    }
}
