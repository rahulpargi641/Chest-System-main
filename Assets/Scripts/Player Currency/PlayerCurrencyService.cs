using UnityEngine;

public class PlayerCurrencyService : MonoSingletonGeneric<PlayerCurrencyService>
{
    public int GemsInAccount => gemsInAccount;
    public int CoinsInAccount => coinsInAccount;

    private int gemsInAccount;
    private int coinsInAccount;

    private void Start()
    {
        gemsInAccount = 20;
        coinsInAccount = 100;

        EventService.onChestUnlocked += UpdateCurrencies;
        EventService.onGemsUsed += DecrementGems;
    }

    private void OnDestroy()
    {
        EventService.onChestUnlocked -= UpdateCurrencies;
        EventService.onGemsUsed -= DecrementGems;
    }

    public void UpdateCurrencies(int gemsReceived, int coinsReceived)
    {
        IncrementGems(gemsReceived);
        IncrementCoins(coinsReceived);
    }

    private void IncrementGems(int gemsReceived)
    {
        gemsInAccount += gemsReceived;
    }

    private void DecrementGems(int gemsUsed)
    {
        gemsInAccount -= gemsUsed;
    }

    private void IncrementCoins(int coinsReceived)
    {
        coinsInAccount += coinsReceived;
    }

    private void DecrementCoins(int coinsUsed)
    {
        coinsInAccount -= coinsUsed;
    }
}
