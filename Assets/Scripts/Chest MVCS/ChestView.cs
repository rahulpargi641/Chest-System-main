using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        public Button ChestButton { get { return chestButton; } private set { } }
        public TextMeshProUGUI TopText { get { return topText; } private set { } }
        public TextMeshProUGUI BottomText { get { return bottomText; } private set { } }
        public Image ChestImage { get { return chestImage; } private set { } }
        public int TimeRemainingSeconds { get; private set; }

        [SerializeField] private RectTransform chestRectTransform;
        [SerializeField] private Image chestImage;
        [SerializeField] private Button chestButton;
        [SerializeField] private TextMeshProUGUI topText;
        [SerializeField] private TextMeshProUGUI bottomText;

        private ChestController chestController;
        private ChestSlot slot;
        public void SetController( ChestController controller )
        {
            this.chestController = controller;
        }

        //This method requires both chest and slot to be anchored about same anchor.
        public void SetSlot(ChestSlot slot )
        {
            this.slot = slot;
            chestRectTransform.anchoredPosition = slot.GetRectTransform().anchoredPosition;
        }
        public void DestroyChest( )
        {
            slot.SetIsEmpty( true );
            chestButton.onClick.RemoveAllListeners( );
            chestController.RemoveView( );
        }

        public IEnumerator CountDown()
        {
            while ( TimeRemainingSeconds >= 0 )
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds( TimeRemainingSeconds );
                string timeString = timeSpan.ToString( @"hh\:mm\:ss" );
                BottomText.text = timeString;

                TimeRemainingSeconds--;
                yield return new WaitForSeconds( 1 );
            }
            chestController.UnlockNow( );
        }

        public void InitialSettings( )
        {
            chestImage.sprite = chestController.ChestModel.ChestClosedImage;
            TimeRemainingSeconds = chestController.ChestModel.UnlockDurationMinutes * 60;
            chestButton.onClick.AddListener( chestController.ChestButtonAction );
        }

        private void Awake( )
        {
            transform.SetParent( ChestService.Instance.ChestParentTransform );
            chestRectTransform.localScale = new Vector3( 1, 1, 1 );
        }
    }

}
