using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokanFileSystem
{
    class File
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
