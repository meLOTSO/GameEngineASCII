using GameCore;
using GameCore.Abstract.GameBasicFunctional;
using GameCore.Extensions;

public class Entity : IControl, IDrawable
{
    private int _currentIndex = 0;

    public List<string> Menu { get; set; } = [];
    public int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            if (value >= Menu.Count)
                _currentIndex = 0;
            else if (value < 0)
                _currentIndex = Menu.Count - 1;
            else
                _currentIndex = value;
        }
    }

    public Entity()
    {
        Menu = new List<string>()
        {
            "Start", "Settings", "Exit"
        };
    }

    public void OnKeyPress(GameContext context, ConsoleKey key, ConsoleModifiers modifiers)
    {
        if (key == ConsoleKey.DownArrow)
            CurrentIndex++;
        else if (key == ConsoleKey.UpArrow)
            CurrentIndex--;
        else if (key == ConsoleKey.Enter)
        {
            switch (CurrentIndex)
            {
                case 0: context.SetState("game"); break;
                case 1: context.SetState("setting"); break;
                case 2: context.SetState("exit"); break;
                default: break;
            }
        }
    }
    public void Draw(GameContext context)
    {
        for (int i = 0; i < Menu.Count; i++)
        {
            if (i == CurrentIndex)
            {
                Console.WriteLine($"> {Menu[i]}");
            }
            else
            {
                Console.WriteLine(Menu[i]);
            }
        }
    }
}