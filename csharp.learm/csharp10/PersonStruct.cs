using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp10
{
    public enum Gender
    {
        Male,
        Female
    }
    public record struct PersonStruct
    {
        public string Name { get; init; }
        public int Age { get;init; }
        public Gender Gender { get;init; }
    }
    /// <summary>
    /// 结构记录类型
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Age"></param>
    /// <param name="Gender"></param>
    public record struct PersonNewStruct(string Name, int Age, Gender Gender);




    record struct PersonInfo01()
    {
        public object Id { get; init; } = 123;
        public object Name { get; init; } = "kkkkk";
    }


    record struct PersonInfo02(string name)
    {
        public object Id { get; init; } = 123;
        public object Name { get; init; } = name;
    }

    record struct PersonInfo03
    {
        //构造函数必须使用public修饰
        public PersonInfo03()
        {
            Assembly assembly = Assembly.LoadFrom("csharp.learm.dll");
        }

        //构造函数必须使用public修饰
        public PersonInfo03(string name)
        {
            Name = name;
        }

        public object? Id { get; init; } = 123;
        public object? Name { get; init; } = "Richard";
    }

}
