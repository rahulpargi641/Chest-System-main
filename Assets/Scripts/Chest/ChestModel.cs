using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestModel
{
    private ChestController chestController;
    public Sprite ChestClosedImage { get; private set; }
    public Sprite ChestOpenImage { get; private set; }
    public int CoinsMin { get; private set; }
    public int CoinsMax { get; private set; }
    public int GemsMin { get; private set; }
    public int GemsMax { get; private set; }
    public int UnlockDurationMinutes { get; private set; }

    public ChestModel(ChestScriptableObject chestObject)
    {
        this.ChestClosedImage = chestObject.chestClosedImage;
        this.ChestOpenImage = chestObject.chestOpenImage;

        this.CoinsMin = chestObject.coinsMin;
        this.CoinsMax = chestObject.coinsMax;
        this.GemsMin = chestObject.gemsMin;
        this.GemsMax = chestObject.gemsMax;

        this.UnlockDurationMinutes = chestObject.unlockDurationMinutes;
    }
    public void SetController(ChestController controller)
    {
        this.chestController = controller;
    }
}