namespace GameCore.Abstract.GameBasicFunctional;

public interface IUpdateable : IGameFunc
{
    public void Update(GameContext context);
}
