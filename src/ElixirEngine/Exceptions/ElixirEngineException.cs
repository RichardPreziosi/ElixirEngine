using System;

namespace ElixirEngine.Exceptions
{
    /// <summary>
    ///     Represents a generic error that occurs during application execution.
    /// </summary>
    public class ElixirEngineException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ElixirEngineException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ElixirEngineException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ElixirEngineException" /> class with a specified error message and a
        ///     reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference if no inner
        ///     exception is specified.
        /// </param>
        public ElixirEngineException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}