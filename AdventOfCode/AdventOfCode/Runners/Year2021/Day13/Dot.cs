namespace AdventOfCode.Runners.Year2021.Day13
{
  public struct Dot
  {
    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Dot"/> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public Dot(int x, int y) => (this.X, this.Y) = (x, y);
  }
}