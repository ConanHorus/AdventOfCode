namespace AdventOfCode.Runners
{
  /// <summary>
  /// Runner base.
  /// </summary>
  public abstract class RunnerBase
  {
    /// <summary>
    /// Year.
    /// </summary>
    public readonly int Year;

    /// <summary>
    /// Day.
    /// </summary>
    public readonly int Day;

    /// <summary>
    /// Initializes a new instance of the <see cref="RunnerBase"/> class.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="day">The day.</param>
    public RunnerBase(int year, int day) => (this.Year, this.Day) = (year, day);

    /// <summary>
    /// Gets input string.
    /// </summary>
    /// <returns>Input string.</returns>
    public string GetInputString()
    {
      return Data.ReadAllText(this.Year, this.Day);
    }

    /// <summary>
    /// Gets input lines.
    /// </summary>
    /// <returns>Input lines.</returns>
    public string[] GetInputLines()
    {
      return Data.ReadAllLines(this.Year, this.Day);
    }

    /// <summary>
    /// Runs code.
    /// </summary>
    /// <param name="inputString">Input string.</param>
    /// <param name="inputLines">Input lines.</param>
    /// <returns>Result.</returns>
    public abstract (object? part1, object? part2) Run(string inputString, string[] inputLines);
  }
}