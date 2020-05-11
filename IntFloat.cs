using System;

namespace IntFloatLib
{
    public readonly struct IntFloat : IEquatable<IntFloat>, IComparable<IntFloat>
    {
        public IntFloat(int value)
        {
            _rawValue = value;
        }
        
        #region Constants
        
        public const int Scale = 10000;
        public const float Epsilon = 1f / Scale;
        public const float MaxValue = int.MaxValue / (float) Scale;
        
        public static readonly IntFloat Zero = new IntFloat();
        public static readonly IntFloat One = new IntFloat(Scale);
        
        #endregion

        #region Fields and Properties

        private readonly int _rawValue;
        public int rawValue => _rawValue;
        public float toFloat => _rawValue / (float) Scale;
        
        #endregion

        #region Operator Overloads

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

        public static bool operator ==(IntFloat self, IntFloat other)
        {
            return self._rawValue == other._rawValue;
        }

        public static bool operator !=(IntFloat self, IntFloat other)
        {
            return !(self == other);
        }

        public static bool operator <(IntFloat self, IntFloat other)
        {
            return self._rawValue < other._rawValue;
        }

        public static bool operator >(IntFloat self, IntFloat other)
        {
            return self._rawValue > other._rawValue;
        }

        public static explicit operator float(IntFloat i)
        {
            return i.toFloat;
        }

        public static IntFloat operator *(IntFloat self, int i)
        {
            int tempRaw = checked(self._rawValue * i);
            return new IntFloat(tempRaw);
        }

        public static IntFloat operator *(int i, IntFloat self)
        {
            return self * i;
        }
        
        #endregion

        #region Helper Functions
        
        public static IntFloat FromInt(int i)
        {
            return new IntFloat(i * Scale);
        }
 
        public static IntFloat FromRaw(int raw)
        {
            return new IntFloat(raw);
        }
        
        #endregion
        
        #region Overrides

        public override string ToString()
        {
            return this.toFloat.ToString();
        }
        
        public bool Equals(IntFloat other)
        {
            return _rawValue == other._rawValue;
        }

        public override bool Equals(object obj)
        {
            return obj is IntFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _rawValue;
        }

        public int CompareTo(IntFloat other)
        {
            return _rawValue.CompareTo(other._rawValue);
        }

        #endregion
    }
}