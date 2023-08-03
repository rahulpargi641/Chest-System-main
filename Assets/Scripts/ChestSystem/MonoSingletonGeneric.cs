using UnityEngine;

namespace ChestSystem
{
    public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
    {
        public static T Instance {get; private set;}
        protected void Awake( )
        {
            if(Instance == null )
            {
                Instance = this as T;
                DontDestroyOnLoad( this.gameObject );
            }
            else
            {
                Destroy( this.gameObject );
            }
        }
    }
}
