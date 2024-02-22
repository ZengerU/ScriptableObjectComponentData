using System;
using UnityEngine;

namespace ComponentDatafier.Runtime
{
    public abstract class DataObject : ScriptableObject
    {
        public Action DataUpdated;
        public abstract void SubscribeToEntries();
        public abstract void UnsubscribeToEntries();

        void OnValidate()
        {
            DataUpdated?.Invoke();
        }
    }
}