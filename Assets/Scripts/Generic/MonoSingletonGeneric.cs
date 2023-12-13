using UnityEngine;

public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        { Destroy(gameObject); }
    }
}