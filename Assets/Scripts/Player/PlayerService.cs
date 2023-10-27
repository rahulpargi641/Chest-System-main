

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    private int gemsInAccount;
    private int coinsInAccount;

    private void Start()
    {
        gemsInAccount = 20;
        coinsInAccount = 100;
    }

    public int GetGemsInAccount() => gemsInAccount;
    public int GetCoinsInAccount() => coinsInAccount;
    public void IncrementGems(int gems)
    {
        gemsInAccount += gems;
        UIService.Instance.RefreshPlayerStats();
    }

    public void DecrementGems(int gems)
    {
        gemsInAccount -= gems;
        UIService.Instance.RefreshPlayerStats();
    }

    public void IncrementCoins(int coins)
    {
        coinsInAccount += coins;
        UIService.Instance.RefreshPlayerStats();
    }

    public void DecrementCoins(int coins)
    {
        coinsInAccount -= coins;
        UIService.Instance.RefreshPlayerStats();
    }
}
