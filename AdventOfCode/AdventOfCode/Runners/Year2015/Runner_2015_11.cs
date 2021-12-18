namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_11.
  /// </summary>
  public class Runner_2015_11
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_11"/> class.
    /// </summary>
    public Runner_2015_11()
      : base(2015, 11)
    {
    }

    /// <summary>
    /// Contains the run.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>A bool.</returns>
    public static bool ContainsRun(string password)
    {
      for (int i = 2; i < password.Length; i++)
      {
        char c1 = password[i - 2];
        char c2 = password[i - 1];
        char c3 = password[i];

        if (c3 == c2 + 1 && c2 == c1 + 1)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Banneds the letters.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>A bool.</returns>
    public static bool BannedLetters(string password)
    {
      foreach (char c in password)
      {
        if (c == 'i' || c == 'l' || c == 'o')
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Doubles the double.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>A bool.</returns>
    public static bool DoubleDouble(string password)
    {
      char[] doubles = { '\0', '\0', '\0', '\0' };
      int pos = 0;
      foreach (char c in password)
      {
        if (doubles[pos] == c)
        {
          pos++;
          doubles[pos] = c;
          pos++;
        }
        else
        {
          doubles[pos] = c;
        }

        if (pos >= 4)
        {
          return true;
        }
      }

      return false;
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      string password = inputString;
      do
      {
        IncrementPassword(password);
      }
      while (!Validate(password));

      string part1 = new string(password.ToCharArray());

      do
      {
        IncrementPassword(password);
      }
      while (!Validate(password));

      string part2 = new string(password.ToCharArray());

      return (part1, part2);
    }

    /// <summary>
    /// Increments the password.
    /// </summary>
    /// <param name="password">The password.</param>
    private static unsafe void IncrementPassword(string password)
    {
      fixed (char* passwordPtr = password)
      {
        char* ptr = passwordPtr + password.Length - 1;

        bool incrementing = true;
        while (incrementing)
        {
          incrementing = false;
          *ptr = (char)((byte)*ptr + 1);
          if (*ptr > 'z')
          {
            *ptr = 'a';
            incrementing = true;
          }

          ptr--;
        }
      }
    }

    /// <summary>
    /// Validates the.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>A bool.</returns>
    private static bool Validate(string password)
    {
      return ContainsRun(password) && !BannedLetters(password) && DoubleDouble(password);
    }
  }
}