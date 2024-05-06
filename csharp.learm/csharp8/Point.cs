using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp8
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);

        public double Distance => Math.Sqrt(X * X + Y * Y);
        public readonly override string ToString() => $"{X},{Y} is {Distance}";
    }


}
