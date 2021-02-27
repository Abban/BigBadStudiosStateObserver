namespace GF.Library.StateBroker
{
    public interface IStateBroker
    {
        void AddProperty(IObservableStateProperty property);
        void RemoveProperty(IObservableStateProperty property);
        void StartTransaction();
        void Commit();
    }
}