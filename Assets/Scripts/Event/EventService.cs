using System;

public class EventService : MonoSingletonGeneric<EventService>
{
    public static event Action<int, int> onChestUnlocked;
    public static event Action<ChestView> OnChestOpened;
    public static event Action onRewardCollected;
    public static event Action<int> onGemsUsed;

    public void InvokeOnChestUnlocked(int gems, int coins)
    {
        onChestUnlocked?.Invoke(gems, coins);
    }

    public void InvokeOnChestOpened(ChestView chestView)
    {
        OnChestOpened?.Invoke(chestView);
    }

    public void InvokeOnRewardCollected()
    {
        onRewardCollected?.Invoke();
    }

    public void InvokeOnGemsUsed(int gemsUsed)
    {
        onGemsUsed?.Invoke(gemsUsed);
    }
}