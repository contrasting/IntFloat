﻿using System;

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
        public double toDouble => _rawValue / (double) Scale;
        
        #endregion

        #region Operator Overloads

        public static IntFloat operator +(IntFloat self, IntFloat other)
        {
            return new IntFloat(checked(self._rawValue + other._rawValue));
        }
        
        public static IntFloat operator -(IntFloat self, IntFloat other)
        {
            return new IntFloat(checked(self._rawValue - other._rawValue));
        }

        public static IntFloat operator *(IntFloat self, IntFloat other)
        {
            long tempRaw = checked(self._rawValue * (long) other._rawValue);
            tempRaw /= Scale;
            if (tempRaw > int.MaxValue || tempRaw < int.MinValue)
                throw new OverflowException("Operation result out of representable range!");
            return new IntFloat((int) tempRaw);
        }

        public static IntFloat operator /(IntFloat self, IntFloat other)
        {
            long tempRaw = checked(self._rawValue * (long) Scale);
            tempRaw /= other._rawValue;
            if (tempRaw > int.MaxValue || tempRaw < int.MinValue)
                throw new OverflowException("Operation result out of representable range!");
            return new IntFloat((int) tempRaw);
        }

        public static IntFloat operator -(IntFloat self)
        {
            return new IntFloat(-self._rawValue);
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
        
        public static bool operator <=(IntFloat self, IntFloat other)
        {
            return self._rawValue <= other._rawValue;
        }

        public static bool operator >=(IntFloat self, IntFloat other)
        {
            return self._rawValue >= other._rawValue;
        }

        public static IntFloat operator ++(IntFloat self)
        {
            return self + One;
        }

        public static IntFloat operator --(IntFloat self)
        {
            return self - One;
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

        #region Explicit Cast Operators

        public static explicit operator float(IntFloat i)
        {
            return i.toFloat;
        }        
        
        public static explicit operator int(IntFloat i)
        {
            return i._rawValue / Scale;
        }

        public static explicit operator IntFloat(int i)
        {
            return FromInt(i);
        }        
        
        public static explicit operator IntFloat(float f)
        {
            return FromFloat(f);
        }

        #endregion

        #region Other Operations

        public static IntFloat Sqrt(IntFloat self)
        {
            if (self._rawValue < 0)
            {
                throw new ArgumentOutOfRangeException(self._rawValue.ToString() , "Trying to find square root of negative number");
            }

            if (self._rawValue == 0)
            {
                return Zero;
            }

            long num = checked(self._rawValue * (long) Scale);
            
            // courtesy of http://www.codecodex.com/wiki/Calculate_an_integer_square_root#C.23
            long n = num / 2 + 1; 
            long n1 = (n + num / n) / 2;  
            while (n1 < n)
            {  
                n = n1;  
                n1 = (n + num / n) / 2;  
            }
            
            // the original wasn't overflowing, and the square root cannot possibly be larger than the original
            // (except if less than 1, in which case overflow would not occur anyway)
            // if (n > int.MaxValue) throw new OverflowException("Result of square root out of representable range");
            
            return new IntFloat((int) n); 
        }

        public static IntFloat Abs(IntFloat self)
        {
            return new IntFloat(Math.Abs(self._rawValue));
        }

        public static IntFloat Max(IntFloat a, IntFloat b)
        {
            return a > b ? a : b;
        }
        
        public static IntFloat Min(IntFloat a, IntFloat b)
        {
            return a < b ? a : b;
        }

        public static int RoundToInt(IntFloat self)
        {
            int remainder = self._rawValue % Scale;
            int sign = Math.Sign(remainder);
            if (Math.Abs(remainder) >= Scale / 2)
            {
                return self._rawValue / Scale + sign * 1;
            }
            
            return self._rawValue / Scale;
        }

        #endregion

        #region Helper Functions
        
        public static IntFloat FromInt(int i)
        {
            return new IntFloat(i * Scale);
        }

        public static IntFloat FromFloat(float f)
        {
            // use with care: float math risks non-determinism on different machines
            return new IntFloat((int) (f * Scale));
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