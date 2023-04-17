using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDokan
{
    public class File
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
