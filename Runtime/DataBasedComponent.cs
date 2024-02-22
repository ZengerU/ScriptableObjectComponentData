using UnityEngine;

namespace ComponentDatafier.Runtime
{
    public abstract class DataBasedComponent<T> : MonoBehaviour where T : Component
    {
        protected T Target;
        
        protected virtual void Awake()
        {
            Target = GetComponent<T>();
            SetValues();
        }

        protected abstract void SetValues();
    }
}