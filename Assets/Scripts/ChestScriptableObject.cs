using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName = "Chest", menuName = "SriptableObjects/NewChest")]
    public class ChestScriptableObject : ScriptableObject
    {
        public Sprite chestClosedImage;
        public Sprite chestOpenImage;

        public int coinsMin;
        public int coinsMax;
        public int gemsMin;
        public int gemsMax;

        public int unlockDurationMinutes;
    }
}

