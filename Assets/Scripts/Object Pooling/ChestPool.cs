using UnityEngine;

public class ChestPool : ObjectPoolGeneric<ChestView>
{
    private ChestView chestPrefab;

    public void Initialize(ChestView chestPrefab)
    {
        this.chestPrefab = chestPrefab;
    }

    public ChestView GetChest()
    {
        return GetItemFromPool(); 
    }

    protected override ChestView CreateItem() 
    { 
        return Object.Instantiate(chestPrefab); 
    }
}


