using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Devise.Ui.Utilities
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion  //  Event

        #region Members

        protected void OnPropertyChanged(/*[CallerMemberName] */string propertyName = "")
        {
            if (propertyName == null)   //  Replace CallerMemberName because it doesn't exist in .net 4.0
            {
                var stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(1);
                var method = frame.GetMethod();
                propertyName = method.Name.Replace("set_", "");
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion  //  Members
    }
}
