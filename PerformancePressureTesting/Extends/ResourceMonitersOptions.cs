namespace PerformancePressureTesting.ResourceMoniter;

/// <summary>
/// 资源监控Options
/// </summary>
public class ResourceMonitersOptions
{
    public string CounterName { set; get; }
    public string CounterHelp { set; get; }
    public string[] LabelNames { set; get; }
}
