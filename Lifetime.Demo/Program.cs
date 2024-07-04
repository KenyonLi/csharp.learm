
using Lifetime.Demo.IServices;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Exceptionless;
using Serilog;
namespace Lifetime.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Log.Logger = new LoggerConfiguration()
    .WriteTo.Exceptionless(b => b.AddTags("Serilog Example"))
    .CreateLogger();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddSingleton<ICounterService, CounterService>();  
            //作用域
            builder.Services.AddScoped<ICounterService, CounterService>();
            // builder.Services.AddTransient<ICounterService, CounterService>();  
            builder.Services.AddControllers(); // 注册控制器服务
            #region -常规注册-
            builder.Services.AddTransient<ITestTransient, TestTransient>();
            builder.Services.AddScoped<ITestScoped, TestScoped>();
            builder.Services.AddSingleton<ITestSingleton, TestSingleton>();
            #endregion

            #region 实例注入模式
            //实例注入 只有单例可以，其他不能
            builder.Services.AddSingleton(new TestSingleton());
            //builder.Services.AddScoped(new TestScoped()); //报错
            //builder.Services.AddTransient(new TestTransient());//报错
            #endregion

            #region -工厂注入模式-
            builder.Services.AddSingleton(provider => new TestSingleton());
            builder.Services.AddScoped(provider => new TestScoped());
            builder.Services.AddTransient(provider => new TestTransient());
            #endregion

            #region -两种排他注入模式-
            /*
             https://www.cnblogs.com/zoe-zyq/p/13375245.html
             */
            //排他注册1
            builder.Services.AddSingleton<IUserService, UserService>();
            //如果 IUserService 已经注册了，通过TryAdd....就不会注册成功，不管实现，只要是一个类型就不能注册
            builder.Services.TryAddSingleton<IUserService, UserServiceEx>();
            builder.Services.TryAddSingleton<IUserService, UserServiceEx>();
            builder.Services.TryAddSingleton<IUserService, UserService>();

            //排他注册2
            builder.Services.AddSingleton<IStudentService, StudentService>();
            // 只要是不同实现就能注册成功，同一个接口的相同实现就不能注册成功。
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IStudentService, StudentServiceEx>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IStudentService, StudentServiceEx>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IStudentService, StudentService>());
            #endregion

            #region -泛型模板注册-
            builder.Services.AddScoped(typeof(ITestGeneric<>), typeof(TestGeneric<>));
            #endregion
            //5JDbkHExO5gHLj2DgjFhARN21tTmHR0yjO3gUJBx
            builder.Services.AddExceptionless(optoins => {
                optoins.ApiKey = "5JDbkHExO5gHLj2DgjFhARN21tTmHR0yjO3gUJBx";
            });
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           // 使用坑：不要从根容器中获取Transient生命周期的对象，因为通过根容器创建的对象不会回收，除非等到应用程序退出，这样会导致内存泄露；如下演示：
            //app.Services.GetService<ITestTransient>();
            //app.Services.GetService<ITestTransient>();
            //app.Services.GetService<ITestTransient>();
            //app.Services.GetService<ITestTransient>();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseExceptionless();//注册管道
            app.Run();
        }
    }
}
