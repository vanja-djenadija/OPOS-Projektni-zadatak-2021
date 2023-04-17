using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskScheduler
{
    public class FileResource : Resource
    {
        public string Path { get; private set; }
        public FileResource(string path)
        {
            Path = path;
        }

        public override Stream GetResource()
        {
            return File.Open(Path, FileMode.Open);
        }

        public override bool Equals(object obj)
        {
            var resource = obj as FileResource;
            if (resource == null)
                return false;

            return resource.Path.Equals(this.Path);
        }

        public override int GetHashCode()
        {
            return this.Path.GetHashCode();
        }

        public override string ToString()
        {
            return "Resource " + Path;
        }
    }
}
