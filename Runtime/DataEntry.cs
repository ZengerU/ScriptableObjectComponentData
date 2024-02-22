using System;

namespace ComponentDatafier.Runtime
{
    [Serializable]
    public class DataEntry<T> 
    {
        public bool isActive;
        public T value;
        Action _dataUpdated;

        public void RegisterAction(Action action)
        {
            _dataUpdated = action;
        }

        public void UnregisterAction()
        {
            _dataUpdated = default;
        }
        public void UpdateValue(T newValue)
        {
            value = newValue;
            _dataUpdated?.Invoke();
        }
    }
}