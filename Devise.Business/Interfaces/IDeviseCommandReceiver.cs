namespace Devise.Business.Interfaces
{
    public interface IDeviseCommandReceiver
    {
        void TreatCommand(IDeviseCommand command);
    }
}
