using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day16
{
  /// <summary>
  /// The message class.
  /// </summary>
  public static class Message
  {
    /// <summary>
    /// Converts the hexidecimal to expanded bits.
    /// </summary>
    /// <param name="hexMessage">The hex message.</param>
    /// <returns>An array of bool.</returns>
    public static BITS ConvertHexidecimalToExpandedBits(string hexMessage)
    {
      bool[] bits = new bool[hexMessage.Length * 4];
      int ptr = 0;
      bool[] workBuffer = new bool[4];
      foreach (char c in hexMessage)
      {
        ConvertCharacterToExpandedBits(c, workBuffer);
        Array.Copy(workBuffer, 0, bits, ptr, 4);
        ptr += 4;
      }

      return new BITS(bits);
    }

    /// <summary>
    /// Converts the bits to packet tree.
    /// </summary>
    /// <param name="bits">The bits.</param>
    /// <returns>A Packet.</returns>
    public static Packet ConvertBitsToPacketTree(BITS bits)
    {
      var packet = new Packet
      {
        Version = bits.GetHeaderNumber(),
        Type = (PacketType)bits.GetHeaderNumber()
      };

      return ChooseDeserializationMethod(packet, bits);
    }

    /// <summary>
    /// Converts the character to expanded bits.
    /// </summary>
    /// <param name="c">The c.</param>
    /// <param name="workBuffer">The work buffer.</param>
    private static void ConvertCharacterToExpandedBits(char c, bool[] workBuffer)
    {
      workBuffer[0] = c == '8' || c == '9' || (c >= 'A' && c <= 'F');
      workBuffer[1] = (c >= '4' && c <= '7') || (c >= 'C' && c <= 'F');
      workBuffer[2] = c switch
      {
        '2' or '3' or '6' or '7' or 'A' or 'B' or 'E' or 'F' => true,
        _ => false,
      };

      workBuffer[3] = c switch
      {
        '1' or '3' or '5' or '7' or '9' or 'B' or 'D' or 'F' => true,
        _ => false,
      };
    }

    /// <summary>
    /// Chooses the deserialization method.
    /// </summary>
    /// <param name="packet">The packet.</param>
    /// <param name="bits">The bits.</param>
    /// <returns>A Packet.</returns>
    private static Packet ChooseDeserializationMethod(Packet packet, BITS bits)
    {
      if (packet.Type == PacketType.LiteralValue)
      {
        return DeserializeLiteralValuePacket(packet, bits);
      }

      return DeserializeOperatorPacket(packet, bits);
    }

    /// <summary>
    /// Deserializes the operator packet.
    /// </summary>
    /// <param name="packet">The packet.</param>
    /// <param name="bits">The bits.</param>
    /// <returns>A Packet.</returns>
    private static Packet DeserializeOperatorPacket(Packet packet, BITS bits)
    {
      PacketLength lengthStrategy;
      int lengthValue;
      if (bits.GetSingleBit())
      {
        lengthValue = bits.GetLength(11);
        lengthStrategy = PacketLength.NewPacketCountStrategy(lengthValue);
      }
      else
      {
        lengthValue = bits.GetLength(15);
        lengthStrategy = PacketLength.NewBitCountStrategy(bits.Head, lengthValue);
      }

      packet.LengthStrategy = lengthStrategy;
      while (!packet.LengthStrategy.HasReachedEnd(bits.Head, packet.Children.Count))
      {
        packet.Children.Add(ConvertBitsToPacketTree(bits));
      }

      return packet;
    }

    /// <summary>
    /// Deserializes the literal value packet.
    /// </summary>
    /// <param name="packet">The packet.</param>
    /// <param name="bits">The bits.</param>
    /// <returns>A Packet.</returns>
    private static Packet DeserializeLiteralValuePacket(Packet packet, BITS bits)
    {
      packet.Value = bits.GetPacketLiteralValue();
      return packet;
    }
  }
}