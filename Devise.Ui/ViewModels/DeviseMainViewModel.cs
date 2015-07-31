using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Devise.Business.Implementations;
using Devise.Configuration;
using Devise.Ui.Utilities;

namespace Devise.Ui.ViewModels
{
    public class DeviseMainViewModel : ViewModelBase
    {
        #region Fields

        private static volatile object _lock = new object();

        private static DeviseMainViewModel _default;

        private double _scrollViewerVerticalOffset;

        private bool _isEnable;

        #endregion  //  Fields

        #region Properties

        public static DeviseMainViewModel Default
        {
            get
            {
                if (_default == null)
                {
                    lock (_lock)
                    {
                        if (_default == null)
                        {
                            _default = new DeviseMainViewModel();
                        }
                    }
                }

                return _default;
            }
        }

        public double ScrollViewerVerticalOffset
        {
            get { return _scrollViewerVerticalOffset; }
            set
            {
                if(DoubleUtil.AreClosed(_scrollViewerVerticalOffset, value))
                    return;

                _scrollViewerVerticalOffset = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                if(_isEnable == value)
                    return;

                _isEnable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DeviseCommandViewModel> DeviseCommands { get; }

        public RelayCommand ConvertCommand { get; private set; }

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand RemoveCommand { get; private set; }

        public XmlFileCommandReceiver DeviseCommandReceiver { get; }

        #endregion  //  Properties

        #region Constructor

        private DeviseMainViewModel()
        {
            _isEnable = true;

            DeviseCommands = new ObservableCollection<DeviseCommandViewModel>();

            ConvertCommand = new RelayCommand(ConvertAction);
            AddCommand = new RelayCommand(AddAction);
            RemoveCommand = new RelayCommand(RemoveAction);

            DeviseCommandReceiver = new XmlFileCommandReceiver(Settings.Default.TargetFileName);

            DeviseCommands.Add(new DeviseCommandViewModel(this, false));
        }

        #endregion  //  Constructor

        #region Members

        private void ConvertAction(object parameter)
        {
            IsEnable = false;
            DeviseCommandReceiver.ClearCache();

            Task.Factory.ContinueWhenAll(
                DeviseCommands.Select(viewModel => Task.Factory.StartNew(SendCommand, viewModel)).ToArray(), Done,
                CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void AddAction(object parameter)
        {
            DeviseCommands.Add(new DeviseCommandViewModel(this));
        }

        private void RemoveAction(object parameter)
        {
            var deviseCommand = parameter as DeviseCommandViewModel;

            DeviseCommands.Remove(deviseCommand);
        }

        private void SendCommand(object obj)
        {
            var vm = obj as DeviseCommandViewModel;

            Debug.Assert(vm != null, "vm != null");
            var command = vm.GetDeviseCommand(DeviseCommandReceiver);
            lock (DeviseCommandReceiver)
            {
                command.Execute();
            }

            vm.Target = command.Result;
        }

        private void Done(Task[] obj)
        {
            IsEnable = true;
        }

        #endregion  //  Members

    }
}
