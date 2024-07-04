using Exceptionless;
using Exceptionless.Models;
using Lifetime.Demo.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Lifetime.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ITestGeneric<object> _test;
        public TestController(ITestGeneric<object> test)
        {

            _test = test;
        }
        [HttpGet("Get")]
        public void Get(
            [FromServices] ITestScoped testScoped1,
            [FromServices] ITestScoped testScoped2,
            [FromServices] ITestTransient testTransient1,
            [FromServices] ITestTransient testTransient2,
            [FromServices] ITestSingleton testSingleton1,
            [FromServices] ITestSingleton testSingleton2
            )
        {
            testScoped1.Show();
            testScoped2.Show();
            Console.WriteLine("--------------Begin--------------------");
            Console.WriteLine($"testScoped1 HashCode:{testScoped1.GetHashCode()},testScoped2 HashCode:{testScoped2.GetHashCode()}");
            Console.WriteLine($"testTransient1 HashCode:{testTransient1.GetHashCode()},testTransient2 HashCode:{testTransient2.GetHashCode()}");
            Console.WriteLine($"testSingleton1 HashCode:{testSingleton1.GetHashCode()},testSingleton2 HashCode:{testSingleton2.GetHashCode()}");
            Console.WriteLine("--------------End--------------------");

        }
        [HttpGet("GetServices")]
        public string GetServices(
            [FromServices] IEnumerable<IUserService> userServices,
            [FromServices] IEnumerable<IStudentService> studentServices
            )
        {
            return "zoe";
        }

        [HttpGet("GetGeneric")]
        public string GetGeneric(
           [FromServices] IEnumerable<ITestGeneric<string>> testGeneric
           )
        {
            return "GetGeneric";
        }

        [HttpGet("TestScopedEx")]
        public string TestScopedEx(
                  [FromServices] ITestSingleton testSingleton,
                  [FromServices] ITestSingleton testSingleton1,
                  [FromServices] ITestScoped testScoped,
                  [FromServices] ITestScoped testScoped1,
                  [FromServices] ITestTransient testTransient,
                  [FromServices] ITestTransient testTransient1)
        {
            //获取请求作用域(请求容器)
            var requestServices = HttpContext.RequestServices;
            //在请求作用域下创建子作用域
            using (IServiceScope scope = requestServices.CreateScope())
            {
                ////在子作用域中通过其容器获取注入的不同生命周期对象
                //var _testScope = scope.ServiceProvider.GetService<ITestScoped>();
                //var _testTransient = scope.ServiceProvider.GetService<ITestTransient>();
                //Console.WriteLine($"_testScope:{_testScope.GetHashCode()},testScoped:{testScoped.GetHashCode()},requestServices:{requestServices.GetService<ITestScoped>().GetHashCode()}");
                //Console.WriteLine($"_testTransient:{_testTransient.GetHashCode()},testTransient:{requestServices.GetService<ITestTransient>().GetHashCode()}");


                //在子作用域中通过其容器获取注入的不同生命周期对象
                ITestSingleton testSingleton11 = scope.ServiceProvider.GetService<ITestSingleton>();
                ITestScoped testScoped11 = scope.ServiceProvider.GetService<ITestScoped>();
                ITestTransient testTransient11 = scope.ServiceProvider.GetService<ITestTransient>();
                ITestSingleton testSingleton12 = scope.ServiceProvider.GetService<ITestSingleton>();
                ITestScoped testScoped12 = scope.ServiceProvider.GetService<ITestScoped>();
                ITestTransient testTransient12 = scope.ServiceProvider.GetService<ITestTransient>();
                Console.WriteLine("================Singleton=============");
                Console.WriteLine($"请求作用域的ITestSingleton对象:{testSingleton.GetHashCode()}");
                Console.WriteLine($"请求作用域的ITestSingleton1对象:{testSingleton1.GetHashCode()}");
                Console.WriteLine($"请求作用域下子作用域的ITestSingleton11对象:{testSingleton11.GetHashCode()}");
                Console.WriteLine($"请求作用域下子作用域的ITestSingleton12对象:{testSingleton12.GetHashCode()}");
                Console.WriteLine("================Scoped=============");
                Console.WriteLine($"请求作用域的ITestScoped对象:{testScoped.GetHashCode()}");
                Console.WriteLine($"请求作用域的ITestScoped1对象:{testScoped1.GetHashCode()}");
                Console.WriteLine($"请求作用域下子作用域的ITestScoped11对象:{testScoped11.GetHashCode()}");
                Console.WriteLine($"请求作用域下子作用域的ITestScoped12对象:{testScoped12.GetHashCode()}");
                Console.WriteLine("================Transient=============");
                Console.WriteLine($"请求作用域的ITestTransient对象:{testTransient.GetHashCode()}");
                Console.WriteLine($"请求作用域的ITestTransient1对象:{testTransient1.GetHashCode()}");
                Console.WriteLine($"请求作用域下子作用域的ITestTransient11对象:{testTransient11.GetHashCode()}");
                Console.WriteLine($"请求作用域下子作用域的ITestTransient12对象:{testTransient12.GetHashCode()}");
            }
            return "TestScoped";
        }

        [HttpGet("GetTestSingleton")]
        public string TestSingleton([FromServices] ITestSingleton testSingleton,
                [FromServices] IHostApplicationLifetime hostApplicationLifetime,
                [FromQuery] bool close)
        {
            if (close)
            {
                //退出引用程序
                hostApplicationLifetime.StopApplication();
            }
            Console.WriteLine("======ITestSingleton 请求结束=========");

            return "ITestSingleton";
        }
        [HttpGet("testEx")]
        public string TestEx()
        {
            using IServiceScope scope = HttpContext.RequestServices.CreateScope();
            ITestSingleton testSingleton = scope.ServiceProvider.GetService<ITestSingleton>();
            ITestScoped testScoped = scope.ServiceProvider.GetService<ITestScoped>();
            ITestTransient testTransient = scope.ServiceProvider.GetService<ITestTransient>();
            Console.WriteLine("==========TestEx结束==========");
            return "ex";
        }

        [HttpGet("TestApplicationSerices")]
        public string TestApplicationSerices([FromServices] IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<ITestTransient>();
            app.ApplicationServices.GetService<ITestTransient>();
            app.ApplicationServices.GetService<ITestTransient>();
            app.ApplicationServices.GetService<ITestTransient>();
            app.ApplicationServices.GetService<ITestTransient>();
            return "TestApplicationSerices";
        }

        [HttpGet("Exceptionless")]
        public string GetExceptionless()
        {
            // Submit logs
            ExceptionlessClient.Default.SubmitLog("lkn Logging made easy");

            // You can also specify the log source and log level.
            // We recommend specifying one of the following log levels: Trace, Debug, Info, Warn, Error
            ExceptionlessClient.Default.SubmitLog(typeof(TestController).FullName, "k This is so easy", "Info");
            ExceptionlessClient.Default.CreateLog(typeof(TestController).FullName, "This is so easy", "Info").AddTags("Exceptionless").Submit();

            // Submit feature usages
            ExceptionlessClient.Default.SubmitFeatureUsage("MyFeature");
            ExceptionlessClient.Default.CreateFeatureUsage("MyFeature").AddTags("Exceptionless").Submit();

            // Submit a 404
            ExceptionlessClient.Default.SubmitNotFound("/somepage");
            ExceptionlessClient.Default.CreateNotFound("/somepage").AddTags("Exceptionless").Submit();

            // Submit a custom event type
            ExceptionlessClient.Default.SubmitEvent(new Event { Message = "Low Fuel", Type = "racecar", Source = "Fuel System" });

            return "Exceptionless";
        }
    }
}
