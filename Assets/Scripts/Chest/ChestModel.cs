using UnityEngine;

public class ChestModel
{
    public Sprite ChestClosedImage { get; private set; }
    public Sprite ChestOpenImage { get; private set; }
    public int ChestFindingProbability { get; private set; }
    public int UnlockDurationMinutes { get; private set; }
    public float TimeReductionByGemSeconds { get; private set; }
    public int CoinsMin { get; private set; }
    public int CoinsMax { get; private set; }
    public int GemsMin { get; private set; }
    public int GemsMax { get; private set; }

    public ChestModel(ChestConfig chestConfig)
    {
        ChestClosedImage = chestConfig.ChestClosedImage;
        ChestOpenImage = chestConfig.ChestOpenImage;

        ChestFindingProbability = chestConfig.ChestFindingProbability;
        UnlockDurationMinutes = chestConfig.UnlockDurationMinutes;
        TimeReductionByGemSeconds = chestConfig.TimeReductionByGemSeconds;

        CoinsMin = chestConfig.CoinsMin;
        CoinsMax = chestConfig.CoinsMax;
        GemsMin = chestConfig.GemsMin;
        GemsMax = chestConfig.GemsMax;
    }
}