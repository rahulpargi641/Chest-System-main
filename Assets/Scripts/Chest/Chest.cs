using UnityEngine;


[System.Serializable]
public class Chest
{
    [SerializeField] private ChestScriptableObject chestSO;
    [SerializeField] private int chestFindingProbability;

    private ChestModel chestModel;

    public void SetModel(ChestModel model) => chestModel = model;
    public ChestModel GetModel() => chestModel;
    public ChestScriptableObject GetChestSO() => chestSO;
    public int GetFindingProbability() => chestFindingProbability;
}
