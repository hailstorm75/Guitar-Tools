namespace guitarTools
{
    // TODO Write documentation for IntLimited class

    class IntLimited
    {
        private static int MinValue { get; set; }
        private static int MaxValue { get; set; }
        public static int Value { get; set; }

        // TODO Remove unnesessary code
        public IntLimited(int value, int min, int max)
        {
            MinValue = min;
            MaxValue = max;
            Value = value > max ? (value - max) :
                     value < min ? (value + max + 1) :
                     value;
        }

        public static int operator +(IntLimited a, int b)
        {
            return (b + a.GetValue) % MaxValue - MinValue;
        }

       
        public int GetValue
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }
    }
}
