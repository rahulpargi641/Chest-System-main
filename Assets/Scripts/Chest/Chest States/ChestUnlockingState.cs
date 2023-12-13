using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestUnlockingState : IChestState
{
    private ChestController controller;

    private Button unlockNowButton;
    private RectTransform unlockButtonRectTransform;
    private TextMeshProUGUI unlockNowText;

    private int timeLeftUntilUnlockSeconds; 

    private Vector2 centerOfChestPopUp = new Vector2(0, 0);
    private Coroutine unlockingCoroutine;

    public ChestUnlockingState(ChestController controller)
    {
        this.controller = controller;

        unlockNowButton = UIService.Instance.UnlockNowButton;
        unlockButtonRectTransform = UIService.Instance.UnlockNowButtonRectTransform;
        unlockNowText = UIService.Instance.UnlockNowText;

        timeLeftUntilUnlockSeconds = controller.Model.UnlockDurationMinutes * 60;
    }

    public void OnStateEnter()
    {
        UpdateCurrentStateText();

        unlockingCoroutine = controller.View.StartCoroutine(ChestUnlockTimer()); // Start Unlock Timer Coroutine

        AudioService.Instance.PlaySound(SoundType.StartUnlocking);
    }

    private void UpdateCurrentStateText()
    {
        controller.View.CurrentChestStateText.text = "Unlocking";
    }

    // get called when Chest is clicked on 
    public void ChestButtonClickedOn()
    {
        ChestPopupSetup();
        UIService.Instance.EnableChestPopUp();

        AudioService.Instance.PlaySound(SoundType.ChestClickedOn);
    }

    private void ChestPopupSetup()
    {
        EnableUnlockNowButton();
        unlockNowText.text = "Unlock Now: " + GetRequiredGemsToUnlock().ToString();
        unlockNowButton.onClick.AddListener(controller.UnlockNow);
    }

    public void OnStateExit()
    {
        StopChestUnlockTimerCoroutine();

        UIService.Instance.DisableChestPopUp();
    }

    private void StopChestUnlockTimerCoroutine()
    {
        if (unlockingCoroutine != null)
        {
            controller.View.StopCoroutine(unlockingCoroutine);
            unlockingCoroutine = null;  // Reset the coroutine reference
            Debug.Log("Timer Coroutine stopped");
        }
    }

    // brings the Unlock Now button to centre of the popup
    private void EnableUnlockNowButton()
    {
        unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;
        unlockNowButton.gameObject.SetActive(true);
    }

    public IEnumerator ChestUnlockTimer()
    {
        Debug.Log("Coroutine Started");

        while (timeLeftUntilUnlockSeconds >= 0)
        {
            UpdateTimeLeftText();

            yield return new WaitForSeconds(1);
            timeLeftUntilUnlockSeconds--;
        }

        controller.UnlockNow();
    }

    private void UpdateTimeLeftText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeftUntilUnlockSeconds);
        string timeString = timeSpan.ToString(@"hh\:mm\:ss");

        UpdateTimeLeftUntilUpdateText(timeString);
    }

    private void UpdateTimeLeftUntilUpdateText(string timeString)
    {
        controller.View.TimeLeftUntilUnlockText.text = timeString;
    }

    public int GetRequiredGemsToUnlock() =>
        Mathf.CeilToInt(timeLeftUntilUnlockSeconds / controller.Model.TimeReductionByGemSeconds);

    public EChestState GetChestState() => EChestState.UNLOCKING;
}
