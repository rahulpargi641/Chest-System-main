using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestLockedState : IChestState
{
    private ChestController controller;

    //private Button unlockNowButton;
    //private Button startUnlockingButton;

    //private RectTransform unlockNowButtonRectTransform;
    private Vector2 unlockButtonInitialPos;
    //private TextMeshProUGUI unlockNowText;

    private int timeLeftUntilUnlock; // in minutes
    private Vector2 centerOfChestPopUp = new Vector2(0, 0);

    public ChestLockedState(ChestController controller)
    {
        this.controller = controller;

        //unlockNowButton = UIService.Instance.UnlockNowButton;
        //startUnlockingButton = UIService.Instance.StartUnlockingButton;

        //unlockNowButtonRectTransform = UIService.Instance.UnlockNowButtonRectTransform;
        unlockButtonInitialPos = UIService.Instance.UnlockNowButtonInitialPos;
        //unlockNowText = UIService.Instance.UnlockNowText;
    }

    public void OnEnter()
    {
        UpdateChestVisualAndInfoTexts();
    }

    private void UpdateChestVisualAndInfoTexts() 
    {
        controller.View.ChestImage.sprite = controller.Model.ChestClosedImage; // updates chest visual
        UpdateChestInfoTexts();
    }

    private void UpdateChestInfoTexts() // Updates Current Chest State and Time remaining until unlock texts 
    {
        controller.View.CurrentChestStateText.text = "Locked";
        timeLeftUntilUnlock = controller.Model.UnlockDurationMinutes;

        controller.View.TimeLeftUntilUnlockText.text = (timeLeftUntilUnlock < 60) ? timeLeftUntilUnlock.ToString() + " Min" :
                                                            (timeLeftUntilUnlock / 60).ToString() + " Hr";
    }

    // get called when Chest is clicked on 
    public void ChestButtonClickedOn()
    {
        ChestPopupSetUp();
        UIService.Instance.EnableChestPopUp();

        AudioService.Instance.PlaySound(SoundType.ChestClickedOn);
    }

    public void OnStateExit()
    {
        UIService.Instance.DisableChestPopUp();
    }

    private void ChestPopupSetUp()
    {
        //EnableUnlockNowButton();
        UIService.Instance.EnableUnlockNowButton(unlockButtonInitialPos, GetRequiredGemsToUnlock());

        EnableStartUnlockingButtonIf();

        //AddButtonsListeners();
        UIService.Instance.AddButtonsListeners(controller);
    }

    //private void EnableUnlockNowButton()
    //{
    //    unlockNowButtonRectTransform.anchoredPosition = unlockButtonInitialPos;
    //    unlockNowText.text = "Unlock Now: " + GetRequiredGemsToUnlock().ToString();
    //    unlockNowButton.gameObject.SetActive(true);
    //}

    private void EnableStartUnlockingButtonIf() // Start Unlocking button is enabled only if no other chests are currently unlocking
    {
        if (SlotService.Instance.IsAnyChestUnlocking() == false)
            //startUnlockingButton.gameObject.SetActive(true);
            UIService.Instance.EnableStartUnlockingButton();
        else
            //unlockNowButtonRectTransform.anchoredPosition = centerOfChestPopUp; // Start Unlocking button doesn't get enabled
            UIService.Instance.DisableStartUnlockingButton();
    }

    //private void AddButtonsListeners()
    //{
    //    unlockNowButton.onClick.AddListener(controller.UnlockNow);
    //    startUnlockingButton.onClick.AddListener(controller.StartUnlocking);
    //}

    public int GetRequiredGemsToUnlock() =>
        Mathf.CeilToInt(timeLeftUntilUnlock * 60 / controller.Model.TimeReductionByGemSeconds);

    public EChestState GetChestState() => EChestState.LOCKED;
}