public interface IStateMachine
{
    void ChangeState(IState newState);
    void Update();
    IState GetCurrentState();
}
