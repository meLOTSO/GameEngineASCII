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
    public GameContext GameContext { get; private set; }
    public ControlContext ControlContext { get; set; } = new();
    public DrawContext DrawContext { get; set; } = new();

    public Action? PreFrame { get; set; }

    public GameApplication(GameBase gameBase) : this()
    {
        UpdateCollection = gameBase.UpdateCollection;
        OnKeyPressCollection = gameBase.OnKeyPressCollection;
        DrawCollection = gameBase.DrawCollection;
    }
    public GameApplication()
    {
        GameContext = new GameContext(this);
    }

    private void Update()
    {
        foreach (var u in UpdateCollection)
        {
            u.Invoke(GameContext);
        }
    }

    private void Draw()
    {
        Console.SetCursorPosition(0, 0);
        foreach (var d in DrawCollection)
        {
            d.Invoke(DrawContext);
        }
    }

    private void OnKeyPress()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            ControlContext.Pressed = true;
            ControlContext.Key = key.Key;
            ControlContext.Modifiers = key.Modifiers;
            foreach (var c in OnKeyPressCollection)
            {
                c.Invoke(GameContext, ControlContext);
            }
        }
        ControlContext.Pressed = false;
        ControlContext.Key = default;
        ControlContext.Modifiers = default;
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