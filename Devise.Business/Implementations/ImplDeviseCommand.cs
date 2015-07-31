using Devise.Business.Interfaces;

namespace Devise.Business.Implementations
{
    public class ImplDeviseCommand : IDeviseCommand
    {
        #region Fields

        #endregion

        #region Properties

        public string Source { get; }

        public string Result { get; set; }

        public IDeviseCommandReceiver DeviseCommandReceiver { get; }

        #endregion

        #region Constructor

        public ImplDeviseCommand(string source, IDeviseCommandReceiver deviseCommandReceiver)
        {
            Source = source;
            DeviseCommandReceiver = deviseCommandReceiver;
        }

        #endregion

        #region Members

        public void Execute()
        {
            DeviseCommandReceiver.TreatCommand(this);
        }

        #endregion
    }
}
