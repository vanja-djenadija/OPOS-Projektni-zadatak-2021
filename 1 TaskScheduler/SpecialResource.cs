using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskScheduler
{
    public class SpecialResource : Resource
    {
        public override Stream GetResource()
        {
            return null;
        }
    }
}
