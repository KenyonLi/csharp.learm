using static csharp.learm.csharp6.StudentService; //引入带有静态成员的类
using static csharp.learm.csharp6.Color;
//using 取别名
using csharp6 = csharp.learm.csharp6;
using csharp6GenericInt = csharp.learm.csharp6.GenericClass<int>;
using Csharp6GenerichString = csharp.learm.csharp6.GenericClass<string>;
namespace csharp.learm.csharp6
{
    /// <summary>
    /// c# 6 语法特性
    /// 随着 vs2015一起发布，通过该版本，它不再推出主导的杀手锏，而是发布了很多使得C#编程更有效率的小功能。
    /// </summary>
    internal class csharp6
    {
        /* 1.静态导入  using
         * 2. 异常筛选器 
         * 3. 自动属性初始化表达式
         * 4. Expression bodied 成员 
         * 5. Null传播器
         * 6. nameof运算符
        */

        public void Show()
        {
            {
                //静态引入  
                Id = 1;
                StudyStatic();
                var red = Red;
                Console.WriteLine($"color.red= {red}");
            }

            // using 取别名
            {
                csharp6GenericInt genericIntClass = new csharp6GenericInt();
                genericIntClass.Show(555);

                Csharp6GenerichString genericString = new Csharp6GenerichString();
                genericString.Show("kenyonli");
            }

            { //异常过滤
                try
                {
                    StudentService.ShowExceptionType();
                }
                catch (Exception ex) when (ex.Message.Contains("002"))
                {

                    Console.WriteLine("这里是处理捕捉到的002筛选。。");
                }
            }

            {//自动属性初始化表达式
                //取默认值
                Console.WriteLine(Id);
            }
            { //null 条件运算符 ?. 
                UserInfo user = new UserInfo();
                string? userName = user?.Name;
                if (userName == null)
                {
                    Console.WriteLine($"user?.Name:{userName}");
                }

                UserInfo? user1 = null;
                string? userName1 = user1?.Name;
                if (userName1 == null)
                {
                    Console.WriteLine($"user1?.Name:{userName1}");
                }
            }

        }
    }
}
