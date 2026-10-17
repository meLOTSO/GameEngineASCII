public class Sprite
{
    public char[,] Texture { get; set; }
    public int Width => Texture.GetLength(1);
    public int Height => Texture.GetLength(0);
    public int X { get; set; }
    public int Y { get; set; }

    public Sprite(char[,] texture, int x = 0, int y = 0)
        => (Texture, X, Y) = (texture, x, y);
}