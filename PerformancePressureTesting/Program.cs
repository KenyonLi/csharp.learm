using PerformancePressureTesting.ResourceMoniter;
using Prometheus;

namespace PerformancePressureTesting;

public class Program
{
    public static void Main(string[] args)
    {
        // �����̳߳ص���С������߳���
        ThreadPool.SetMinThreads(100, 100);
        ThreadPool.SetMaxThreads(1000, 1000);
        // ��֤����
        ThreadPool.GetMinThreads(out int minWorkerThreads, out int minCompletionPortThreads);
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
        Console.WriteLine($"Min Worker Threads: {minWorkerThreads}, Min IO Threads: {minCompletionPortThreads}");
        Console.WriteLine($"Max Worker Threads: {maxWorkerThreads}, Max IO Threads: {maxCompletionPortThreads}");

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            // ���� Kestrel ����󲢷�������
            serverOptions.Limits.MaxConcurrentConnections = 10000;
            serverOptions.Limits.MaxConcurrentUpgradedConnections = 10000;

            // ���� Keep-Alive ��ʱʱ��
            serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2); // Ĭ��ֵ�� 2 ����
            // ��������ͷ������С
            serverOptions.Limits.MaxRequestHeadersTotalSize = 32 * 1024; // 32 KB
            // �����������ĵ�����С
            serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
        });
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //ע����Դ���
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
        //ע����Դ���
        app.UseResourceMoniter();
        app.Run();
    }
}
