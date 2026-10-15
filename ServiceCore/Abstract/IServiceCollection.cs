public interface IServiceCollection
{
    public TService? GetService<TService>();
    public TService GetServiceRequired<TService>();
}