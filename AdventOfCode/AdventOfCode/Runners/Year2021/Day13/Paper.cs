using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2021.Day13
{
  /// <summary>
  /// The paper.
  /// </summary>
  public class Paper
  {
    /// <summary>
    /// Dots.
    /// </summary>
    private HashSet<Dot> dots = new HashSet<Dot>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Paper"/> class.
    /// </summary>
    /// <param name="lines">The lines.</param>
    public Paper(string[] lines)
    {
      foreach (string line in lines)
      {
        string[] parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
          return;
        }

        this.dots.Add(new Dot(int.Parse(parts[0]), int.Parse(parts[1])));
      }
    }

    /// <summary>
    /// Gets the visible dots.
    /// </summary>
    public int VisibleDots => this.dots.Count;

    /// <summary>
    /// Folds the up horizontally.
    /// </summary>
    /// <param name="y">The y.</param>
    public void FoldUpHorizontally(int y)
    {
      var newDots = new HashSet<Dot>();
      foreach (var dot in this.dots)
      {
        int newY = dot.Y;

        if (newY >= y)
        {
          newY -= y;
          newY *= -1;
          newY += y;
        }

        newDots.Add(new Dot(dot.X, newY));
      }

      this.dots = newDots;
    }

    /// <summary>
    /// Prints the paper.
    /// </summary>
    public void Print()
    {
      int totalLines = this.dots.Select(x => x.Y).Max() + 1;
      for (int i = 0; i < totalLines; i++)
      {
        var thisLine = this.dots.Where(x => x.Y == i);
        char[] stringBuffer;
        if (thisLine.Count() > 0)
        {
          int lineLength = thisLine.Select(x => x.X).Max() + 2;
          stringBuffer = Enumerable.Repeat(' ', lineLength).ToArray();
          foreach (int x in thisLine.Select(x => x.X))
          {
            stringBuffer[x + 1] = '#';
          }
        }
        else
        {
          stringBuffer = new char[1];
        }

        stringBuffer[0] = '>';
        Console.WriteLine(new string(stringBuffer));
      }

      Console.WriteLine();
    }

    /// <summary>
    /// Folds the left vertically.
    /// </summary>
    /// <param name="x">The x.</param>
    public void FoldLeftVertically(int x)
    {
      var newDots = new HashSet<Dot>();
      foreach (var dot in this.dots)
      {
        int newX = dot.X;

        if (newX >= x)
        {
          newX -= x;
          newX *= -1;
          newX += x;
        }

        newDots.Add(new Dot(newX, dot.Y));
      }

      this.dots = newDots;
    }
  }
}