using UnityEngine;

public class ChestModel
{
    public Sprite ChestClosedImage { get; private set; }
    public Sprite ChestOpenImage { get; private set; }
    public int CoinsMin { get; private set; }
    public int CoinsMax { get; private set; }
    public int GemsMin { get; private set; }
    public int GemsMax { get; private set; }
    public int UnlockDurationMinutes { get; private set; }

    public ChestModel(ChestScriptableObject chestObject)
    {
        ChestClosedImage = chestObject.chestClosedImage;
        ChestOpenImage = chestObject.chestOpenImage;

        CoinsMin = chestObject.coinsMin;
        CoinsMax = chestObject.coinsMax;
        GemsMin = chestObject.gemsMin;
        GemsMax = chestObject.gemsMax;

        UnlockDurationMinutes = chestObject.unlockDurationMinutes;
    }
}