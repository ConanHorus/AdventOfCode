using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day16
{
  /// <summary>
  /// The BITS
  /// </summary>
  public class BITS
  {
    /// <summary>
    /// Data.
    /// </summary>
    private readonly bool[] data;

    /// <summary>
    /// Initializes a new instance of the <see cref="BITS"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public BITS(bool[] data) => this.data = data;

    /// <summary>
    /// Gets the head.
    /// </summary>
    public int Head { get; private set; }

    /// <summary>
    /// Gets the header number.
    /// </summary>
    /// <returns>A byte.</returns>
    public byte GetHeaderNumber()
    {
      return (byte)this.GetUninterruptedNumber(3);
    }

    /// <summary>
    /// Gets the packet literal value.
    /// </summary>
    /// <returns>A long.</returns>
    public long GetPacketLiteralValue()
    {
      long value = 0;
      bool indicator;
      do
      {
        indicator = this.data[this.Head++];
        value <<= 4;
        value += GetUninterruptedNumber(4);
      }
      while (indicator);

      return value;
    }

    /// <summary>
    /// Gets the single bit.
    /// </summary>
    /// <returns>A bool.</returns>
    public bool GetSingleBit()
    {
      return this.data[this.Head++];
    }

    /// <summary>
    /// Gets the length.
    /// </summary>
    /// <param name="bits">The bits.</param>
    /// <returns>An int.</returns>
    public int GetLength(int bits)
    {
      return this.GetUninterruptedNumber(bits);
    }

    /// <summary>
    /// Gets the uninterrupted number.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <returns>An int.</returns>
    private int GetUninterruptedNumber(int length)
    {
      int value = 0;
      for (int i = 0; i < length; i++)
      {
        value <<= 1;
        value += (byte)(this.data[this.Head] ? 1 : 0);
        this.Head++;
      }

      return value;
    }
  }
}