using System.Diagnostics;
using GameCore.Abstract.GameBasicFunctional;
using ServiceCore;

namespace GameCore;

public class GameApplication : GameBase
{
    private bool _run = false;

    public IServiceCollection Services { get; init; } = new ServiceCollection();
    public int TargetDeltaTimeMS { get; set; } = 1000 / 60; // заданная дельта
    public float DeltaTimeSeconds { get; private set; } // фактическая дельта
    public GameContext Context { get; private set; }

    public Action? PreFrame { get; set; }

    public GameApplication(GameBase gameBase) : this()
    {
        UpdateCollection = gameBase.UpdateCollection;
        OnKeyPressCollection = gameBase.OnKeyPressCollection;
        DrawCollection = gameBase.DrawCollection;
    }
    public GameApplication() => Context = new GameContext(this);

    private void Update()
    {
        foreach (var u in UpdateCollection.ToArray())
        {
            u.Invoke(Context);
        }
    }

    private void Draw()
    {
        Console.SetCursorPosition(0, 0);
        foreach (var d in DrawCollection.ToArray())
        {
            d.Invoke(Context);
        }
    }

    private void OnKeyPress()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            foreach (var c in OnKeyPressCollection.ToArray())
            {
                c.Invoke(Context, key.Key, key.Modifiers);
            }
        }
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();
        _run = true;

        long prevTimestamp = Stopwatch.GetTimestamp();

        while (_run)
        {
            long frameStartTimestamp = Stopwatch.GetTimestamp();
            DeltaTimeSeconds = (float)Stopwatch.GetElapsedTime(prevTimestamp).TotalSeconds;
            prevTimestamp = frameStartTimestamp;

            PreFrame?.Invoke();
            OnKeyPress();
            Update();
            Draw();

            TimeSpan elapsed = Stopwatch.GetElapsedTime(frameStartTimestamp);
            int sleepTime = TargetDeltaTimeMS - (int)elapsed.TotalMilliseconds;

            if (sleepTime > 0)
                Thread.Sleep(sleepTime);
        }

        Console.CursorVisible = true;
    }

    public void Stop() => _run = false;
}