using UnityEngine;

public class ChestPool : ObjectPoolGeneric<ChestView>
{
    private ChestView view;

    public void Initialize(ChestView view)
    {
        this.view = view;
    }

    public ChestView GetChest()
    {
        return GetItemFromPool(); 
    }

    protected override ChestView CreateItem() 
    { 
        return Object.Instantiate(view); 
    }
}


