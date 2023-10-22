using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChestState
{
    public void OnStateEnable();

    public void ChestButtonAction();

    public void OnStateDisable();

    public ChestState GetChestState();
}