using System;

namespace IntFloat
{
    public struct IntFloat
    {
        public const int Scale = 10000;
        public const float Epsilon = 1f / Scale;
        public const float MaxValue = int.MaxValue / (float) Scale;

        private int _rawValue;
        public int rawValue => _rawValue;
        public float toFloat => _rawValue / (float) Scale;

        public IntFloat(int value)
        {
            _rawValue = value;
        }

        public static IntFloat operator +(IntFloat self, IntFloat other)
        {
            return new IntFloat(self._rawValue + other._rawValue);
        }
        
        public static IntFloat operator -(IntFloat self, IntFloat other)
        {
            return new IntFloat(self._rawValue - other._rawValue);
        }

        public static IntFloat operator *(IntFloat self, IntFloat other)
        {
            long tempRaw = checked(self._rawValue * (long) other._rawValue);
            tempRaw /= Scale;
            if (tempRaw > int.MaxValue) throw new OverflowException("Operation result out of representable range!");
            return new IntFloat((int) tempRaw);
        }

        public static IntFloat operator /(IntFloat self, IntFloat other)
        {
            long tempRaw = checked(self._rawValue * (long) Scale);
            tempRaw /= other._rawValue;
            if (tempRaw > int.MaxValue) throw new OverflowException("Operation result out of representable range!");
            return new IntFloat((int) tempRaw);
        }

        public static explicit operator float(IntFloat i)
        {
            return i.toFloat;
        }

        public override string ToString()
        {
            return this.toFloat.ToString();
        }
    }
}