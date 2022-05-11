namespace Anvil;

public interface ISpatial
{
    int Width { get; }
    
    int Height { get; }

    public int Area => Width * Height;
    
    public Size Size => new Size(Width, Height);
}