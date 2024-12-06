namespace mars_rover.Grids;

public class Grid
{
    public int Length { get; init; }
    public int Width { get; init; }

    public IMappable?[,] Map;

    public Grid(int length, int width)
    {
        Length = length;
        Width = width;
        Map = new IMappable[Length, Width];
    }

    public (bool, GridCheckOutcome) CheckPosition(Position position)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Length) return (false, GridCheckOutcome.OUT_OF_BOUNDS);
        if (Map[position.X, position.Y] != null) return (false, GridCheckOutcome.OCCUPIED);
        return (true, GridCheckOutcome.VALID);
    }

    public void Add(IMappable iMappable)
    {
        Map[iMappable.CurrentPosition.X, iMappable.CurrentPosition.Y] = iMappable;    
    }

    public void Remove(Position position)
    {
        Map[position.X, position.Y] = null;
    }
}

public enum GridCheckOutcome
{
    VALID,
    OUT_OF_BOUNDS,
    OCCUPIED

}
