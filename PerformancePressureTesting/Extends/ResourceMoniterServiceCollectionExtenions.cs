using PerformancePressureTesting.StaticServiceLocator;
namespace PerformancePressureTesting.ResourceMoniter;

/// <summary>
/// ResourceMoniter扩展 
/// </summary>
public static class ResourceMoniterServiceCollectionExtenions
{
    /// <summary>
    /// ResourceMoniter 扩展方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IServiceCollection AddResourceMoniter(this IServiceCollection services,IConfiguration configuration)
    {
        //10、注册资源监控
        services.AddSingleton<ResourceMoniters>();
        // 10.1、注册ResourceMonitersOptions (获取值，然后注册 到IOC容器)
        services.Configure<ResourceMonitersOptions>(configuration.GetSection("ResourceMoniter"));
        // 配置服务
        MyServiceLocator.ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}
