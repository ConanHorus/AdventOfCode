using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2021.Day08
{
  /// <summary>
  /// The line.
  /// </summary>
  public class Line
  {
    /// <summary>
    /// Top.
    /// </summary>
    private const int TOP = 0;

    /// <summary>
    /// Top left.
    /// </summary>
    private const int TOP_LEFT = 1;

    /// <summary>
    /// Top right.
    /// </summary>
    private const int TOP_RIGHT = 2;

    /// <summary>
    /// Middle.
    /// </summary>
    private const int MIDDLE = 3;

    /// <summary>
    /// Bottom left.
    /// </summary>
    private const int BOTTOM_LEFT = 4;

    /// <summary>
    /// Bottom right.
    /// </summary>
    private const int BOTTOM_RIGHT = 5;

    /// <summary>
    /// Bottom.
    /// </summary>
    private const int BOTTOM = 6;

    /// <summary>
    /// Number definitions.
    /// </summary>
    private static readonly int[][] NUMBER_DEFS = {
      new int[] { TOP, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, BOTTOM }, // 0
      new int[] { TOP_RIGHT, BOTTOM_RIGHT }, // 1
      new int[] { TOP, TOP_RIGHT, MIDDLE, BOTTOM_LEFT, BOTTOM }, // 2
      new int[] { TOP, TOP_RIGHT, MIDDLE, BOTTOM_RIGHT, BOTTOM }, // 3
      new int[] { TOP_LEFT, TOP_RIGHT, MIDDLE, BOTTOM_RIGHT }, // 4
      new int[] { TOP, TOP_LEFT, MIDDLE, BOTTOM_RIGHT, BOTTOM }, // 5
      new int[] { TOP, TOP_LEFT, MIDDLE, BOTTOM_LEFT, BOTTOM_RIGHT, BOTTOM }, // 6
      new int[] { TOP, TOP_RIGHT, BOTTOM_RIGHT }, // 7
      new int[] { TOP, TOP_LEFT, TOP_RIGHT, MIDDLE, BOTTOM_LEFT, BOTTOM_RIGHT, BOTTOM }, // 8
      new int[] { TOP, TOP_LEFT, TOP_RIGHT, MIDDLE, BOTTOM_RIGHT, BOTTOM } // 9
    };

    /// <summary>
    /// Input raw.
    /// </summary>
    private readonly string[] inputRaw;

    /// <summary>
    /// Output raw.
    /// </summary>
    private readonly string[] outputRaw;

    /// <summary>
    /// Raw data.
    /// </summary>
    private readonly string[] rawData;

    /// <summary>
    /// Real output numbers.
    /// </summary>
    private readonly List<int> outputNumbers = new List<int>();

    /// <summary>
    /// Signal code.
    /// </summary>
    private readonly HashSet<char>[] signalCode = new HashSet<char>[7];

    /// <summary>
    /// Initializes a new instance of the <see cref="Line"/> class.
    /// </summary>
    /// <param name="lineData">The line data.</param>
    public Line(string lineData)
    {
      string[] parts = lineData.Split('|');
      this.inputRaw = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
      this.outputRaw = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
      this.rawData = new string[this.inputRaw.Length + this.outputRaw.Length];
      Array.Copy(this.inputRaw, 0, this.rawData, 0, this.inputRaw.Length);
      Array.Copy(this.outputRaw, 0, this.rawData, this.inputRaw.Length, this.outputRaw.Length);

      for (int i = 0; i < signalCode.Length; i++)
      {
        signalCode[i] = new HashSet<char>();
      }

      this.DiscoverSignalInfo();
    }

    /// <summary>
    /// Gets the output value.
    /// </summary>
    /// <returns>An int.</returns>
    public int GetOutputValue()
    {
      int value = 0;
      foreach (int digit in this.outputNumbers)
      {
        value *= 10;
        value += digit;
      }

      return value;
    }

    /// <summary>
    /// Counts the occurances in output.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>An int.</returns>
    public int CountOccurancesInOutput(int number)
    {
      return this.outputNumbers.Where(x => x == number).Count();
    }

    /// <summary>
    /// Discovers the signal info.
    /// </summary>
    private void DiscoverSignalInfo()
    {
      string one = this.rawData.Where(x => x.Length == 2).First();
      string four = this.rawData.Where(x => x.Length == 4).First();
      string seven = this.rawData.Where(x => x.Length == 3).First();
      string eight = this.rawData.Where(x => x.Length == 7).First();

      char[] correctedSignals = new char[7];
      correctedSignals[TOP] = seven.Where(x => !one.Contains(x)).First();
      correctedSignals[BOTTOM_LEFT] = this.rawData
        .Where(x => x.Length == 6)
        .Select(x => eight.Where(y => !x.Contains(y)).First())
        .Where(x => !four.Contains(x))
        .First();
      correctedSignals[TOP_RIGHT] = this.rawData
        .Where(x => x.Length == 6)
        .Select(x => eight.Where(y => !x.Contains(y)).First())
        .Where(x => one.Contains(x))
        .First();
      correctedSignals[BOTTOM_RIGHT] = one.Where(x => x != correctedSignals[TOP_RIGHT]).First();

      string nine = this.rawData.Where(x => x.Length == 6 && !x.Contains(correctedSignals[BOTTOM_LEFT])).First();
      string six = this.rawData.Where(x => x.Length == 6 && !x.Contains(correctedSignals[TOP_RIGHT])).First();

      correctedSignals[MIDDLE] = this.rawData
        .Where(x => x.Length == 6)
        .Select(x => eight.Where(y => !x.Contains(y)).First())
        .Where(x => six.Contains(x) && nine.Contains(x))
        .First();

      string zero = this.rawData.Where(x => x.Length == 6 && !x.Contains(correctedSignals[MIDDLE])).First();

      correctedSignals[BOTTOM] = nine
        .Where(x => x != correctedSignals[TOP] && !four.Contains(x))
        .First();
      correctedSignals[TOP_LEFT] = nine
        .Where(x => x != correctedSignals[MIDDLE] && x != correctedSignals[BOTTOM] && !seven.Contains(x))
        .First();

      string two = new string(new char[] {
        correctedSignals[TOP],
        correctedSignals[TOP_RIGHT],
        correctedSignals[MIDDLE],
        correctedSignals[BOTTOM_LEFT],
        correctedSignals[BOTTOM]
      });
      string three = new string(new char[] {
        correctedSignals[TOP],
        correctedSignals[TOP_RIGHT],
        correctedSignals[MIDDLE],
        correctedSignals[BOTTOM_RIGHT],
        correctedSignals[BOTTOM]
      });
      string five = new string(new char[] {
        correctedSignals[TOP],
        correctedSignals[TOP_LEFT],
        correctedSignals[MIDDLE],
        correctedSignals[BOTTOM_RIGHT],
        correctedSignals[BOTTOM]
      });

      foreach (string signal in outputRaw)
      {
        this.RecordNumber(signal, zero, one, two, three, four, five, six, seven, eight, nine);
      }
    }

    /// <summary>
    /// Records the number.
    /// </summary>
    /// <param name="signal">The signal.</param>
    /// <param name="zero">The zero.</param>
    /// <param name="one">The one.</param>
    /// <param name="two">The two.</param>
    /// <param name="three">The three.</param>
    /// <param name="four">The four.</param>
    /// <param name="five">The five.</param>
    /// <param name="six">The six.</param>
    /// <param name="seven">The seven.</param>
    /// <param name="eight">The eight.</param>
    /// <param name="nine">The nine.</param>
    private void RecordNumber(
      string signal,
      string zero,
      string one,
      string two,
      string three,
      string four,
      string five,
      string six,
      string seven,
      string eight,
      string nine)
    {
      this.outputNumbers.Add(signal.Length switch
      {
        2 => 1,
        3 => 7,
        4 => 4,
        5 => two.Select(x => signal.Contains(x)).All(x => x) ? 2 : (three.Select(x => signal.Contains(x)).All(x => x) ? 3 : 5),
        6 => zero.Select(x => signal.Contains(x)).All(x => x) ? 0 : (six.Select(x => signal.Contains(x)).All(x => x) ? 6 : 9),
        7 => 8,
        _ => throw new NotImplementedException()
      });
    }
  }
}