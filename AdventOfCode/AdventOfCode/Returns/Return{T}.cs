using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Returns
{
  /// <summary>
  /// Return of type T.
  /// </summary>
  /// <typeparam name="T">Type.</typeparam>
  public struct Return<T>
  {
    /// <summary>
    /// Gets a value indicating whether successful.
    /// </summary>
    public bool Successful => this.Exception is null;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    public Exception? Exception { get; set; }

    public static implicit operator bool(Return<T> obj)
    {
      return obj.Successful;
    }

    public static implicit operator Return<T>(Exception exception)
    {
      return new Return<T> { Exception = exception };
    }
  }
}