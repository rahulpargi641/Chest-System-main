using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockedState : IChestState
{
    private ChestController chestController;

    public ChestUnlockedState(ChestController chestController)
    {
        this.chestController = chestController;
    }
    public void OnStateEnable()
    {
        chestController.ChestView.TopText.text = "Unlocked";
    }
    public void ChestButtonAction()
    {

    }
    public void OnStateDisable()
    {

    }
    public ChestState GetChestState()
    {
        return ChestState.UNLOCKED;
    }
}