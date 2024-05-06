using csharp.learm.csharp9;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        }
        public record ApiResponse<T>(T Data, bool IsSuccess, string Message);
    }
}
