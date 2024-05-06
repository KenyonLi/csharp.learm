using csharp.learm.csharp9;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm
{
    public class csharp9Info
    {
        public static void Show()
        {
            #region 记录类型
            RecordTest();
            #endregion

        }

        public static void RecordTest()
        {
            //不可变性
            Person1 person = new Person1("kenyoli", "lkn");
            Console.WriteLine($"persion.FirstName={person.FirstName}");
            Console.WriteLine($"persion.LastName={person.LastName}");

            person.Deconstruct(out string f, out string l);
            Console.WriteLine($"persion.FirstName={f}");
            Console.WriteLine($"persion.LastName={l}");
            //person.FirstName = "ddff";  不能重新赋值
            Console.WriteLine("================================================");

            // 不可变性   init
            Person2 persionInfo = new Person2()
            {
                LastName = "last"
            };

            Console.WriteLine($"persion.FirstName={persionInfo.FirstName}");
            Console.WriteLine($"persion.LastName={persionInfo.LastName}");
            Console.WriteLine("===========================================");

            //可变
            Person3 personNew = new Person3()
            {
                FirstName = "Richard老师",
                LastName = "金牌讲师Richard"
            };
            Console.WriteLine($"personNew.FirstName={personNew.FirstName}");
            Console.WriteLine($"personNew.LastName={personNew.LastName}");
            personNew.FirstName = "SDGWRWT"; //可以重新赋值
            Console.WriteLine($"personNew.FirstName={personNew.FirstName}");

            Console.WriteLine("==============================");
            //API 的不可变返回类型：如果你设计了一个 API，并且希望返回的数据对象是不可变的，那么使用记录类型可以确保客户端无法修改返回的数据对象
            var response = new ApiResponse<string>("Data", true, "Success");

            Console.WriteLine(response.ToString());

            Console.WriteLine("==============================");
            {
                // 值相等性
                var phoneNumbers = new string[2];
                Person4 person1 = new("Nanc", "Davolio", phoneNumbers);
                Person4 person2 = new("Nanc", "Davolio", phoneNumbers);

                Console.WriteLine(person1 == person2);// output : True

                person1.PhoneNumbers[0] = "1232-34343";

                Console.WriteLine(person1 == person2);// output : True

                Console.WriteLine(ReferenceEquals(person1, person2)); //output: False
            }


            {
                Console.WriteLine("===============非破坏性变化===================");
                //非破坏性变化
                Person5 person1 = new("Nanc", "Davolio") { PhoneNumbers = new string[1] };
                Console.WriteLine(person1);

                Person5 person2 = person1 with { FirstName = "John" };
                Console.WriteLine(person2);

                Console.WriteLine(person1 == person2);// output:false

                person2 = person1 with { PhoneNumbers = new string[1] };
                Console.WriteLine(person2);

                person2 = person1 with { };
                Console.WriteLine(person1 == person2);
                Console.WriteLine($"显示的内置格式：{person1.ToString()}");

            }
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
            {
                //继承
                //条记录可以从另一条记录继承。但是不能从类继承，类也不能从记录继承。
                Person6 teachr = new Teacher("Nan", "Dav", 3);

                Console.WriteLine(teachr);
            }
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
            {
                //要使两个记录变量相等，运行时类型必须相等。包含变量的类型可能不同，下面的代码示例中说明了
                Person6 teacher = new Teacher("Na", "Da", 3);
                Person6 studen = new Student1("Na", "Da", 3);
                Console.WriteLine(teacher == studen);

                Person6 student2 = new Student1("Na", "Da", 3);
                Console.WriteLine(student2 == studen);

                //派生类型和基类类型的所有公共属性和字段都包含在 ToString 输出中，如以下示例所示
                Console.WriteLine(teacher.ToString());
                Console.WriteLine(studen.ToString());
                Console.WriteLine(student2.ToString());
            }
            {
                // 仅限 Init的资源库
                var now = new WeatherObservation
                {
                    RecordedAt = DateTime.Now,
                    TemperatureInCelsius = 20,
                    PressureInMillibars = 9989.0m
                };

                // now.TemperatureInCelsius = 18;// 不允许--必须在初始化的时候赋值，后续就不允许修改了。
                Console.WriteLine(now);
            }
            #region 顶级语句
            {
                //顶级语句 - 不使用 Main 方法的程序
                //最明显的案例就是控制台直接撸码
                //只有一行代码执行所有操作。 借助顶级语句，可使用 using 指令和执行操作的一行替换所有样本：
            }
            #endregion
            #region 模式匹配增强功能
            {
                //C# 9 包括新的模式匹配改进： 
                //类型模式 与对象匹配特定类型
                //带圆括号的模式强制或强调模式组合的优先级
                //联合 and 模式要求两个模式都匹配
                //析取 or 模式要求任一模式匹配
                //否定 not 模式要求模式不匹配
                //关系模式要求输入小于、大于、小于等于或大于等于给定常数。

                bool bResult = Extension.IsLetter('1');
                bool bResult1 = Extension.IsLetterOrSeparator('C');

                string? e = null;
                if (e is not null)
                {
                    // ...
                }

            }
            #endregion


            #region 调整和完成功能
            //还有其他很多功能有助于更高效地编写代码。 在 C# 9.0 中，已知创建对象的类型时，可在 new 表达式中省略该类型。 最常见的用法是在字段声明中：
            {
                List<WeatherObservation> _observations = new();
                WeatherStation station = new() { Location = "Seattle, WA" };
                station.ForecastFor(DateTime.Now.AddDays(2), new());
            }
            #endregion

            #region 静态匿名函数 
            {
                Func<int, bool> func = static i => { return true; };
                func.Invoke(123);
            }
            #endregion


            #region 扩展 GetEnumerator 支持 foreach 循环。
            {
                Person[] PersonList = new Person[3]
                {
                    new ("John", "Smith"),
                    new ("Jim", "Johnson"),
                    new ("Sue", "Rabon"),
                };
                People people = new People(PersonList);
                foreach (var perso in people)
                {
                    Console.WriteLine(perso);
                }
            }
            #endregion

            #region Lambda 弃元参数
            {
                // C# 9 之前
                Func<int, int, int> zero = (a, b) => 0;
                Func<int, int, int> func = delegate (int a, int b) { return 0; };

                // C# 9
                Func<int, int, int> zero1 = (_, _) => 0;
                Func<int, int, int> func2 = delegate (int _, int _) { return 0; };
            }
            #endregion

            #region MyRegion
            {
                string mark = "line";
                string?[] lines = new string[5] { "line1", "line2", "line3", "line4", null };
                foreach (var line in lines)
                {
                    if (IsValid(line))
                    {
                        // Processing logic...
                    }
                    ShowConsole(line);
                }


                bool IsValid([NotNullWhen(true)] string? line)
                {
                    return !string.IsNullOrEmpty(line) && line.Length >= mark.Length;
                }

                [CusotmAttribute]
                bool ShowConsole([CusotmAttribute] string? line)
                {
                    return !string.IsNullOrEmpty(line) && line.Length >= mark.Length;
                }

            }
            #endregion


            #region 扩展分部方法
            {
                User user = new User();
                user.Show("123456");

            }
            #endregion 
        }
        public record ApiResponse<T>(T Data, bool IsSuccess, string Message);
    }
}
public static class Extension
{
    public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

    public static bool IsLetterOrSeparator(this char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';


}