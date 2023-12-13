

public class CurrencyService : MonoSingletonGeneric<CurrencyService>
{
    public int GetGemsInAccount() => gemsInAccount;
    public int GetCoinsInAccount() => coinsInAccount;

    private int gemsInAccount;
    private int coinsInAccount;

    private void Start()
    {
        gemsInAccount = 20;
        coinsInAccount = 100;
    }

    public void IncrementGems(int gems)
    {
        gemsInAccount += gems;
        UIService.Instance.SetCurrencyStats();
    }

    public void DecrementGems(int gems)
    {
        gemsInAccount -= gems;
        UIService.Instance.SetCurrencyStats();
    }

    public void IncrementCoins(int coins)
    {
        coinsInAccount += coins;
        UIService.Instance.SetCurrencyStats();
    }

    public void DecrementCoins(int coins)
    {
        coinsInAccount -= coins;
        UIService.Instance.SetCurrencyStats();
    }
}
