using System;

namespace Lifetime.Demo.IServices
{
    public interface ITestScoped
    {
        void Show();
    }

    public class TestScoped : ITestScoped, IDisposable
    {

        public void Show()
        {
            Console.WriteLine("TestScoped");
        }

        public void Dispose()
        {
            Console.WriteLine($" GetHashCode:{this.GetHashCode()}  TestScoped  调用了 Dispose");
        }
    }

    public interface ITestTransient { }
    public class TestTransient : ITestTransient, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"GetHashCode:{this.GetHashCode()}  TestTransient  调用了 Dispose");

        }
    }

    public interface ITestSingleton { }
    public class TestSingleton : ITestSingleton, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($" GetHashCode:{this.GetHashCode()}  TestSingleton  调用了 Dispose");
        }
    }
}
