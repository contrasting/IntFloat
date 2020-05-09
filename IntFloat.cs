namespace IntFloat
{
    public struct IntFloat
    {
        public const int Scale = 10000;
        public const float Epsilon = 1f / Scale;
        public const float MaxValue = int.MaxValue / (float) Scale;

        private int _rawValue;

        public float toFloat => _rawValue / (float) Scale;

        public IntFloat(int value)
        {
            _rawValue = value;
        }

        public static IntFloat operator +(IntFloat self, IntFloat other)
        {
            self._rawValue += other._rawValue;
            return self;
        }
        
        public static IntFloat operator -(IntFloat self, IntFloat other)
        {
            self._rawValue += other._rawValue;
            return self;
        }

        public static IntFloat operator *(IntFloat self, IntFloat other)
        {
            self._rawValue *= other._rawValue;
            self._rawValue /= Scale;
            return self;
        }

        public static IntFloat operator /(IntFloat self, IntFloat other)
        {
            self._rawValue *= Scale;
            self._rawValue /= other._rawValue;
            return self;
        }

        public static explicit operator float(IntFloat i)
        {
            return i.toFloat;
        }
    }
}