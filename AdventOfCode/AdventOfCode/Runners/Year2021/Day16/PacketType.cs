namespace AdventOfCode.Runners.Year2021.Day16
{
  /// <summary>
  /// The packet type.
  /// </summary>
  public enum PacketType
    : byte
  {
    Sum = 0,

    Product = 1,

    Minimum = 2,

    Maximum = 3,

    LiteralValue = 4,

    GreaterThan = 5,

    LessThan = 6,

    EqualTo = 7
  }
}