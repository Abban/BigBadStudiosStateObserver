using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.Library.StateBroker
{
    public class StateBroker : IStateBroker
    {
        private bool _isTransacting;
        private readonly List<IObservableStateProperty> _properties = new List<IObservableStateProperty>();


        public void AddProperty(IObservableStateProperty property)
        {
            if (_properties.Contains(property))
            {
                throw new Exception("Tried to add the same property to the broker twice");
            }
            _properties.Add(property);
        }

        
        public void RemoveProperty(IObservableStateProperty property)
        {
            if (!_properties.Contains(property))
            {
                throw new Exception("Tried to remove an unknown property from the broker");
            }
            _properties.Remove(property);
        }

        
        public void StartTransaction()
        {
            if (_isTransacting)
            {
                throw new TransactionException();
            }

            _isTransacting = true;
        }

        
        public void Commit()
        {
            NotifyObservers();
            _isTransacting = false;
        }

        
        private void NotifyObservers()
        {
            var called = new List<Delegate>();
        
            foreach (var property in _properties.Where(x => x.Dirty))
            {
                var delegates = property.Action.GetInvocationList();

                foreach (var @delegate in delegates)
                {
                    if (called.Contains(@delegate)) continue;

                    @delegate.DynamicInvoke();
                    called.Add(@delegate);
                }
                
                property.SetClean();
            }
        }
    }
}