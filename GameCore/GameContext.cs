namespace GameCore;

public class GameContext
{
    private readonly GameApplication _gameApplication;

    public int DeltaTimeMS => _gameApplication.TargetDeltaTimeMS;
    public float DeltaTimeSeconds => _gameApplication.DeltaTimeSeconds;
    public IServiceCollection Services => _gameApplication.Services;

    public GameContext(GameApplication gameApplication)
    {
        _gameApplication = gameApplication;
    }

    public void Stop() => _gameApplication.Stop();
}