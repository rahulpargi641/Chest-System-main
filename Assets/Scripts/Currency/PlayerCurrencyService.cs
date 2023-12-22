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
        UIService.Instance.SetCurrencyStats();
    }

    private void DecrementGems(int gemsUsed)
    {
        gemsInAccount -= gemsUsed;
        UIService.Instance.SetCurrencyStats();
    }

    private void IncrementCoins(int coinsReceived)
    {
        coinsInAccount += coinsReceived;
        UIService.Instance.SetCurrencyStats();
    }

    private void DecrementCoins(int coinsUsed)
    {
        coinsInAccount -= coinsUsed;
        UIService.Instance.SetCurrencyStats();
    }
}
