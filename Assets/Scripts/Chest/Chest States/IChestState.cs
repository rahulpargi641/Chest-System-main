
public interface IChestState
{
    public void OnEnter();
    public void OnStateExit();
    public void ChestButtonClickedOn();
    public int GetRequiredGemsToUnlock();
    public EChestState GetChestState(); 
}