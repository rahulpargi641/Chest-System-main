using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestModel ChestModel { get; private set; }
    public ChestView ChestView { get; private set; }

    private IChestState currentState;
    private ChestLockedState chestLocked;
    private ChestUnlockingState chestUnlocking;
    private ChestUnlockedState chestUnlocked;

    public ChestController(ChestModel chestModel, ChestView chestPrefab)
    {
        this.ChestModel = chestModel;
        this.ChestView = GameObject.Instantiate<ChestView>(chestPrefab);

        ChestModel.SetController(this);
        ChestView.SetController(this);

        chestLocked = new ChestLockedState(this);
        chestUnlocking = new ChestUnlockingState(this);
        chestUnlocked = new ChestUnlockedState(this);
        currentState = chestLocked;
    }

    private void ChangeState()
    {
        switch (currentState.GetChestState())
        {
            case ChestState.LOCKED:
                currentState = chestUnlocking;
                break;
            case ChestState.UNLOCKING:
                currentState = chestUnlocked;
                break;
        }
    }
    public void UnlockNow()
    {
        //reduce number of gems from account;
        currentState = chestUnlocked;
    }
    public void StartUnlocking()
    {

    }
}