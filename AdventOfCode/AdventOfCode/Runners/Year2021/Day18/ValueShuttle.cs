using System;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The value shuttle.
  /// </summary>
  public class ValueShuttle
  {
    /// <summary>
    /// Shuttle strategy.
    /// </summary>
    private readonly IShuttleStrategy shuttleStrategy;

    /// <summary>
    /// Payload value.
    /// </summary>
    private readonly int value;

    /// <summary>
    /// Starting point.
    /// </summary>
    private readonly SnailNumber start;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueShuttle"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="start">The start.</param>
    /// <param name="shuttleStrategy">The shuttle strategy.</param>
    public ValueShuttle(int value, SnailNumber start, IShuttleStrategy shuttleStrategy)
    {
      this.value = value;
      this.start = start;
      this.shuttleStrategy = shuttleStrategy;
    }

    /// <summary>
    /// Launches this value shuttle.
    /// </summary>
    public void Launch()
    {
      var destination = this.shuttleStrategy.TraverseTreeToDestination(this.start);
      if (destination is null)
      {
        return;
      }

      if (this.shuttleStrategy is LeftShuttleStrategy)
      {
        if (destination.RightNumber is not null)
        {
          destination.RightNumber += this.value;
          return;
        }

        destination.LeftNumber += this.value;
        return;
      }

      if (destination.LeftNumber is not null)
      {
        destination.LeftNumber += this.value;
        return;
      }

      destination.RightNumber += this.value;
    }
  }
}