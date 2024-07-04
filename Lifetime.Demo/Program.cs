
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
            //������
            builder.Services.AddScoped<ICounterService, CounterService>();
            // builder.Services.AddTransient<ICounterService, CounterService>();  
            builder.Services.AddControllers(); // ע�����������
            #region -����ע��-
            builder.Services.AddTransient<ITestTransient, TestTransient>();
            builder.Services.AddScoped<ITestScoped, TestScoped>();
            builder.Services.AddSingleton<ITestSingleton, TestSingleton>();
            #endregion

            #region ʵ��ע��ģʽ
            //ʵ��ע�� ֻ�е������ԣ���������
            builder.Services.AddSingleton(new TestSingleton());
            //builder.Services.AddScoped(new TestScoped()); //����
            //builder.Services.AddTransient(new TestTransient());//����
            #endregion

            #region -����ע��ģʽ-
            builder.Services.AddSingleton(provider => new TestSingleton());
            builder.Services.AddScoped(provider => new TestScoped());
            builder.Services.AddTransient(provider => new TestTransient());
            #endregion

            #region -��������ע��ģʽ-
            /*
             https://www.cnblogs.com/zoe-zyq/p/13375245.html
             */
            //����ע��1
            builder.Services.AddSingleton<IUserService, UserService>();
            //��� IUserService �Ѿ�ע���ˣ�ͨ��TryAdd....�Ͳ���ע��ɹ�������ʵ�֣�ֻҪ��һ�����;Ͳ���ע��
            builder.Services.TryAddSingleton<IUserService, UserServiceEx>();
            builder.Services.TryAddSingleton<IUserService, UserServiceEx>();
            builder.Services.TryAddSingleton<IUserService, UserService>();

            //����ע��2
            builder.Services.AddSingleton<IStudentService, StudentService>();
            // ֻҪ�ǲ�ͬʵ�־���ע��ɹ���ͬһ���ӿڵ���ͬʵ�־Ͳ���ע��ɹ���
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IStudentService, StudentServiceEx>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IStudentService, StudentServiceEx>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IStudentService, StudentService>());
            #endregion

            #region -����ģ��ע��-
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
           // ʹ�ÿӣ���Ҫ�Ӹ������л�ȡTransient�������ڵĶ�����Ϊͨ�������������Ķ��󲻻���գ����ǵȵ�Ӧ�ó����˳��������ᵼ���ڴ�й¶��������ʾ��
            //app.Services.GetService<ITestTransient>();
            //app.Services.GetService<ITestTransient>();
            //app.Services.GetService<ITestTransient>();
            //app.Services.GetService<ITestTransient>();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseExceptionless();//ע��ܵ�
            app.Run();
        }
    }
}
