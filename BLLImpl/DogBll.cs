using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLImpl
{
    public class DogBll : IDogBll
    {
        public void Bark()
        {
            Console.WriteLine("汪汪汪汪");
        }
    }
}
