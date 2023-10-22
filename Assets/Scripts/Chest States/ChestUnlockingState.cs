using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnlockingState : IChestState
{
    private ChestController chestController;
    public RectTransform unlockButtonRectTransform;
    private Vector2 centerOfChestPopUp = new Vector2(0, 0);
    private Vector2 unlockButtonInitialPos;

    public ChestUnlockingState(ChestController chestController)
    {
        this.chestController = chestController;
        unlockButtonRectTransform = UIService.Instance.UnlockNowRectTransform;
        unlockButtonInitialPos = unlockButtonRectTransform.anchoredPosition;
    }
    public void OnStateEnable()
    {
        chestController.ChestView.TopText.text = "Unlocking";

        //bring the Unlock button to centre of the popup
        unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;
    }
    public void ChestButtonAction()
    {

    }
    public void OnStateDisable()
    {
        unlockButtonRectTransform.anchoredPosition = unlockButtonInitialPos;
    }
    public ChestState GetChestState()
    {
        return ChestState.UNLOCKING;
    }
}
