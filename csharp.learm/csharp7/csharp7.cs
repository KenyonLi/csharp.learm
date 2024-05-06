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

            {
                //类型测试
                int intResult = MidPoint(new List<int> { 123, 234, 444 });
                List<int> para = null;
                int nullResult = MidPoint(para);

            }

            { //命令参数

                QueryCityDataForYears(year1: 2022, year2: 2021, name: "Richard");

                void QueryCityDataForYears(string name, int year1, int year2)
                {
                    Console.WriteLine($"name:{name},year1:{year1},year2:{year2}");
                }
            }

            //Ref 局部变量和返回结果
            {
                int[] a = { 0, 1, 2, 3, 4, 5 };

                // x不是一个引用，函数将值赋值给左侧变量x
                int x = GetList(a);
                Console.WriteLine("======================================================================");
                Console.WriteLine($"x:{x}, a[2]:{a[a.Length - 1]}");
                x = 99;
                Console.WriteLine("======================================================================");
                Console.WriteLine($"x:{x}, a[2]:{a[a.Length - 1]} \n");

                // 返回引用，需要使用ref关键字，y是一个引用，指向a[a.Lenght-1]
                ref int y = ref GetList(a);
                Console.WriteLine("======================================================================");
                Console.WriteLine($"y:{y}, a[2]:{a[a.Length - 1]}");
                y = 100;
                Console.WriteLine("======================================================================");
                Console.WriteLine($"y:{y}, a[2]:{a[a.Length - 1]}");
            }

        }

        public T MidPoint<T>(IEnumerable<T> data)
        {
            if (data is List<T> list)
            {
                Console.WriteLine($"{nameof(List<T>)}");
                return list[list.Count / 2];
            }
            else
            {
                Console.WriteLine("not list");
                return default(T);
            }
        }


        public ref int GetList(int[] a)
        {
            if (a == null || a.Length < 1)
            {
                throw new Exception("数组为空");
            }

            int number = 18;
            // 错误声明: 引用申明和初始化分开是错误的 
            //ref int n1; 
            //n1 = number;

            //正确声明：申声明时必须初始化，声明和初始化在一起。
            //添加关键词 ref 表示 n1 引用是一个引用
            ref int n1 = ref number;
            // n1 指向 number,不论修改 n1 和 number,对双方都有影响，相当于双方绑定了。
            n1 = 19;

            Console.WriteLine($"n1:{n1},number:{number}");

            number = 20;
            Console.WriteLine($"n1:{n1},number:{number}");

            //语法正确，但本质是将a[2]的值赋值给n1引用所指，n1仍指向number,如果需要指向别的变量 需要在赋值号（=）之后加上ref
            n1 = a[2];
            Console.WriteLine($"n1:{n1},number:{number},a[a]={a[2]}");
            number = 21;
            Console.WriteLine($"n1:{n1},number:{number},a[a]={a[2]}");
               
            // --------------------- 引用返回 ------------------------ Cx j
            // 错误：n1引用number，但number生存期限于方法内，故不可返回
            // return ref n1;

            // 正确：n2引用a[2]，a[2]生存期不仅仅限于方法内，所以可以返回。
            ref int n2 = ref a[a.Length - 1];
            return ref n2; // 需要ref返回一个引用
            //return ref a[a.Length - 1];  // 也可以直接返回一个引用
        }
    }
}
