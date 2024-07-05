using PerformancePressureTesting.ResourceMoniter;
using Prometheus;

namespace PerformancePressureTesting;

public class Program
{
    public static void Main(string[] args)
    {
        // 设置线程池的最小和最大线程数
        ThreadPool.SetMinThreads(100, 100);
        ThreadPool.SetMaxThreads(1000, 1000);
        // 验证配置
        ThreadPool.GetMinThreads(out int minWorkerThreads, out int minCompletionPortThreads);
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
        Console.WriteLine($"Min Worker Threads: {minWorkerThreads}, Min IO Threads: {minCompletionPortThreads}");
        Console.WriteLine($"Max Worker Threads: {maxWorkerThreads}, Max IO Threads: {maxCompletionPortThreads}");

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            // 设置 Kestrel 的最大并发连接数
            serverOptions.Limits.MaxConcurrentConnections = 10000;
            serverOptions.Limits.MaxConcurrentUpgradedConnections = 10000;

            // 设置 Keep-Alive 超时时间
            serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2); // 默认值是 2 分钟
            // 设置请求头的最大大小
            serverOptions.Limits.MaxRequestHeadersTotalSize = 32 * 1024; // 32 KB
            // 设置请求正文的最大大小
            serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
        });
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //注册资源监控
        builder.Services.AddResourceMoniter(builder.Configuration);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        //注册资源监控
        app.UseResourceMoniter();
        app.Run();
    }
}
