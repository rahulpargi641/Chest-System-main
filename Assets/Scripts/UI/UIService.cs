using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoSingletonGeneric<UIService>
{
    public RectTransform UnlockNowRectTransform { get { return unlockNowRectTransform; } private set { } }

    public Vector2 UnlockButtonInitialPos { get; private set; }
    public Button UnlockNowButton { get { return unlockNowButton; } private set { } }
    public Button SetTimerButton { get { return setTimerButton; } private set { } }
    public TextMeshProUGUI GiftMessage { get { return giftMessage; } private set { } }
    public TextMeshProUGUI GiftCoinText { get { return giftCoinText; } private set { } }
    public TextMeshProUGUI GiftGemText { get { return giftGemText; } private set { } }
    public TextMeshProUGUI UnlockText { get { return unlockText; } private set { } }

    [Header("Chest Related")]
    [SerializeField] private Button createChestButton;
    [SerializeField] private GameObject rayCastBlocker;
    [SerializeField] private GameObject chestSlotsFullPopUp;
    [SerializeField] private Button closeChestSlotsFullButton;

    [Header("Chest Pop Up")]
    [SerializeField] private GameObject chestPopUp;
    [SerializeField] private Button closeChestPopUpButton;
    [SerializeField] private Button unlockNowButton;
    [SerializeField] private RectTransform unlockNowRectTransform;
    [SerializeField] private TextMeshProUGUI unlockText;
    [SerializeField] private Button setTimerButton;
    [SerializeField] private TextMeshProUGUI giftMessage;
    [SerializeField] private TextMeshProUGUI giftCoinText;
    [SerializeField] private TextMeshProUGUI giftGemText;

    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI gems;


    public static event Action OnChestPopUpClosed;

    public void RefreshPlayerStats()
    {
        coins.text = PlayerService.Instance.GetCoinsInAccount().ToString();
        gems.text = PlayerService.Instance.GetGemsInAccount().ToString();
    }
    public void EnableChestPopUp()
    {
        rayCastBlocker.SetActive(true);
        chestPopUp.SetActive(true);
    }
    public void DisableChestPopUp()
    {
        rayCastBlocker.SetActive(false);
        chestPopUp.SetActive(false);
        unlockNowButton.gameObject.SetActive(false);
        setTimerButton.gameObject.SetActive(false);
        giftMessage.gameObject.SetActive(false);

        OnChestPopUpClosed?.Invoke();
        unlockNowButton.onClick.RemoveAllListeners();
        setTimerButton.onClick.RemoveAllListeners();
    }
    public void EnableSlotsFullPopUp()
    {
        rayCastBlocker.SetActive(true);
        chestSlotsFullPopUp.SetActive(true);
    }
    private void DisableSlotsFullPopUp()
    {
        rayCastBlocker.SetActive(false);
        chestSlotsFullPopUp.SetActive(false);
    }
    private void Start()
    {
        rayCastBlocker.SetActive(false);
        chestSlotsFullPopUp.SetActive(false);
        chestPopUp.SetActive(false);
        unlockNowButton.gameObject.SetActive(false);
        setTimerButton.gameObject.SetActive(false);
        giftMessage.gameObject.SetActive(false);
        UnlockButtonInitialPos = unlockNowRectTransform.anchoredPosition;

        createChestButton.onClick.AddListener(ChestService.Instance.SpawnRandomChest);
        closeChestSlotsFullButton.onClick.AddListener(DisableSlotsFullPopUp);
        closeChestPopUpButton.onClick.AddListener(DisableChestPopUp);

        RefreshPlayerStats();
    }
}