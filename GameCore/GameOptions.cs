using GameCore.Delegates;

public record class GameOptions(
    List<UpdateDelegate> UpdateCollection,
    List<OnKeyPressDelegate> OnKeyPressCollection,
    List<DrawDelegate> DrawCollection
);