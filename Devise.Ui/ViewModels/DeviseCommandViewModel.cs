using Devise.Business.Implementations;
using Devise.Business.Interfaces;
using Devise.Ui.Utilities;

namespace Devise.Ui.ViewModels
{
    public class DeviseCommandViewModel : ViewModelBase
    {
        #region Fields

        private string _source;

        private string _target;

        #endregion

        #region Properties

        public bool Removable { get; private set; }

        public DeviseMainViewModel Parent { get; private set; }

        public string Source
        {
            get { return _source; }
            set
            {
                if(_source == value)
                    return;

                _source = value;
                OnPropertyChanged();
            }
        }

        public string Target
        {
            get { return _target; }
            set
            {
                if(_target == value)
                    return;

                _target = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public DeviseCommandViewModel(DeviseMainViewModel parent, bool removable = true)
        {
            Parent = parent;
            Removable = removable;
        }

        #endregion

        #region Members

        public IDeviseCommand GetDeviseCommand(IDeviseCommandReceiver deviseCommandReceiver)
        {
            return new ImplDeviseCommand(Source, deviseCommandReceiver);
        }

        #endregion
    }
}
