namespace GameCore.Abstract.GameBasicFunctional;

public interface IControl : IGameFunc
{
    public void OnKeyPress(GameContext context, ControlContext controlContext);
}