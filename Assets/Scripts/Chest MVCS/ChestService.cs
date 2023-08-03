using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class ChestService : MonoSingletonGeneric<ChestService>
    {
        [SerializeField] private List<ChestRarity> chestList;
        
        [SerializeField] private Transform chestParentTransform;

        public Transform ChestParentTransform { get { return chestParentTransform; } private set { } }


        /*
         * Select Model according to Probability.
         * Select Controller according to slot available.
         * Get View from object pool. 
         * Assign View and Model to the Controller.
         */
        public void SpawnRandomChest( )
        {
            ChestSlot slot = SlotService.Instance.GetVacantSlot( );
            if ( slot == null )
            {
                UIService.Instance.EnableSlotsFullPopUp( );
                return;
            }

            int randomNumber = Random.Range( 1, 101 );
            ChestRarity chestRarity = null;
            int totalProbability = 100;
            foreach ( var i in chestList )
            {
                if ( randomNumber >= ( totalProbability - i.GetProbability( ) ) )
                {
                    chestRarity = i;
                    break;
                }
                else
                {
                    totalProbability -= i.GetProbability( );
                }
            }
            ChestController controller = slot.GetController( );
            controller.SetModel( chestRarity.GetModel( ) );
            controller.SetChestView( );
            controller.ChestView.SetSlot( slot );
        }
        public bool IsAnyChestUnlocking( )
        {
            int numberOfSlots = SlotService.Instance.GetNumberOfSlots( );
            for ( int i = 0; i < numberOfSlots; i++ )
            {
                ChestSlot slot = SlotService.Instance.GetSlotAtPos( i );
                if ( slot.GetController( ).ChestState == ChestState.UNLOCKING )
                {
                    return true;
                }
            }
            return false;
        }

        private void Start( )
        {
            chestList.Sort( ( p1, p2 ) => p1.GetProbability( ).CompareTo( p2.GetProbability( ) ) );
            CreateChestModels( );
            CreateChestControllers( );
        }

        /*
         * Create a chest model for each type of chest (scriptable object). 
         * No of models = No of types of chest
         */
        private void CreateChestModels( )
        {
            foreach(var i in chestList )
            {
                ChestModel model = new ChestModel( i.GetChestObject() );
                i.SetModel( model );
            }
        }

        /*
         * Create a chest controller for each slot.
         * No of controllers = No of slots
         */
        private void CreateChestControllers( )
        {
            ChestScriptableObject chestObject = null;
            int numberOfSlots = SlotService.Instance.GetNumberOfSlots( );
            for (int i=0; i< numberOfSlots; i++ )
            {
                ChestController chestController = new ChestController();
                SlotService.Instance.GetSlotAtPos( i ).SetController( chestController );
            }
        }

        

    }
}
