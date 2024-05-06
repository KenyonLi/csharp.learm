using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp9
{
    public record Person1(string FirstName, string LastName);

    public record Person2
    {
        public Person2()
        {
            FirstName = "kkkkk";
        }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
    };

    public record Person3
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    };

    public record Person4(string FirstName, string LastName, string[] PhoneNumbers);

    public record Person5(string FirstName, string LastName)
    {
        public string[] PhoneNumbers { get; set; }
    }

    public record Person6(string FirstName, string LastName);

    public record Teacher(string FirstName, string LastName, int Grade) : Person6(FirstName, LastName);
    public record Student1(string FirstName, string LastName, int Grade) : Person6(FirstName, LastName);
}
