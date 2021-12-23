using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day16
{
  /// <summary>
  /// The packet.
  /// </summary>
  public class Packet
  {
    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    public byte Version { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public PacketType Type { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public long Value { get; set; }

    /// <summary>
    /// Gets or sets the length strategy.
    /// </summary>
    public PacketLength LengthStrategy { get; set; } = PacketLength.None;

    /// <summary>
    /// Gets the children.
    /// </summary>
    public List<Packet> Children { get; } = new List<Packet>();

    /// <summary>
    /// Enumerates the children.
    /// </summary>
    /// <returns>A list of Packets.</returns>
    public IEnumerable<Packet> EnumerateChildren()
    {
      yield return this;
      foreach (var child in this.Children.SelectMany(x => x.EnumerateChildren()))
      {
        yield return child;
      }
    }

    /// <summary>
    /// Reduces this packet.
    /// </summary>
    public void Reduce()
    {
      foreach (var child in this.Children)
      {
        child.Reduce();
      }

      switch (this.Type)
      {
        case PacketType.EqualTo:
          this.Value = this.Children[0].Value == this.Children[1].Value ? 1 : 0;
          break;

        case PacketType.GreaterThan:
          this.Value = this.Children[0].Value > this.Children[1].Value ? 1 : 0;
          break;

        case PacketType.LessThan:
          this.Value = this.Children[0].Value < this.Children[1].Value ? 1 : 0;
          break;

        case PacketType.Maximum:
          this.Value = this.Children.Select(x => x.Value).Max();
          break;

        case PacketType.Minimum:
          this.Value = this.Children.Select(x => x.Value).Min();
          break;

        case PacketType.Product:
          this.Value = this.Children.Select(x => x.Value).Aggregate((x, y) => x * y);
          break;

        case PacketType.Sum:
          this.Value = this.Children.Select(x => x.Value).Sum();
          break;
      }
    }
  }
}