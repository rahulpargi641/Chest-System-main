using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem
{
    public class ChestService : MonoSingletonGeneric<ChestService>
    {
        public ChestController ChestController { get; private set; }

        [SerializeField] private List<ChestRarity> chestList;
        [SerializeField] private ChestView chestPrefab;
        [SerializeField] private Button createChestButton;
        [SerializeField] private Canvas parentcanvas;

        private ChestModel chestModel;
        private ChestView chestView;

        private void Start()
        {
            chestList.Sort((p1, p2) => p1.GetProbability().CompareTo(p2.GetProbability()));
            Debug.Log(chestList[0]);

            createChestButton.onClick.AddListener(CreateRandomChest);
        }

        private void CreateRandomChest()
        {
            ChestSlot slot = SlotService.Instance.GetVacantSlot();
            if (slot == null)
            {
                return;
            }

            int randomNumber = Random.Range(1, 101);
            ChestScriptableObject chestObject = null;

            foreach (var i in chestList)
            {
                if (randomNumber >= i.GetProbability())
                {
                    chestObject = i.GetChestObject();
                }
            }

            chestModel = new ChestModel(chestObject);
            chestView = GameObject.Instantiate<ChestView>(chestPrefab);
            chestView.transform.SetParent(parentcanvas.transform);
            chestView.SetSlot(slot);
            ChestController = new ChestController(chestModel, chestView);
        }


    }
    [System.Serializable]
    public class ChestRarity
    {
        [SerializeField] private ChestScriptableObject chestObject;
        [SerializeField] private int probabilityPercentage;

        public ChestScriptableObject GetChestObject() => chestObject;
        public int GetProbability() => probabilityPercentage;
    }
}
