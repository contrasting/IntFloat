using System;
using IntFloatLib;
using Xunit;
using Xunit.Abstractions;
using static IntFloatLib.IntFloat;

namespace IntFloatTest
{
    public class IntFloatTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public IntFloatTest(ITestOutputHelper _outputHelper)
        {
            this._outputHelper = _outputHelper;
        }
        
        [Fact]
        public void AdditionTest()
        {
            IntFloat two = FromInt(2);
            IntFloat threeHalf = FromFloat(3.5f);
            Assert.Equal(5.5f, (two + threeHalf).toFloat);
        }      
        
        [Fact]
        public void SubtractionTest()
        {
            IntFloat two = FromInt(2);
            IntFloat threeHalf = FromFloat(3.5f);
            Assert.Equal(-1.5f, (two - threeHalf).toFloat);
        }

        [Fact]
        public void DivisionTestOne()
        {
            IntFloat three = FromInt(3);
            IntFloat two = FromInt(2);
            Assert.Equal(1.5f, (three / two).toFloat);
        }           
        
        [Fact]
        public void DivisionTestTwo()
        {
            IntFloat a = FromFloat(0.2f);
            IntFloat b = FromInt(10);
            Assert.Equal(0.02f, (a / b).toFloat);
        }       
        
        [Fact]
        public void DivisionTestThree()
        {
            IntFloat a = FromInt(10);
            IntFloat b = FromFloat(0.2f);
            Assert.Equal(50f, (a / b).toFloat);
        }
        
        [Fact]
        public void DivisionTestFour()
        {
            IntFloat a = new IntFloat(2125);
            IntFloat b = new IntFloat(98143);
            AreEqualWithinPrecision(0.212f / 9.8143f, a / b);
        }         
        
        [Fact]
        public void DivisionTestFive()
        {
            IntFloat a = new IntFloat(120000);
            IntFloat b = new IntFloat(20001);
            AreEqualWithinPrecision(12f / 2.0001f, a / b);
        }       

        [Fact]
        public void MultiplicationTest()
        {
            IntFloat a = FromInt(10);
            IntFloat b = FromFloat(0.2f);
            Assert.Equal(2f, (a * b).toFloat);
        }

        [Fact]
        public void MultiplicationTestTwo()
        {
            IntFloat a = new IntFloat(365004);
            IntFloat b = new IntFloat(2012);
            AreEqualWithinPrecision(36.5004f * 0.2012f, a * b);
        }

        [Fact]
        public void OverflowCheck()
        {
            IntFloat a = new IntFloat(202050365);
            IntFloat b = new IntFloat(200002012);

            Assert.Throws<OverflowException>(() =>
            {
                IntFloat c = a * b;
            });
        }

        [Fact]
        public void MultiplyByIntTest()
        {
            Assert.Equal(20f, (20 * One).toFloat);
            Assert.Equal(20f, (FromInt(20)).toFloat);
        }

        [Fact]
        public void SqrtTest()
        {
            Assert.Equal(20f, Sqrt(400 * One).toFloat);
        }        
        
        [Fact]
        public void SqrtTestTwo()
        {
            int whatever = 15231;
            AreEqualWithinPrecision(Math.Sqrt(whatever), Sqrt(whatever * One));
        }

        [Fact]
        public void AbsMaxMinTest()
        {
            Assert.True(Abs(new IntFloat(-1000)) == new IntFloat(1000));
            Assert.True(Max(One, Zero) == One);
            Assert.True(Min(One, Zero) == Zero);
        }

        [Fact]
        public void RoundToIntTest()
        {
            int halfScale = Scale / 2;
            Assert.True(RoundToInt(new IntFloat(halfScale * 5 + 1)) == 3);
            Assert.True(RoundToInt(new IntFloat(halfScale * 5)) == 3);
            Assert.True(RoundToInt(new IntFloat(halfScale * 5 - 1)) == 2);
        }

        [Fact]
        public void RoundToIntNegTest()
        {
            int halfScale = Scale / 2;
            Assert.True(RoundToInt(new IntFloat(-halfScale * 5 - 1)) == -3);
            Assert.True(RoundToInt(new IntFloat(-halfScale * 5)) == -3);
            Assert.True(RoundToInt(new IntFloat(-halfScale * 5 + 1)) == -2);
        }

        [Fact]
        public void VectorMagnitudeTest()
        {
            IntFloat a = 3 * One;
            IntFloat b = 4 * One;
            Assert.True(Magnitude(a, b) == 5 * One);
        }

        [Fact]
        public void PlusPlusTest()
        {
            IntFloat a = One;
            a++;
            Assert.True(a == 2 * One);
        }

        [Fact]
        public void Atan2TestOne()
        {
            IntFloat y = new IntFloat(1732); // root 3
            IntFloat x = FromInt(3);
            _outputHelper.WriteLine((Pi / 6).ToString());
            _outputHelper.WriteLine(Atan2(y, x).ToString());
            AreEqualWithinPrecision(Pi / 6, Atan2(y, x));
        }        
        
        [Fact]
        public void Atan2TestTwo()
        {
            IntFloat y = FromInt(3);
            IntFloat x = FromInt(3);
            _outputHelper.WriteLine((Pi / 4).ToString());
            _outputHelper.WriteLine(Atan2(y, x).ToString());
            AreEqualWithinPrecision(Pi / 4,Atan2(y, x));
        }        
        
        [Fact]
        public void Atan2TestThree()
        {
            IntFloat y = FromInt(3);
            IntFloat x = FromRaw(1732);
            _outputHelper.WriteLine((Pi / 3).ToString());
            _outputHelper.WriteLine(Atan2(y, x).ToString());
            AreEqualWithinPrecision(Pi / 3, Atan2(y, x));
        }
        
        [Fact]
        public void Atan2TestFour()
        {
            IntFloat y = FromInt(-3);
            IntFloat x = FromInt(-3);
            _outputHelper.WriteLine((-3 * Pi / 4).ToString());
            _outputHelper.WriteLine(Atan2(y, x).ToString());
            AreEqualWithinPrecision(-3 * Pi / 4,Atan2(y, x));
        }       
        
        [Fact]
        public void SinTestOne()
        {
            _outputHelper.WriteLine(Sin(Pi).ToString());
            AreEqualWithinPrecision(Sin(Pi), Zero);
        }               
        
        [Fact]
        public void SinTestTwo()
        {
            _outputHelper.WriteLine(Sin(Pi / 6).ToString());
            AreEqualWithinPrecision(Sin(Pi / 6), One / 2);
        }            
        
        [Fact]
        public void SinTestThree()
        {
            IntFloat y = new IntFloat(1732); // root 3
            IntFloat x = FromInt(2);
            _outputHelper.WriteLine(Sin(Pi / 3).ToString());
            _outputHelper.WriteLine((y / x).ToString());
            AreEqualWithinPrecision(Sin(Pi / 3), y / x);
        }           
        
        [Fact]
        public void SinTestFour()
        {
            IntFloat y = new IntFloat(1732); // root 3
            IntFloat x = FromInt(2);
            _outputHelper.WriteLine(Sin(-Pi + Pi / 3).ToString());
            _outputHelper.WriteLine((-y / x).ToString());
            AreEqualWithinPrecision(Sin(-Pi + Pi / 3), -y / x);
        }            
        
        [Fact]
        public void SinTestFive()
        {
            _outputHelper.WriteLine(Sin(PiOver2).ToString());
            AreEqualWithinPrecision(Sin(PiOver2), One);
        }       
        
        [Fact]
        public void CosTestOne()
        {
            _outputHelper.WriteLine(Cos(Pi).ToString());
            AreEqualWithinPrecision(Cos(Pi), -One);
        }       
        
        [Fact]
        public void CosTestTwo()
        {
            _outputHelper.WriteLine(Cos(Pi / 3).ToString());
            AreEqualWithinPrecision(Cos(Pi / 3), One / 2);
        }      
        
        
        [Fact]
        public void CosTestThree()
        {
            IntFloat y = new IntFloat(1732); // root 3
            IntFloat x = FromInt(2);
            _outputHelper.WriteLine(Cos(Pi / 6).ToString());
            _outputHelper.WriteLine((y / x).ToString());
            AreEqualWithinPrecision(Cos(Pi / 6), y / x);
        }           
        
        [Fact]
        public void CosTestFour()
        {
            IntFloat y = new IntFloat(1732); // root 3
            IntFloat x = FromInt(2);
            _outputHelper.WriteLine(Cos(-Pi + Pi / 6).ToString());
            _outputHelper.WriteLine((-y / x).ToString());
            AreEqualWithinPrecision(Cos(-Pi + Pi / 6), -y / x);
        }            
        
        [Fact]
        public void CosTestFive()
        {
            _outputHelper.WriteLine(Cos(PiOver2).ToString());
            AreEqualWithinPrecision(Cos(PiOver2), Zero);
        }       
        
        public static void AreEqualWithinPrecision(double f, IntFloat i)
        {
            Assert.True(Math.Abs(i.toDouble - f) < Epsilon);
        }
        
        public static void AreEqualWithinPrecision(IntFloat a, IntFloat b)
        {
            Assert.True(Abs(a - b).rawValue <= 1);
        }
    }
}