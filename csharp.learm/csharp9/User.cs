using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp9
{
    public partial class User
    {
        public partial void Show(string message);
    }

    public partial class User
    {
        public partial void Show(string message) => Console.WriteLine(message);
    }
}
