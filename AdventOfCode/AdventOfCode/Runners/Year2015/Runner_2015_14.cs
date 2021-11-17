using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_14.
  /// </summary>
  public class Runner_2015_14
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_14"/> class.
    /// </summary>
    public Runner_2015_14()
      : base(2015, 14)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      var reindeerArray = inputLines.Select(x => new Reindeer(x)).ToArray();
      for (int i = 0; i < 2503; i++)
      {
        foreach (var reindeer in reindeerArray)
        {
          reindeer.Update();
        }

        int distance = 0;
        foreach (var reindeer in reindeerArray.OrderByDescending(x => x.Distance))
        {
          if (distance == 0)
          {
            distance = reindeer.Distance;
          }

          if (distance == reindeer.Distance)
          {
            reindeer.Score++;
          }
        }
      }

      int part1 = reindeerArray.Select(x => x.Distance).Max();
      int part2 = reindeerArray.Select(x => x.Score).Max();

      return (part1.ToString(), part2.ToString());
    }

    /// <summary>
    /// The reindeer.
    /// </summary>
    private class Reindeer
    {
      /// <summary>
      /// Speed.
      /// </summary>
      private int speed;

      /// <summary>
      /// Running time.
      /// </summary>
      private int runningTime;

      /// <summary>
      /// Resting time.
      /// </summary>
      private int restingTime;

      /// <summary>
      /// Whether running currently.
      /// </summary>
      private bool running;

      /// <summary>
      /// Timer.
      /// </summary>
      private int timer;

      /// <summary>
      /// Gets the name.
      /// </summary>
      public string Name { get; private set; }

      /// <summary>
      /// Gets the distance.
      /// </summary>
      public int Distance { get; private set; }

      /// <summary>
      /// Gets or sets the score.
      /// </summary>
      public int Score { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Reindeer"/> class.
      /// </summary>
      /// <param name="description">The description.</param>
      public Reindeer(string description)
      {
        string[] parts = description.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim('.')).ToArray();
        this.Name = parts[0];
        this.speed = int.Parse(parts[3]);
        this.runningTime = int.Parse(parts[6]);
        this.restingTime = int.Parse(parts[^2]);

        this.running = true;
        this.ResetTimer();
      }

      /// <summary>
      /// Updates the.
      /// </summary>
      public void Update()
      {
        if (this.running)
        {
          this.Distance += this.speed;
        }

        this.timer--;
        if (this.timer <= 0)
        {
          this.running = !this.running;
          ResetTimer();
        }
      }

      /// <summary>
      /// Resets the timer.
      /// </summary>
      private void ResetTimer()
      {
        this.timer = this.running ? this.runningTime : this.restingTime;
      }
    }
  }
}