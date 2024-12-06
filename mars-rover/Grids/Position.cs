namespace mars_rover.Grids;

public class Position(int x, int y, CardinalDirection direction)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public CardinalDirection Facing { get; set; } = direction;

    public override string ToString()
    {
        return $"{X} {Y}, {Facing}";
    }
}
