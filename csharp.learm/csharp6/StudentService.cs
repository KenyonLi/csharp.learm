using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp6
{
    public class StudentService
    {
        public static int Id { get; set; }
        public static string? Name;
        public static void StudyStatic()
        {
            Console.WriteLine("this is static Method....");
        }

        public static void ShowExceptionType()
        {
            Console.WriteLine("开始抛异常。。。");
            //throw new Exception("001");
            throw new Exception("002");
        }
    }
}
