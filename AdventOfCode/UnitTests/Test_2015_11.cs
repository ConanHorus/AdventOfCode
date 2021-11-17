using AdventOfCode.Runners.Year2015;
using NUnit.Framework;

namespace UnitTests
{
  /// <summary>
  /// The test_15_11.
  /// </summary>
  public class Test_2015_11
  {
    [Test]
    public void DoubleDouble_HasDoubles()
    {
      Assert.IsTrue(Runner_2015_11.DoubleDouble("abcdffaa"));
    }

    [Test]
    public void DoubleDouble_NoDoubles()
    {
      Assert.IsFalse(Runner_2015_11.DoubleDouble("abcdffab"));
    }

    [Test]
    public void Run_HasRun()
    {
      Assert.IsTrue(Runner_2015_11.ContainsRun("abcdffaa"));
    }

    [Test]
    public void Run_NoRun()
    {
      Assert.IsFalse(Runner_2015_11.ContainsRun("azddffaa"));
    }
  }
}