using System;
using System.Collections.Generic;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The snail number.
  /// </summary>
  public class SnailNumber
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SnailNumber"/> class.
    /// </summary>
    /// <param name="line">The line.</param>
    public SnailNumber(string line)
      : this(line, 0)
    {
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="SnailNumber"/> class from being created.
    /// </summary>
    private SnailNumber()
    {
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="SnailNumber"/> class from being created.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <param name="ptr">The ptr.</param>
    private SnailNumber(string line, int ptr)
    {
      bool left = true;
      int depth = 0;
      for (int iPtr = ptr; iPtr < line.Length; iPtr++)
      {
        char c = line[iPtr];
        if (c == '[')
        {
          depth++;
          if (depth == 2)
          {
            var snailNumber = new SnailNumber(line, iPtr)
            {
              Parent = this
            };

            if (left)
            {
              this.LeftSnailNumber = snailNumber;
            }
            else
            {
              this.RightSnailNumber = snailNumber;
            }
          }
        }

        if (c == ']')
        {
          depth--;
          if (depth == 0)
          {
            return;
          }
        }

        if (c == ',' && depth == 1)
        {
          left = false;
        }

        if (c >= '0' && c <= '9' && depth == 1)
        {
          if (left)
          {
            this.LeftNumber = c - '0';
          }
          else
          {
            this.RightNumber = c - '0';
          }
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether exploded.
    /// </summary>
    public bool Exploded { get; private set; }

    /// <summary>
    /// Gets the parent.
    /// </summary>
    public SnailNumber? Parent { get; private set; }

    /// <summary>
    /// Gets or sets the left number.
    /// </summary>
    public int? LeftNumber { get; set; }

    /// <summary>
    /// Gets or sets the right number.
    /// </summary>
    public int? RightNumber { get; set; }

    /// <summary>
    /// Gets the left snail number.
    /// </summary>
    public SnailNumber? LeftSnailNumber { get; private set; }

    /// <summary>
    /// Gets the right snail number.
    /// </summary>
    public SnailNumber? RightSnailNumber { get; private set; }

    public static SnailNumber operator +(SnailNumber left, SnailNumber right)
    {
      var root = new SnailNumber() { LeftSnailNumber = left, RightSnailNumber = right };
      left.Parent = root;
      right.Parent = root;

      while (root.Explode() || root.Split())
      {
      }

      return root;
    }

    /// <summary>
    /// Calculates the magnitude.
    /// </summary>
    /// <returns>A long.</returns>
    public long CalculateMagnitude()
    {
      long left;
      long right;

      if (this.LeftSnailNumber is not null)
      {
        left = this.LeftSnailNumber.CalculateMagnitude();
      }
      else
      {
        left = (int)this.LeftNumber!;
      }

      if (this.RightSnailNumber is not null)
      {
        right = this.RightSnailNumber.CalculateMagnitude();
      }
      else
      {
        right = (int)this.RightNumber!;
      }

      return left * 3 + right * 2;
    }

    /// <summary>
    /// Attempts to explode this snail number.
    /// </summary>
    /// <returns>A bool.</returns>
    public bool Explode()
    {
      return this.Explode(0);
    }

    /// <summary>
    /// Attempts to split this snail number.
    /// </summary>
    /// <returns>A bool.</returns>
    public bool Split()
    {
      if (this.LeftNumber is not null && this.LeftNumber > 9)
      {
        this.LeftSnailNumber = SplitValue((int)this.LeftNumber);
        this.LeftSnailNumber.Parent = this;
        this.LeftNumber = null;
        return true;
      }

      if (this.LeftSnailNumber is not null)
      {
        if (this.LeftSnailNumber.Split())
        {
          return true;
        }
      }

      if (this.RightSnailNumber is not null)
      {
        if (this.RightSnailNumber.Split())
        {
          return true;
        }
      }

      if (this.RightNumber is not null && this.RightNumber > 9)
      {
        this.RightSnailNumber = SplitValue((int)this.RightNumber);
        this.RightSnailNumber.Parent = this;
        this.RightNumber = null;
        return true;
      }

      return false;
    }

    /// <summary>
    /// Splits the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A SnailNumber.</returns>
    private static SnailNumber SplitValue(int value)
    {
      int newLeft = (int)Math.Floor((float)value / 2);
      int newRight = (int)Math.Ceiling((float)value / 2);
      return new SnailNumber()
      {
        LeftNumber = newLeft,
        RightNumber = newRight
      };
    }

    /// <summary>
    /// Attempts to explode this snail number.
    /// </summary>
    /// <param name="depth">The depth.</param>
    /// <returns>A bool.</returns>
    private bool Explode(int depth)
    {
      if (this.LeftSnailNumber is not null)
      {
        if (this.LeftSnailNumber.Explode(depth + 1))
        {
          if (this.LeftSnailNumber.Exploded)
          {
            this.LeftSnailNumber = null;
            this.LeftNumber = 0;
          }

          return true;
        }
      }

      if (this.RightSnailNumber is not null)
      {
        if (this.RightSnailNumber.Explode(depth + 1))
        {
          if (this.RightSnailNumber.Exploded)
          {
            this.RightSnailNumber = null;
            this.RightNumber = 0;
          }

          return true;
        }
      }

      if (depth < 4)
      {
        return false;
      }

      var leftwardShuttle = new ValueShuttle((int)this.LeftNumber!, this, new LeftShuttleStrategy());
      var rightwardShuttle = new ValueShuttle((int)this.RightNumber!, this, new RightShuttleStrategy());
      leftwardShuttle.Launch();
      rightwardShuttle.Launch();
      this.Exploded = true;
      return true;
    }
  }
}