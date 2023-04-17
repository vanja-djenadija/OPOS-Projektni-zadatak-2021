using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyTaskScheduler
{
    public abstract class Resource
    {
        public Stream _resource { get; set; }

        public MyTask Task { get; set; }

        public bool Taken { get; set; } = false;

        public Resource() { }

        public abstract Stream GetResource();
    }
}
