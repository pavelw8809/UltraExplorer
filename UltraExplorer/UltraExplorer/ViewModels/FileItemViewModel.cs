using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;

namespace UltraExplorer.ViewModels
{
    public partial class FileItemViewModel : ObservableObject
    {
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string extension;
        [ObservableProperty]
        public long size;
        [ObservableProperty]
        public string fullpath;
        [ObservableProperty]
        public bool isDirectory;

        [ObservableProperty]
        private bool isBeingCopied;

        public FileItemViewModel(FileSystemInfo fsObj)
        {
            Name = fsObj.Name;
            Extension = Path.GetExtension(fsObj.FullName);            
            Fullpath = fsObj.FullName;

            if (fsObj is FileInfo fi)
            {
                Size = fi.Length;
                IsDirectory = false;
            }
            else
            {
                IsDirectory = true;
            }
        }
    }
}
