using Microsoft.Extensions.Options;
using Prometheus;

namespace PerformancePressureTesting.ResourceMoniter;

/// <summary>
/// 资源监控
/// </summary>
public class ResourceMoniters
{
    private readonly Counter _requestCounter;
    public ResourceMoniters(IOptions<ResourceMonitersOptions> options)
    {
        //1、获取options
        ResourceMonitersOptions monitersOptions = options.Value;

        /// <summary>
        ///1、先创建Counter
        /// </summary>
        _requestCounter = Metrics.CreateCounter(monitersOptions.CounterName, monitersOptions.CounterHelp, new CounterConfiguration
        {
            LabelNames = monitersOptions.LabelNames
        });
    }

    /// <summary>
    /// 监控方法
    /// </summary>
    public void Moniter(string method,string endpoint)
    {
        _requestCounter.WithLabels(method, endpoint).Inc();
    }
}
