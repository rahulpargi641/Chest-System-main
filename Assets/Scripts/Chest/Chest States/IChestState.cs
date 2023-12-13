
public interface IChestState
{
    public void OnStateEnter();
    public void OnStateExit();
    public void ChestButtonClickedOn();
    public int GetRequiredGemsToUnlock();
    public EChestState GetChestState(); 
}