namespace ServiceCore.Abstract;

public interface IServiceCollectionBuilder
{
    public void AddSingleton<TService>(Func<object> func);
    public void AddSingleton<TService>(object serviceObj);
    public void AddTransient<TService>(Func<object> func);
    public TService? GetService<TService>();
}