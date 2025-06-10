using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraExplorer.Models
{
    public class FileItemModel
    {
        public string? Name { get; set; }
        public string? FullPath { get; set; }
        public long Size { get; set; }
        public string? Extension { get; set; }
        public bool IsDirectory { get; set; }
    }
}
