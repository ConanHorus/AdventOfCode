using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
  /// <summary>
  /// The data.
  /// </summary>
  public static class Data
  {
    /// <summary>
    /// The data location.
    /// </summary>
    private const string DATA_LOCATION = @"C:\Users\timot\Desktop\Advent of Code\Advent of Code\Data\";

    /// <summary>
    /// Reads the all text.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="day">The day.</param>
    /// <returns>A string.</returns>
    public static string ReadAllText(int year, int day)
    {
      return File.ReadAllText(GenerateDataFileName(year, day));
    }

    /// <summary>
    /// Reads the all lines.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="day">The day.</param>
    /// <returns>An array of string.</returns>
    public static string[] ReadAllLines(int year, int day)
    {
      return File.ReadAllLines(GenerateDataFileName(year, day));
    }

    /// <summary>
    /// Generates the data file name.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="day">The day.</param>
    /// <returns>A string.</returns>
    private static string GenerateDataFileName(int year, int day)
    {
      return DATA_LOCATION + $"Data {year}.{day:00}.txt";
    }
  }
}