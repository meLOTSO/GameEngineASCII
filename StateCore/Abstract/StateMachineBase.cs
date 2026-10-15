namespace StateCore.Abstract;

public abstract class StateMachineBase<TKey, TValue> where TKey : notnull
{
    protected Dictionary<TKey, List<TValue>> StateValuePairs { get; set; } = new();
}