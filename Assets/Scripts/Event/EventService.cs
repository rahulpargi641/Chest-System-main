using System;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action<int, int> OnChestUnlocked; 
    public event Action<ChestView> OnChestOpened;
    public event Action OnChestSlotsFull;
    public event Action OnRewardCollected;
    public event Action<int> OnGemsUsed;

    public void InvokeOnChestUnlocked(int gemsReceived, int coinsReceived)
    {
        OnChestUnlocked?.Invoke(gemsReceived, coinsReceived);
    }

    public void InvokeOnChestOpened(ChestView chestToRemove)
    {
        OnChestOpened?.Invoke(chestToRemove);
    }

    public void InvokeOnChestSlotsFull()
    {
        OnChestSlotsFull?.Invoke();
    }

    public void InvokeOnRewardCollected()
    {
        OnRewardCollected?.Invoke();
    }

    public void InvokeOnGemsUsed(int gemsUsed)
    {
        OnGemsUsed?.Invoke(gemsUsed);
    }
}