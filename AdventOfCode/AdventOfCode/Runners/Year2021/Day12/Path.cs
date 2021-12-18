using System.Collections.Generic;

namespace AdventOfCode.Runners.Year2021.Day12
{
  /// <summary>
  /// The path.
  /// </summary>
  public class Path
  {
    /// <summary>
    /// Whether a small room was visited twice.
    /// </summary>
    private bool visitedTwice;

    /// <summary>
    /// Gets the rooms.
    /// </summary>
    public List<string> Rooms { get; private set; } = new List<string>();

    /// <summary>
    /// Gets the state.
    /// </summary>
    public PathState State { get; private set; } = PathState.KeepGoing;

    /// <summary>
    /// Gets the last room.
    /// </summary>
    private string LastRoom => this.Rooms[^1];

    /// <summary>
    /// Initializes a new instance of the <see cref="Path"/> class.
    /// </summary>
    public Path()
    {
      Rooms.Add("start");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Path"/> class.
    /// </summary>
    /// <param name="previous">The previous.</param>
    /// <param name="next">The next.</param>
    /// <param name="allowSingleSmallReturn">Whether to allow a single return to a small room.</param>
    public Path(Path previous, string next, bool allowSingleSmallReturn)
    {
      this.Rooms.AddRange(previous.Rooms);
      this.Rooms.Add(next);
      this.visitedTwice = previous.visitedTwice;

      if (next == "start")
      {
        this.State = PathState.Bad;
        return;
      }

      if (next == "end")
      {
        this.State = PathState.Good;
        return;
      }

      if (next[0] >= 'a' && next[0] <= 'z')
      {
        if (previous.Rooms.Contains(next))
        {
          this.HandleSmallRoomReturn(next, allowSingleSmallReturn);
        }
      }
    }

    /// <summary>
    /// Generates the child paths.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="allowSingleSmallReturn">Whether to allow a single return to a small room.</param>
    /// <returns>A list of Paths.</returns>
    public IEnumerable<Path> GenerateChildPaths(AdjacencyMatrix matrix, bool allowSingleSmallReturn)
    {
      foreach (string nextRoom in matrix.GetConnectedRooms(this.LastRoom))
      {
        yield return new Path(this, nextRoom, allowSingleSmallReturn);
      }
    }

    /// <summary>
    /// Handles the small room return.
    /// </summary>
    /// <param name="roomName">The room name.</param>
    /// <param name="allowSingleSmallReturn">If true, allow single small return.</param>
    private void HandleSmallRoomReturn(string roomName, bool allowSingleSmallReturn)
    {
      if (!allowSingleSmallReturn)
      {
        this.State = PathState.Bad;
        return;
      }

      if (this.visitedTwice)
      {
        this.State = PathState.Bad;
        return;
      }

      this.visitedTwice = true;
    }
  }
}