namespace ServicesLibrary
{
    /// <summary>
    /// Limits value to a set range.
    /// Requires a minimum, maximum and a value to limit.
    /// Shrinks number to range upon initiation.
    /// Shrinks number to range when using mathematical operators: +, -.
    /// </summary>
    public class IntLimited
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private static int MinValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static int MaxValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public IntLimited(int value, int min, int max)
        {
            // Setting private property values
            MinValue = min;
            MaxValue = max;

            // Setting private property based on input value
            Value = value > max ? (value - max) :       // IF value is greater than max,
                    value < min ? (value + max) :       // ELSE IF value is less than max,
                    value == max? 0 : value;            // ELSE
        }

        #region Operator overrides
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int operator +(IntLimited a, int b)
        {
            return (b + a.Value) % MaxValue - MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int operator -(IntLimited a, int b)
        {
            return (b - a.Value) % MaxValue - MinValue;
        }
        #endregion 
    }
}