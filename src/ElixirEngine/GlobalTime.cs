using System.Diagnostics;

namespace ElixirEngine
{
    /// <summary>
    ///     Represents the global, real time.
    /// </summary>
    internal class GlobalTime : IGlobalTime
    {
        /// <summary>
        ///     The stopwatch.
        /// </summary>
        private readonly Stopwatch _stopwatch;

        /// <summary>
        ///     The ticks per second.
        /// </summary>
        private readonly long _ticksPerSecond;

        /// <summary>
        ///     The frames counter.
        /// </summary>
        private int _framesCounter;

        /// <summary>
        ///     The last frame ticks.
        /// </summary>
        private long _lastFrameTicks;

        /// <summary>
        ///     The current frame ticks.
        /// </summary>
        private long _currentFrameTicks;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalTime" /> class.
        /// </summary>
        public GlobalTime()
        {
            _stopwatch = Stopwatch.StartNew();
            _ticksPerSecond = Stopwatch.Frequency;
        }

        /// <inheritdoc />
        public int FramesPerSecond { get; private set; }

        /// <inheritdoc />
        public long Milliseconds { get; private set; }

        /// <summary>
        ///     Update the internal state and sets the timing for the current frame.
        /// </summary>
        public void Update()
        {
            _lastFrameTicks = _currentFrameTicks;
            _currentFrameTicks = _stopwatch.ElapsedTicks;
            Milliseconds = _currentFrameTicks * 1000 / _ticksPerSecond;
            UpdateFramesPerSecondEverySecond();
        }

        private void UpdateFramesPerSecondEverySecond()
        {
            _framesCounter++;

            if (_currentFrameTicks / _ticksPerSecond <= _lastFrameTicks / _ticksPerSecond)
            {
                return;
            }

            FramesPerSecond = _framesCounter;
            _framesCounter = 0;
        }
    }
}