using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class UIService : MonoSingletonGeneric<UIService>
    {
        public RectTransform UnlockNowRectTransform { get { return unlockNowRectTransform; } private set { } }
        public Button UnlockNowButton { get { return unlockNowButton; } private set { } }
        public Button SetTimerButton { get { return setTimerButton; } private set { } }

        [SerializeField] private Button createChestButton;
        [SerializeField] private GameObject rayCastBlocker;
        [SerializeField] private GameObject chestSlotsFullPopUp;
        [SerializeField] private Button closeChestSlotsFull;
        [SerializeField] private GameObject chestPopUp;
        [SerializeField] private Button closeChestPopUp;
        [SerializeField] private Button unlockNowButton;
        [SerializeField] private RectTransform unlockNowRectTransform;
        [SerializeField] private Button setTimerButton;
        [SerializeField] private TextMeshProUGUI giftText;

        private void Start()
        {
            rayCastBlocker.SetActive(false);
            chestSlotsFullPopUp.SetActive(false);
            chestPopUp.SetActive(false);

            createChestButton.onClick.AddListener(ChestService.Instance.CreateRandomChest);
            closeChestSlotsFull.onClick.AddListener(DisableSlotsFullPopUp);
            closeChestPopUp.onClick.AddListener(DisableChestPopUp);
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

        public void EnableChestPopUp()
        {
            rayCastBlocker.SetActive(true);
            chestPopUp.SetActive(true);
        }

        private void DisableChestPopUp()
        {
            unlockNowButton.gameObject.SetActive(false);
            setTimerButton.gameObject.SetActive(false);
        }
    }
