using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp7
{
    /// <summary>
    /// c# 7.0 版本与vs2017一起发布。虽然该版本继承和发展了C#6.0,但不包含编译器即服务
    /// </summary>
    internal class csharp7
    {
        /* 1. out 变量
         * 2. 元组和析构函数
         * 3. 模式匹配
         * 4. 本地函数
         * 5. Ref局部变量和返回结果
         */

        public void Show()
        {
            {
                // out 
                var intput = "1234343";
                if (int.TryParse(intput, out int v))
                {

                    Console.WriteLine(v);
                }
            }
            {
                // 元组
                var a = (1, 2);
                (int, int) t = a;

                (int a, int b) c = t;
                var f = (A: 5, B: 10);
                var q = (B: 5, A: 10);
                Console.WriteLine($"a.Item1:{a.Item1},a.Item2:{a.Item2}");
                Console.WriteLine($"c.a:{c.a},c.b:{c.b}");
                Console.WriteLine($"a==c:{a == c}");
                Console.WriteLine($"f==q:{f == q}");
                Console.WriteLine($"f==c:{f == c}");

            }
            {
                //字典元组

                var limitsLookup = new Dictionary<int, (int min, int max)>
                {
                    [1] = (min: 122, max: 34343),
                    [2] = (min: 12, max: 34),
                    [3] = (min: 32, max: 364)
                };

                if (limitsLookup.TryGetValue(3, out (int Min, int Max) limits))
                {
                    Console.WriteLine($"Found limits: min is {limits.Min}, max is {limits.Max}");
                }

            }
            {
                //模式匹配
                var a = 1;
                if (a is int b)
                {
                    Console.WriteLine($"模式匹配 b{b}");
                }
                string? s = "模式匹配";
                if (s is not null)
                {
                    Console.WriteLine($"模式匹配 s{s}");
                }

            }
            {
                // 本地函数
                SayHello("你好");
                string SayHello(string h)
                {
                    Console.WriteLine(h);
                    return h;
                }
            }

            {
                //弃元
                var (_, p, _) = (1, 2, 3);
                Console.WriteLine($"弃元：(_, p, _)= {p}");

            }
            {
                //switch
                string sResult1 = PerformOperation("SystemTest");
                Console.WriteLine($"PerformOperation {sResult1}");

                string PerformOperation(string comm) => comm switch
                {
                    "SystemTest" => "SystemTest",
                    "SysTest" => "SysTest",
                    _ => throw new ArgumentException("Invlid string value of command", nameof(comm))

                };
                string PerformOperation1(int comm) => comm switch
                {
                    (> 100) => "SystemTest",
                    (< 100) and (> 20) => "SysTest",
                    _ => throw new ArgumentException("Invlid string value of command", nameof(comm))

                };

            }

            { //命令参数

                QueryCityDataForYears(year1: 2022, year2: 2021, name: "Richard");

                void QueryCityDataForYears(string name, int year1, int year2)
                {
                    Console.WriteLine($"name:{name},year1:{year1},year2:{year2}");
                }
            }

        }
    }
}
