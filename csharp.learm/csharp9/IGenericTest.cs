using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp9
{
    public abstract class Animal
    {
        public abstract Food GetFood();

    }
    public class Tiger : Animal
    {
        public override Meat GetFood() => new Meat();
    }

    public class Food
    {

    }

    public class Meat : Food
    {
    }
}
