using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp9
{
    public record Person(string FirstName, string LastName);

    public class People
    {
        int postion = -1;
        private Person[] _people { get; init; }
        public People(Person[] people)
        {
            _people = people;
        }

        public bool MoveNext()
        {
            postion++;
            return (postion < _people.Length);
        }

        public object Current
        {
            get
            {
                try
                {
                    return _people[postion];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Reset()
        {
            postion = -1;
        }
        public void Dispose()
        {
            Reset();
        }
    }


    public static class PeopleExtensions
    {
        //public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> people) => people;
        public static People GetEnumerator(this People people) => people;
    }
}
