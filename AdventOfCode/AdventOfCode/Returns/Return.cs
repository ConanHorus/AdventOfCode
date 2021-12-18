using System;

namespace AdventOfCode.Returns
{
  /// <summary>
  /// Return instance.
  /// </summary>
  public struct Return
  {
    /// <summary>
    /// Gets a value indicating whether successful.
    /// </summary>
    public bool Successful => this.Exception is null;

    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    public Exception? Exception { get; set; }

    public static implicit operator bool(Return obj)
    {
      return obj.Successful;
    }

    public static implicit operator Return(Exception exception)
    {
      return new Return { Exception = exception };
    }

    /// <summary>
    /// Successes.
    /// </summary>
    /// <returns>A Return.</returns>
    public static Return Success()
    {
      return new Return();
    }

    /// <summary>
    /// Failure.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>A Return.</returns>
    public static Return Failure(Exception exception)
    {
      return exception;
    }

    /// <summary>
    /// Successes.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>A Return.</returns>
    public static Return<T> Success<T>(T? value)
    {
      return new Return<T> { Value = value };
    }

    /// <summary>
    /// Failure.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>A Return.</returns>
    public static Return<T> Failure<T>(Exception exception)
    {
      return exception;
    }
  }
}