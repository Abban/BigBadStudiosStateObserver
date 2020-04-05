namespace BBX.Library.StateObserver
{
    public interface IStateObserverNotifier
    {
        /// <summary>
        /// Notify Observers of a change in property state
        /// </summary>
        void NotifyObservers();
    }
}