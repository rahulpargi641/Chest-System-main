using System;
using System.Collections;


public class EventService : MonoSingletonGeneric<EventService>
{
    public static event Action OnRewardCollected;

    public void InvokeOnRewardCollected()
    {
        OnRewardCollected?.Invoke();
    }
}