using csharp.learm.csharp10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm
{
    public class csharp10Info
    {
        public const string PolicyPrefix = "Permission:";

        public static void Show()
        {

            #region -记录结构-
            {
                var policy = "Permission:user,comm";
                var s = policy[PolicyPrefix.Length..].Split(",", StringSplitOptions.RemoveEmptyEntries);

                PersonStruct personStruct = new PersonStruct
                {
                    Name = "k",
                    Age = 1,
                    Gender = Gender.Male,

                };
                //  personStruct.Age = 33;// 不允许修改

                PersonNewStruct personNewStruct = new PersonNewStruct
                {
                    Name = "kenyonli",
                    Age = 35,
                    Gender = Gender.Male,
                };
                // personStruct.Age = 444;
                personNewStruct.Deconstruct(out string name, out int age, out Gender gender);
                Console.WriteLine($"Name:{name},Age:{age},Gender:{gender}");
            }
            #endregion

            List<string> list = new List<string>();
            HashSet<string> ht = new HashSet<string>();
            list.Add("dd");
            ht.Add("dd");
        }
    }
}
