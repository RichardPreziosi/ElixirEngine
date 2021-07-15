using System;

namespace ElixirEngine
{
    /// <inheritdoc />
    internal class Time : ITime
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Time" /> class.
        /// </summary>
        public Time()
        {
            SpeedFactor = 1.0f;
        }

        /// <inheritdoc />
        public float Delta { get; internal set; }

        /// <inheritdoc />
        public bool IsPaused { get; set; }

        /// <inheritdoc />
        public float RapidUpdateDelta { get; internal set; }

        /// <inheritdoc />
        public float SpeedFactor { get; set; }

        /// <inheritdoc />
        public float Total { get; internal set; }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///     Thrown if <paramref name="interval" /> is less than or equal to <see cref="Delta" />.
        /// </exception>
        public bool CheckEvery(float interval)
        {
            if (interval <= Delta)
            {
                throw new ArgumentException(
                    $"Interval of `{interval}` is too small, it must be larger than the current delta of `{Delta}`.",
                    nameof(interval));
            }

            return (int) (Total / interval) > (int) ((Total - Delta) / interval);
        }
    }
}