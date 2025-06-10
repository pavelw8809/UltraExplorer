using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraExplorer.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public const int maxExpInstances = 5;

        [ObservableProperty]
        public ObservableCollection<ExplorerViewModel> explorers = new();

        public int ExplorerCount => Explorers.Count;

        public MainViewModel()
        {
            Explorers.Add(new ExplorerViewModel("C:\\"));
            Explorers.Add(new ExplorerViewModel("C:\\"));
        }

        [RelayCommand(CanExecute = nameof(CanAddExplorer))]
        private void AddExplorer()
        {
            Explorers.Add(new ExplorerViewModel("C:\\"));
            OnPropertyChanged(nameof(ExplorerCount));
            AddExplorerCommand.NotifyCanExecuteChanged();
        }

        private bool CanAddExplorer => ExplorerCount < maxExpInstances;


    }
}
