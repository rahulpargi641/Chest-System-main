
public interface IChestState
{
    public EChestState ChestState { get; }
    public int GemsToUnlock { get; }
    public void OnEnter();
    public void OnExit();
    public void OnChestClicked();
}