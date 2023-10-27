using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestModel ChestModel { get; private set; }
    public ChestView ChestView { get; private set; }
    public ChestController(ChestModel chestModel, ChestView chestView)
    {
        ChestModel = chestModel;
        this.ChestView = GameObject.Instantiate<ChestView>(chestView);

        ChestModel.SetController(this);
        ChestView.SetController(this);
    }
}