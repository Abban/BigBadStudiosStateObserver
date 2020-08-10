namespace GF.Library.StateObserver
{
    public interface IStatePropertyBroker
    {
        /// <summary>
        /// a target for a property to alert to a value change
        /// </summary>
        /// <param name="property"></param>
        void SetChanged(IObservableStateProperty property);
    }
}