using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        public void SetController(ChestController controller)
        {
            chestController = controller;
        }
    }
}
