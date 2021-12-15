using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day12
{
  /// <summary>
  /// The adjacency matrix.
  /// </summary>
  public class AdjacencyMatrix
  {
    /// <summary>
    /// Matrix.
    /// </summary>
    private readonly List<bool[]> matrix = new List<bool[]>();

    /// <summary>
    /// Room names.
    /// </summary>
    private readonly List<string> roomNames = new List<string>();

    /// <summary>
    /// Room lookup.
    /// </summary>
    private readonly Dictionary<string, int> roomLookup = new Dictionary<string, int>();

    /// <summary>
    /// Initializes a new instance of the <see cref="AdjacencyMatrix"/> class.
    /// </summary>
    /// <param name="lines">The lines.</param>
    public AdjacencyMatrix(string[] lines)
    {
      this.SetRoomNames(lines);
      this.SetMatrix(lines);
    }

    /// <summary>
    /// Gets the connected rooms.
    /// </summary>
    /// <param name="roomName">The room name.</param>
    /// <returns>A list of string.</returns>
    public IEnumerable<string> GetConnectedRooms(string roomName)
    {
      var row = this.matrix[this.GetRoomNumber(roomName)];

      for (int i = 0; i < row.Length; i++)
      {
        if (row[i])
        {
          yield return this.GetsRoomName(i);
        }
      }
    }

    /// <summary>
    /// Sets the matrix.
    /// </summary>
    /// <param name="lines">The lines.</param>
    private void SetMatrix(string[] lines)
    {
      int size = this.roomNames.Count;
      for (int i = 0; i < size; i++)
      {
        this.matrix.Add(new bool[size]);
      }

      foreach (string line in lines)
      {
        string[] names = line.Split('-');
        var row = this.matrix[this.GetRoomNumber(names[0])];
        row[this.GetRoomNumber(names[1])] = true;
        row = this.matrix[this.GetRoomNumber(names[1])];
        row[this.GetRoomNumber(names[0])] = true;
      }
    }

    /// <summary>
    /// Sets the room names.
    /// </summary>
    /// <param name="lines">The lines.</param>
    private void SetRoomNames(string[] lines)
    {
      foreach (string line in lines)
      {
        string[] names = line.Split('-');
        foreach (string name in names)
        {
          this.AddRoom(name);
        }
      }
    }

    /// <summary>
    /// Gets the room number.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>An int.</returns>
    private int GetRoomNumber(string name)
    {
      return this.roomLookup[name];
    }

    /// <summary>
    /// Adds the room.
    /// </summary>
    /// <param name="roomName">The room name.</param>
    private void AddRoom(string roomName)
    {
      if (this.roomLookup.ContainsKey(roomName))
      {
        return;
      }

      this.roomNames.Add(roomName);
      this.roomLookup.Add(roomName, this.roomNames.Count - 1);
    }

    /// <summary>
    /// Gets the room name.
    /// </summary>
    /// <param name="roomNumber">The room number.</param>
    /// <returns>A string.</returns>
    private string GetsRoomName(int roomNumber)
    {
      return this.roomNames[roomNumber];
    }
  }
}