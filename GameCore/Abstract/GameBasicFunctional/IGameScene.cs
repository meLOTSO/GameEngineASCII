using GameCore.Delegates;

namespace GameCore.Abstract.GameBasicFunctional;

public interface IGameScene
{
    public OnKeyPressDelegate? OnKeyPress { get; set; }
    public UpdateDelegate? Update { get; set; }
    public DrawDelegate? Draw { get; set; }
}
