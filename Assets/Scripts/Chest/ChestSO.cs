using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "SriptableObjects/NewChest")]
public class ChestSO : ScriptableObject
{
    [field:SerializeField] public ChestType ChestType { get; private set; }
    [field:SerializeField] public Sprite ChestClosedImage { get; private set; }
    [field:SerializeField] public Sprite ChestOpenImage { get; private set; }
    [field:SerializeField] public int ChestFindingProbability { get; private set; }
    [field:SerializeField] public int UnlockDurationMinutes { get; private set; }
    [field:SerializeField] public float TimeReductionByGemSeconds { get; private set; } = 60f; // 1 minutes reduction by 1 gem
    [field:SerializeField] public int CoinsMin { get; private set; }
    [field:SerializeField] public int CoinsMax { get; private set; }
    [field:SerializeField] public int GemsMin { get; private set; }
    [field:SerializeField] public int GemsMax { get; private set; }
}