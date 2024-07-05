using Microsoft.AspNetCore.Builder;
using Prometheus;

namespace PerformancePressureTesting.ResourceMoniter;

/// <summary>
/// ResourceMoniter 扩展 
/// </summary>
public static class ResourceMoniterApplicationBuilderExtenions
{
    /// <summary>
    /// ResourceMoniter 扩展方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IApplicationBuilder UseResourceMoniter(this IApplicationBuilder builder)
    {
        // 3、集成prometheus-net.AspNetCore
        builder.UseMetricServer(); // 集成
        builder.UseHttpMetrics();//访问监控的数据。
        return builder;
    }
}
