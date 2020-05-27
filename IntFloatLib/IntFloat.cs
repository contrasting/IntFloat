using System;
using System.Runtime.Serialization;

namespace IntFloatLib
{
    [Serializable]
    public readonly struct IntFloat : IEquatable<IntFloat>, IComparable<IntFloat>, ISerializable
    {
        public IntFloat(int value)
        {
            _rawValue = value;
        }

        public IntFloat(SerializationInfo info, StreamingContext context)
        {
            _rawValue = info.GetInt32(nameof(rawValue));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(rawValue), _rawValue);
        }

        #region Constants
        
        public const int Scale = 1000;
        public const float Epsilon = 1f / Scale;
        public const float MaxValue = int.MaxValue / (float) Scale;
        
        public static readonly IntFloat Zero = new IntFloat();
        public static readonly IntFloat One = new IntFloat(Scale);
        // values dependent on Scale
        public static readonly IntFloat TwoPi = new IntFloat(6283);
        public static readonly IntFloat Pi = new IntFloat(3142);
        public static readonly IntFloat PiOver2 = new IntFloat(1571);
        
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
                throw new OverflowException($"Raw value {tempRaw} is out of representable range!");
            return new IntFloat((int) tempRaw);
        }

        public static IntFloat operator /(IntFloat self, IntFloat other)
        {
            long tempRaw = checked(self._rawValue * (long) Scale);
            tempRaw /= other._rawValue;
            if (tempRaw > int.MaxValue || tempRaw < int.MinValue)
                throw new OverflowException($"Raw value {tempRaw} is out of representable range!");
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
        
        public static IntFloat operator /(IntFloat self, int i)
        {
            // divide by int, can't possibly overflow
            return new IntFloat(self._rawValue / i);
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

        public static IntFloat Square(IntFloat self)
        {
            return self * self;
        }

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

        #region Vector Operations

        /// <summary>
        /// Returns the magnitude of a vector (a, b). Provided because often the square magnitude will overflow.
        /// </summary>
        public static IntFloat Magnitude(IntFloat a, IntFloat b)
        {
            long num = checked(a._rawValue * (long) a._rawValue + b._rawValue * (long) b._rawValue);
            
            if (num == 0)
            {
                return Zero;
            }

            long n = num / 2 + 1; 
            long n1 = (n + num / n) / 2;  
            while (n1 < n)
            {  
                n = n1;  
                n1 = (n + num / n) / 2;  
            }
            
            if (n > int.MaxValue) throw new OverflowException("Result of square root out of representable range");
            
            return new IntFloat((int) n);
        }

        // courtesy of https://www.dsprelated.com/showarticle/1052.php
        public static IntFloat Atan(IntFloat z)
        {
            // note: change these constants if you change the scale
            IntFloat n1 = new IntFloat(972);
            IntFloat n2 = new IntFloat(-192);
            return (n1 + n2 * z * z) * z;
        }
        
        /// <summary>
        /// Note: the precision of this function can fluctuate.
        /// </summary>
        public static IntFloat Atan2(IntFloat y, IntFloat x)
        {
            if (x != Zero)
            {
                if (Abs(x) > Abs(y))
                {
                    IntFloat z = y / x;
                    if (x > Zero)
                    {
                        // atan2(y,x) = atan(y/x) if x > 0
                        return Atan(z);
                    }
                    else if (y >= Zero)
                    {
                        // atan2(y,x) = atan(y/x) + PI if x < 0, y >= 0
                        return Atan(z) + Pi;
                    }
                    else
                    {
                        // atan2(y,x) = atan(y/x) - PI if x < 0, y < 0
                        return Atan(z) - Pi;
                    }
                }
                else // Use property atan(y/x) = PI/2 - atan(x/y) if |y/x| > 1.
                {
                    IntFloat z = x / y;
                    if (y > Zero)
                    {
                        // atan2(y,x) = PI/2 - atan(x/y) if |y/x| > 1, y > 0
                        return -Atan(z) + PiOver2;
                    }
                    else
                    {
                        // atan2(y,x) = -PI/2 - atan(x/y) if |y/x| > 1, y < 0
                        return -Atan(z) - PiOver2;
                    }
                }
            }
            else
            {
                if (y > Zero) // x = 0, y > 0
                {
                    return PiOver2;
                }
                else if (y < Zero) // x = 0, y < 0
                {
                    return -PiOver2;
                }
            }
            return Zero; // x,y = 0. Could return NaN instead.
        }

        // courtesy of https://www.coranac.com/2009/07/sines/.
        // Plot at https://www.desmos.com/calculator/xxkkb0gmvw
        // I'm using a fifth order polynomial approximation
        public static IntFloat Sin(IntFloat x)
        {
            // clamp to unit circle
            if (x > Pi)
            {
                while (x > Pi) x -= TwoPi;
            }
            else if (x < -Pi)
            {
                while (x < -Pi) x += TwoPi;
            }
            
            if (x > PiOver2)
            {
                return Sin(Pi - x);
            }
            else if (x < -PiOver2)
            {
                return Sin(-Pi - x);
            }

            IntFloat a = Square(2 * x / Pi);
            IntFloat b = Pi - a * ((TwoPi - 5 * One) - a * (Pi - 3 * One));
            // multiply FIRST then divide preserves more accuracy!
            IntFloat result = x * b / Pi;

            // clamp to abs value of 1
            return result > Zero ? Min(result, One) : Max(result, -One);
        }

        // cos is just shift of sin by -pi/2    
        public static IntFloat Cos(IntFloat x)
        {
            return Sin(x + PiOver2);
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