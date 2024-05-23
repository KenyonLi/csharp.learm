using System.Runtime.InteropServices;

namespace csharp.learm
{
    public class csharp8Info
    {
        // C# 8.0 版本专门面向 .NET C# core 的第一个主要c# 版本
        // 一些功能依赖于新的CLR功能，而其他功能依赖于仅在 .NET Core 中添加的库类型。c# 8.0 向c#语言添加了以下功能和增强功能；

        public static void Show()
        {
            //Readonly 成员
            {
                Point point = new Point()
                {
                    X = 123,
                    Y = 234
                };
                string text = point.ToString();
                // Console.WriteLine($"Point {text}");
            }
            //默认接口方法
            {
                IStudent student = new Student() { };
                student.GetAge();
                student.GetName();
            }


            //可处置的 ref 结构
            {
                //详见C#8  UserStruct 结构
            }

            //可为空引用类型
            {
                string? name = "kenyonli";
                Student? student = null;
            }

            //异步流
            {
                //GetOldAsyncIntList(); 
                GetAsyncData().Wait();
            }

            //异步可释放IAsyncDisposable
            {
                GetAsyncDispose().Wait();
            }

            //索引和范围
            {
                string[] words = new string[]
                {
                                // index from start    index from end
                    "The",      // 0                   ^9
                    "quick",    // 1                   ^8
                    "brown",    // 2                   ^7
                    "fox",      // 3                   ^6
                    "jumped",   // 4                   ^5
                    "over",     // 5                   ^4
                    "the",      // 6                   ^3
                    "lazy",     // 7                   ^2
                    "dog"       // 8                   ^1
                };
                string message1 = words[0];
                string message2 = words[1];

                var quickBrownFox = words[1..4];



                string message3 = words[^1];
                string message4 = words[^2];

                var lazyDog = words[^2..^0];


                var allWords = words[..]; // contains "The" through "dog".
                var firstPhrase = words[..4]; // contains "The" through "fox"
                var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"

                //声明范围
                Range phrase = 1..4;
                var text = words[phrase]; //使用范围
            }


            //Null 合并赋值
            {
                List<int> numbers = null;
                int? i = null;

                numbers ??= new List<int>();
                numbers.Add(i ??= 17);
                numbers.Add(i ??= 20);

                Console.WriteLine(string.Join(" ", numbers));  // output: 17 17
                Console.WriteLine(i);  // output: 17

                numbers ??= new List<int>() { 123, 234, 345, 456, 567, 678 };
            }

            //非托管构造类型
            {
                Span<Coords<int>> coordinates = stackalloc[]
                {
                    new Coords<int> { X = 0, Y = 0 },
                    new Coords<int> { X = 0, Y = 3 },
                    new Coords<int> { X = 4, Y = 0 }
                };

                Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
                var ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6, 8 });
                Console.WriteLine(ind);  // output: 1
            }

            //内插逐字字符串的增强功能--$和@不分前后顺序了--在早期，$必须在@前面
            {
                string teacher = " kenyonli";
                string text = $@" ，我是{teacher}";

                string text1 = $@"欢迎大家来到.NET 
                                     ，
                                    我是{teacher}";
                string text2 = @$"欢迎大家来到.NET 
                                    我是{teacher}";
            }
        }
        /// <summary>
        /// using声明--老版本
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static int WriteLinesToFile(IEnumerable<string> lines)
        {
            using (var file = new System.IO.StreamWriter("WriteLines2.txt"))
            {
                int skippedLines = 0;
                foreach (string line in lines)
                {
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                    else
                    {
                        skippedLines++;
                    }
                }
                return skippedLines;
            } // file is disposed here
        }


        /// <summary>
        /// using声明--新版本
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static int WriteLinesToFileNew(IEnumerable<string> lines)
        {
            using var file = new System.IO.StreamWriter("WriteLines2.txt");
            int skippedLines = 0;
            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    file.WriteLine(line);
                }
                else
                {
                    skippedLines++;
                }
            }
            // Notice how skippedLines is in scope here.
            return skippedLines;
            // file is disposed here
        }

        /// <summary>
        /// Switch模式
        /// </summary>
        /// <param name="colorBand"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Rainbow FromRainbowClassic(Rainbow colorBand)
        {
            switch (colorBand)
            {
                case Rainbow.Red:
                    return Rainbow.Red;
                case Rainbow.Orange:
                    return Rainbow.Orange;
                case Rainbow.Yellow:
                    return Rainbow.Yellow;
                case Rainbow.Green:
                    return Rainbow.Green;
                case Rainbow.Blue:
                    return Rainbow.Blue;
                case Rainbow.Indigo:
                    return Rainbow.Indigo;
                case Rainbow.Violet:
                    return Rainbow.Violet;
                default:
                    throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand));
            };
        }


        /// <summary>
        /// Switch模式
        /// </summary>
        /// <param name="colorBand"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Rainbow FromRainbow(Rainbow colorBand) => colorBand switch
        {
            Rainbow.Red => Rainbow.Red,
            Rainbow.Orange => Rainbow.Orange,
            Rainbow.Yellow => Rainbow.Yellow,
            Rainbow.Green => Rainbow.Green,
            Rainbow.Blue => Rainbow.Blue,
            Rainbow.Indigo => Rainbow.Indigo,
            Rainbow.Violet => Rainbow.Violet,
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
        };

        public static decimal ComputeStaesTax(Address loction, decimal salePrice) => loction switch
        {
            { State: "WA" } => salePrice * 0.06M,
            { State: "MN" } => salePrice * 0.075M,
            _ => 0M

        };

        /// <summary>
        /// 元组模式
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string RockPaperScissors(string first, string second) => (first, second) switch
        {
            ("rock", "paper") => "rock is covered by paper. Paper wins.",
            ("rock", "scissors") => "rock breaks scissors. Rock wins.",
            ("paper", "rock") => "paper covers rock. Paper wins.",
            ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
            ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
            ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
            (_, _) => "tie"
        };

        /// <summary>
        /// 位置模式
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Quadrant GetQuadrant(Point point) => point switch
        {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            var (_, _) => Quadrant.OnBorder,
        };


        /// <summary>
        /// 异步流支持
        /// </summary>
        /// <returns></returns>
        public static async IAsyncEnumerable<int> GetNewAsyncIntList()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        /// <summary>
        /// 异步流支持
        /// </summary>
        /// <returns></returns>
        public static async Task GetAsyncData()
        {
            await foreach (var number in GetNewAsyncIntList())
            {
                Console.WriteLine(number);
            }
        }

        public static async Task GetAsyncDispose()
        {
            await using (var exampleAsyncDisposable = new ExampleAsyncDisposable())
            {

            }
        }
    }
}
