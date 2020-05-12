using System;
using Xunit;
using Xunit.Abstractions;
using IntFloatLib;

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
            IntFloat two = new IntFloat(20000);
            IntFloat three = new IntFloat(35000);
            Assert.Equal(5.5f, (two + three).toFloat);
        }      
        
        [Fact]
        public void SubtractionTest()
        {
            IntFloat two = new IntFloat(20000);
            IntFloat three = new IntFloat(35000);
            Assert.Equal(-1.5f, (two - three).toFloat);
        }

        [Fact]
        public void DivisionTestOne()
        {
            IntFloat three = new IntFloat(30000);
            IntFloat two = new IntFloat(20000);
            Assert.Equal(1.5f, (three / two).toFloat);
        }           
        
        [Fact]
        public void DivisionTestTwo()
        {
            IntFloat a = new IntFloat(2000);
            IntFloat b = new IntFloat(100000);
            Assert.Equal(0.02f, (a / b).toFloat);
        }       
        
        [Fact]
        public void DivisionTestThree()
        {
            IntFloat a = new IntFloat(100000);
            IntFloat b = new IntFloat(2000);
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
            IntFloat a = new IntFloat(100000);
            IntFloat b = new IntFloat(2000);
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
            Assert.Equal(20f, (20 * IntFloat.One).toFloat);
            Assert.Equal(20f, (IntFloat.FromInt(20)).toFloat);
        }

        [Fact]
        public void SqrtTest()
        {
            Assert.Equal(20f, IntFloat.Sqrt(400 * IntFloat.One).toFloat);
        }        
        
        [Fact]
        public void SqrtTestTwo()
        {
            int whatever = 15231;
            AreEqualWithinPrecision(Math.Sqrt(whatever), IntFloat.Sqrt(whatever * IntFloat.One));
        }

        [Fact]
        public void AbsMaxMinTest()
        {
            Assert.True(IntFloat.Abs(new IntFloat(-1000)) == new IntFloat(1000));
            Assert.True(IntFloat.Max(IntFloat.One, IntFloat.Zero) == IntFloat.One);
            Assert.True(IntFloat.Min(IntFloat.One, IntFloat.Zero) == IntFloat.Zero);
        }

        [Fact]
        public void RoundToIntTest()
        {
            Assert.True(IntFloat.RoundToInt(new IntFloat(25001)) == 3);
            Assert.True(IntFloat.RoundToInt(new IntFloat(25000)) == 3);
            Assert.True(IntFloat.RoundToInt(new IntFloat(24999)) == 2);
        }

        [Fact]
        public void RoundToIntNegTest()
        {
            Assert.True(IntFloat.RoundToInt(new IntFloat(-25001)) == -3);
            Assert.True(IntFloat.RoundToInt(new IntFloat(-25000)) == -3);
            Assert.True(IntFloat.RoundToInt(new IntFloat(-24999)) == -2);
        }

        public static void AreEqualWithinPrecision(double f, IntFloat i)
        {
            Assert.True(Math.Abs(i.toDouble - f) < IntFloat.Epsilon);
        }
    }
}