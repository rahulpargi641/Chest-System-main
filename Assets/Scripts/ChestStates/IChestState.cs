namespace ChestSystem.Chest
{
    public interface IChestState
    {
        public void OnStateEnable( );

        public void ChestButtonAction( );

        public void OnStateDisable( );

        public ChestState GetChestState( );

        public int GetRequiredGemsToUnlock( );
    }
}
