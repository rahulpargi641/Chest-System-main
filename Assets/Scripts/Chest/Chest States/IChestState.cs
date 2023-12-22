
public interface IChestState
{
    EChestState ChestState { get; }

    public void OnEnter();
    public void OnExit();
    public void OnChestClicked();
}