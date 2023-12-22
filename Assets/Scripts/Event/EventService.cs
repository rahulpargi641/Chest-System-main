using System;
using System.Collections;


public class EventService : MonoSingletonGeneric<EventService>
{
    public static event Action<int, int> onChestOpened;
    public static event Action onRewardCollected;
    public static event Action<int> onGemsUsed;

    public void InvokeOnRewardCollected()
    {
        onRewardCollected?.Invoke();
    }

    public void InvokeOnChestOpened(int gems, int coins)
    {
        onChestOpened?.Invoke(gems, coins);
    }

    public void InvokeOnGemsUsed(int gemsUsed)
    {
        onGemsUsed?.Invoke(gemsUsed);
    }
}