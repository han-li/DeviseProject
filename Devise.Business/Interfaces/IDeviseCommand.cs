namespace Devise.Business.Interfaces
{
    public interface IDeviseCommand
    {
        string Source { get; }

        string Result { get; set; }

        void Execute();
    }
}
