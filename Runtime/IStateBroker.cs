namespace GF.Library.StateBroker
{
    public interface IStateBroker
    {
        /// <summary>
        /// a target for a property to alert to a value change
        /// </summary>
        /// <param name="property"></param>
        void SetChanged(IObservableStateProperty property);
        
        /// <summary>
        /// Start a state transaction
        /// </summary>
        void StartTransaction();

        /// <summary>
        /// Finish a state transaction
        /// </summary>
        void Commit();
    }
}