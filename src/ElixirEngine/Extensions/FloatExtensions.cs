using System;

namespace ElixirEngine.Extensions
{
    /// <summary>
    ///     Provides helper and extension methods for working with a <see cref="float" /> structure.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="float" /> structure is within the range of the provided
        ///     <paramref name="difference" /> and this <see cref="float" /> structure.
        /// </summary>
        /// <param name="x">
        ///     The source <see cref="float" /> structure.
        /// </param>
        /// <param name="y">
        ///     The <see cref="float" /> structure to test.
        /// </param>
        /// <param name="difference">
        ///     The difference used to determine near equality.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the two <see cref="float" /> structures are nearly equal; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public static bool IsNearlyEqual(this float x, float y, float difference = float.Epsilon)
        {
            return MathF.Abs(x - y) < difference;
        }
    }
}