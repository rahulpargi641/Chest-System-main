using System;

public class EventService : MonoSingletonGeneric<EventService>
{
    public static event Action<int, int> onChestUnlocked;
    public static event Action<ChestView> OnChestOpened;
    public static event Action onChestSlotsFull;
    public static event Action onRewardCollected;
    public static event Action<int> onGemsUsed;

    public void InvokeOnChestUnlocked(int gemsReceived, int coinsReceived)
    {
        onChestUnlocked?.Invoke(gemsReceived, coinsReceived);
    }

    public void InvokeOnChestOpened(ChestView chestToRemove)
    {
        OnChestOpened?.Invoke(chestToRemove);
    }

    public void InvokeOnChestSlotsFull()
    {
        onChestSlotsFull?.Invoke();
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