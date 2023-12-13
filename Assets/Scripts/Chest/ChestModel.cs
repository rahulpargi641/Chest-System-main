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

    public ChestModel(ChestSO chestSO)
    {
        ChestClosedImage = chestSO.ChestClosedImage;
        ChestOpenImage = chestSO.ChestOpenImage;

        ChestFindingProbability = chestSO.ChestFindingProbability;
        UnlockDurationMinutes = chestSO.UnlockDurationMinutes;
        TimeReductionByGemSeconds = chestSO.TimeReductionByGemSeconds;

        CoinsMin = chestSO.CoinsMin;
        CoinsMax = chestSO.CoinsMax;
        GemsMin = chestSO.GemsMin;
        GemsMax = chestSO.GemsMax;
    }
}