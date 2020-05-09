namespace IntFloat
{
    public struct IntFloat
    {
        public const int Scale = 1000;
        public const float Epsilon = 1f / Scale;

        private int _rawValue;
        private int _scale;

        public int toFlooredInt => _rawValue / _scale;
        public float toFloat => _rawValue / (float) _scale;

        public IntFloat(int value)
        {
            _rawValue = value;
            _scale = Scale;
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
            self._scale *= other._scale;
            return self;
        }

        public static IntFloat operator /(IntFloat self, IntFloat other)
        {
            self._rawValue /= other.toFlooredInt;
            return self;
        }
    }
}