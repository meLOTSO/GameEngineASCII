using GameCore;
using GameCore.Extensions;
using GameCore.Builder;

var gameBuilder = new GameApplicationBuilder();

gameBuilder.Services.AddStateMachine(smBuilder =>
{
    smBuilder.AddValueForState("menu", GameScene.FromEntity(new Entity()));
});

var game = gameBuilder.Build();

game.UseStateMachine();

game.SetState("menu");

game.Run();