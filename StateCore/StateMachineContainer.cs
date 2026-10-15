using StateCore.Delegates;

namespace StateCore;

public class StateMachineContainer<TKey> where TKey : notnull
{
    protected event SetStateDelegate<TKey>? OnSetState = null;

    public StateMachineContainer<TKey> AddStateMachine(SetStateDelegate<TKey> setState)
    {
        OnSetState += setState;
        return this;
    }

    public void SetState(TKey state) => OnSetState?.Invoke(state);
}