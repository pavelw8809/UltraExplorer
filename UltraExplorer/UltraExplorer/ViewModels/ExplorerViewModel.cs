using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using UltraExplorer.Models;

namespace UltraExplorer.ViewModels
{
    public partial class ExplorerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cPath;

        [ObservableProperty]
        private ObservableCollection<FileItemViewModel> fsObjects = new();

        [ObservableProperty]
        private ObservableCollection<BreadcrumbItem> breadcrumbs = new();

        [ObservableProperty]
        private ObservableCollection<string> availableFolders = new();

        [ObservableProperty]
        private string selectedFolder;

        public ExplorerViewModel(string cPath)
        {
            CPath = cPath;
            LoadFiles();
            UpdateBreadcrumbs();
            UpdateAvailableFolders();
        }

        public void LoadFiles()
        {
            FsObjects.Clear();
            var fsInfo = new DirectoryInfo(CPath);

            foreach (var fsObj in fsInfo.GetFileSystemInfos())
            {
                FsObjects.Add(new FileItemViewModel(fsObj));
            }

            UpdateAvailableFolders();
        }

        partial void OnSelectedFolderChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CPath = System.IO.Path.Combine(CPath, value);
                LoadFiles();
                UpdateBreadcrumbs();
                UpdateAvailableFolders();
            }
        }

        private void UpdateBreadcrumbs()
        {
            Breadcrumbs.Clear();
            string[] pathParts = CPath.Split(Path.DirectorySeparatorChar);
            string currentPath = "";

            foreach (var part in pathParts)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    currentPath = string.IsNullOrEmpty(currentPath) ? part : Path.Combine(currentPath, part);
                    Breadcrumbs.Add(new BreadcrumbItem(part, currentPath));
                }
            }
        }

        private void UpdateAvailableFolders()
        {
            AvailableFolders.Clear();
            var fsInfo = new DirectoryInfo(CPath);
            var chDirs = fsInfo.GetDirectories().Select(d => d.Name);

            foreach (var fld in chDirs)
            {
                AvailableFolders.Add(fld);
            }
        }

        [RelayCommand]
        public void Navigate(string selectedPath)
        {
            if (!selectedPath.Contains(Path.DirectorySeparatorChar))
            {
                selectedPath = $"{selectedPath}\\";
            }
            CPath = selectedPath;
            LoadFiles();
            UpdateBreadcrumbs();
        }

        [RelayCommand]
        public void SelectFolder(FileItemViewModel item)
        {
            if (CanSelectItem(item) && item.Fullpath != null) 
            {
                CPath = item.Fullpath;
                LoadFiles(); 
                UpdateBreadcrumbs();
            }
        }

        private bool CanSelectItem(FileItemViewModel item) => item.IsDirectory ? true : false;
    }

    public class BreadcrumbItem
    {
        public string DisplayName { get; }
        public string FullPath { get; }

        public BreadcrumbItem(string displayName, string fullPath)
        {
            DisplayName = displayName;
            FullPath = fullPath;
        }
    }
}
