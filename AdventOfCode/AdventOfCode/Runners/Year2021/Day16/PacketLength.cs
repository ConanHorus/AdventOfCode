using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day16
{
  /// <summary>
  /// The packet length.
  /// </summary>
  public class PacketLength
  {
    /// <summary>
    /// None option.
    /// </summary>
    public static readonly PacketLength None = new PacketLength()
    {
      isBits = false,
      useStrategy = false,
      value = 0
    };

    /// <summary>
    /// Whether is bits strategy.
    /// </summary>
    private bool isBits;

    /// <summary>
    /// Whether to use strategy.
    /// </summary>
    private bool useStrategy;

    /// <summary>
    /// Value of strategy.
    /// </summary>
    private int value;

    /// <summary>
    /// News the bit count strategy.
    /// </summary>
    /// <param name="bitsHead">The bits head.</param>
    /// <param name="bits">The bits.</param>
    /// <returns>A PacketLength.</returns>
    public static PacketLength NewBitCountStrategy(int bitsHead, int bits)
    {
      return new PacketLength()
      {
        isBits = true,
        useStrategy = true,
        value = bitsHead + bits
      };
    }

    /// <summary>
    /// News the packet count strategy.
    /// </summary>
    /// <param name="packets">The packets.</param>
    /// <returns>A PacketLength.</returns>
    public static PacketLength NewPacketCountStrategy(int packets)
    {
      return new PacketLength()
      {
        isBits = false,
        useStrategy = true,
        value = packets
      };
    }

    /// <summary>
    /// Have the reached end.
    /// </summary>
    /// <param name="bitsHead">The bits head.</param>
    /// <param name="packets">The packets.</param>
    /// <returns>A bool.</returns>
    public bool HasReachedEnd(int bitsHead, int packets)
    {
      if (!this.useStrategy)
      {
        return true;
      }

      if (this.isBits)
      {
        return bitsHead >= this.value;
      }

      return packets >= this.value;
    }
  }
}