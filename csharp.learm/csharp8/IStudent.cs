using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.learm.csharp8
{
    public interface IStudent
    {
        public int GetAge();
        /// <summary>
        /// 接口默认实现
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return "Kenyonli";
        }
    }
}
