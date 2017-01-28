namespace ServicesLibrary
{
    /// <summary>
    /// Limits value to a set range.
    /// Requires a minimum, maximum and a value to limit.
    /// Shrinks number to range upon initiation.
    /// Shrinks number to range when using mathematical operators: +, -
    /// </summary>

    public class IntLimited
    {
        #region Properties
        private static int MinValue { get; set; }
        private static int MaxValue { get; set; }
        public int Value { get; set; }
        #endregion

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
        public static int operator +(IntLimited a, int b)
        {
            return (b + a.Value) % MaxValue - MinValue;
        }
        public static int operator -(IntLimited a, int b)
        {
            return (b - a.Value) % MaxValue - MinValue;
        }
        #endregion 
    }
}