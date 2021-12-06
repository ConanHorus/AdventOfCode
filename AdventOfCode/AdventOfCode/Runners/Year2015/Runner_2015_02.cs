using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_2.
  /// </summary>
  public class Runner_2015_02
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_02"/> class.
    /// </summary>
    public Runner_2015_02()
      : base(2015, 2)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var gifts = inputLines.Select(x => new Gift(x));
      int paperSquareFeet = 0;
      int ribbon = 0;

      foreach (var gift in gifts)
      {
        paperSquareFeet += gift.PaperSquareFeet;
        ribbon += gift.RibbonLength;
      }

      return (paperSquareFeet, ribbon);
    }

    /// <summary>
    /// The side.
    /// </summary>
    private readonly struct Side
    {
      /// <summary>
      /// Width.
      /// </summary>
      private readonly int width;

      /// <summary>
      /// Height.
      /// </summary>
      private readonly int height;

      /// <summary>
      /// Gets the area.
      /// </summary>
      public int Area => this.width * this.height;

      /// <summary>
      /// Gets the perimeter.
      /// </summary>
      public int Perimeter => (this.width * 2) + (this.height * 2);

      /// <summary>
      /// Initializes a new instance of the <see cref="Side"/> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public Side(int width, int height) => (this.width, this.height) = (width, height);
    }

    /// <summary>
    /// The gift.
    /// </summary>
    private class Gift
    {
      /// <summary>
      /// Side a.
      /// </summary>
      private readonly Side a;

      /// <summary>
      /// Side b.
      /// </summary>
      private readonly Side b;

      /// <summary>
      /// Side c.
      /// </summary>
      private readonly Side c;

      /// <summary>
      /// Volume.
      /// </summary>
      private readonly int volume;

      /// <summary>
      /// Gets the paper square feet.
      /// </summary>
      public int PaperSquareFeet => (2 * this.a.Area) + (2 * this.b.Area) + (2 * this.c.Area) + this.MinimumArea;

      /// <summary>
      /// Gets the ribbon length.
      /// </summary>
      public int RibbonLength => Math.Min(this.a.Perimeter, Math.Min(this.b.Perimeter, this.c.Perimeter)) + this.volume;

      /// <summary>
      /// Gets the minimum area.
      /// </summary>
      private int MinimumArea => Math.Min(this.a.Area, Math.Min(this.b.Area, this.c.Area));

      /// <summary>
      /// Initializes a new instance of the <see cref="Gift"/> class.
      /// </summary>
      /// <param name="line">The line.</param>
      public Gift(string line)
      {
        int[] parts = line.Split('x').Select(x => int.Parse(x)).ToArray();
        this.a = new Side(parts[0], parts[1]);
        this.b = new Side(parts[1], parts[2]);
        this.c = new Side(parts[0], parts[2]);
        this.volume = parts[0] * parts[1] * parts[2];
      }
    }
  }
}