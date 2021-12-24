using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day17
{
  /// <summary>
  /// The runner_2021_17.
  /// </summary>
  public class Runner_2021_17
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_17"/> class.
    /// </summary>
    public Runner_2021_17()
      : base(2021, 17)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var target = new Target(inputString);
      int part1Velocity = FindMaximumVerticalVelocity(target.Bottom);
      part1Velocity = FinetuneDunkVertical(target, part1Velocity);
      int part1 = FindMaximumYValue(part1Velocity);

      var velocityDomain = new Target
      {
        Left = FindMinimumHorizontalVelocity(target.Left),
        Right = FindMaximumHorizontalVelocity(target.Right),
        Top = part1Velocity,
        Bottom = FindMinimumVerticalVelocity(target.Bottom)
      };

      int part2 = IterateOverDomain(velocityDomain).Where(v => SimulateInitialXAndY(target, v)).Count();

      return (part1, part2);
    }

    /// <summary>
    /// Iterates the over domain.
    /// </summary>
    /// <param name="velocityDomain">The velocity domain.</param>
    /// <returns>A list of (int xVelocity, int yVelocity).</returns>
    private static IEnumerable<(int xVelocity, int yVelocity)> IterateOverDomain(Target velocityDomain)
    {
      int count = 0;
      for (int xVelocity = velocityDomain.Left; xVelocity <= velocityDomain.Right; xVelocity++)
      {
        for (int yVelocity = velocityDomain.Bottom; yVelocity <= velocityDomain.Top; yVelocity++)
        {
          count++;
          yield return (xVelocity, yVelocity);
        }
      }
    }

    /// <summary>
    /// Finetunes the dunk vertical.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="velocity">The velocity.</param>
    /// <returns>An int.</returns>
    private static int FinetuneDunkVertical(Target target, int velocity)
    {
      while (!SimulateY(target, velocity))
      {
        velocity--;
      }

      return velocity;
    }

    /// <summary>
    /// Simulates the y velocity.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="velocity">The velocity.</param>
    /// <returns>A bool.</returns>
    private static bool SimulateY(Target target, int velocity)
    {
      int y = 0;
      while (y >= target.Bottom)
      {
        y += velocity;
        velocity--;

        if (y <= target.Top && y >= target.Bottom)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Simulates the initial x and y.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="initialVelocity">The initial velocity.</param>
    /// <returns>A bool.</returns>
    private static bool SimulateInitialXAndY(Target target, (int xVelocity, int yVelocity) initialVelocity)
    {
      int x = 0;
      int y = 0;
      (int xVelocity, int yVelocity) = initialVelocity;
      while (x <= target.Right && y >= target.Bottom)
      {
        x += xVelocity;
        y += yVelocity;
        yVelocity--;
        if (xVelocity > 0)
        {
          xVelocity--;
        }

        if (xVelocity < 0)
        {
          xVelocity++;
        }

        if (x >= target.Left && x <= target.Right && y <= target.Top && y >= target.Bottom)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Finds the minimum horizontal velocity.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <returns>An int.</returns>
    private static int FindMinimumHorizontalVelocity(int left)
    {
      int value = 0;
      int step = 0;
      while (value < left)
      {
        value += step;
        step++;
      }

      return step - 1;
    }

    /// <summary>
    /// Finds the maximum horizontal velocity.
    /// </summary>
    /// <param name="right">The right.</param>
    /// <returns>An int.</returns>
    private static int FindMaximumHorizontalVelocity(int right)
    {
      return right;
    }

    /// <summary>
    /// Finds the maximum vertical velocity.
    /// </summary>
    /// <param name="bottom">The bottom.</param>
    /// <returns>An int.</returns>
    private static int FindMaximumVerticalVelocity(int bottom)
    {
      return Math.Abs(bottom);
    }

    /// <summary>
    /// Finds the minimum vertical velocity.
    /// </summary>
    /// <param name="bottom">The bottom.</param>
    /// <returns>An int.</returns>
    private static int FindMinimumVerticalVelocity(int bottom)
    {
      return bottom;
    }

    /// <summary>
    /// Finds the maximum y value.
    /// </summary>
    /// <param name="velocity">The part1 y.</param>
    /// <returns>An int.</returns>
    private int FindMaximumYValue(int velocity)
    {
      int y = 0;
      int maxY = 0;
      while (y == maxY)
      {
        y += velocity;
        if (y > maxY)
        {
          maxY = y;
        }

        velocity--;
      }

      return maxY;
    }
  }
}