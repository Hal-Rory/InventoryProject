using System;
using UnityEngine;

namespace Common.Utility
{
    public static class IntExtensions
    {
        /// <summary>
        /// Loops the value t, so that it is never larger than length and never smaller than
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int Repeat(this int i, int length)
        {
            return Mathf.FloorToInt(Mathf.Repeat(i, length));
        }

        /// <summary>
        /// Checks if between two values(inclusive)
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool IsBetweenRange(this int thisValue, int value1, int value2)
        {
            return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
        }
    }


    /// <summary>
    ///   <para>Describes an integer range.</para>
    /// </summary>
    /// <remarks>
    /// Why isn't this serializable, Unity?
    /// </remarks>
    [Serializable]
    public struct RangeInt
    {
        /// <summary>
        ///   <para>The starting index of the range, where 0 is the first position, 1 is the second, 2 is the third, and so on.</para>
        /// </summary>
        [SerializeField] public int Start;

        /// <summary>
        ///   <para>The length of the range.</para>
        /// </summary>
        public int Length => Math.Abs(End - Start);

        /// <summary>
        ///   <para>The end index of the range (not inclusive).</para>
        /// </summary>
        [Tooltip("Exclusive")]
        [SerializeField] public int End;

        /// <summary>
        ///   <para>Constructs a new RangeInt with given start, length values.</para>
        /// </summary>
        /// <param name="start">The starting index of the range.</param>
        /// <param name="end">The length of the range.</param>
        public RangeInt(int start, int end)
        {
            Start = start;
            End = end;
        }

    }
}