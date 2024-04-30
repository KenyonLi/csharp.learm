using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp6
{
    public class GenericClass<T>
    {
        public void Show(T t)
        {
            Console.WriteLine($"T name is {typeof(T).FullName}");
        }
    }
}
