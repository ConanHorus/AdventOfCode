﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_4.
  /// </summary>
  public class Runner_2015_04
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_04"/> class.
    /// </summary>
    public Runner_2015_04()
      : base(2015, 4)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      using var hasher = MD5.Create();

      long i = 0;
      string hash;
      byte[] hashedArray;
      long number_5 = 0;
      long number_6 = 0;
      do
      {
        i++;
        byte[] byteArray = (inputString + i.ToString()).Select(x => (byte)x).ToArray();
        hashedArray = hasher.ComputeHash(byteArray);
        hash = BitConverter.ToString(hashedArray).Replace("-", string.Empty);

        if (i % 1000 == 0)
        {
          Console.WriteLine(hash);
        }

        if (hash.StartsWith("00000") && number_5 == 0)
        {
          number_5 = i;
        }

        if (hash.StartsWith("000000"))
        {
          number_6 = i;
        }
      }
      while (number_6 == 0);

      return (number_5.ToString(), number_6.ToString());
    }
  }
}