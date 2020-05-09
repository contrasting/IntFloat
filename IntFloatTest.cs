using System;
using Xunit;

namespace IntFloat
{
    public class IntFloatTest
    {
        [Fact]
        public void AdditionTest()
        {
            IntFloat two = new IntFloat(20000);
            IntFloat three = new IntFloat(35000);
            Assert.Equal(5.5f, (two + three).toFloat);
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

        public static void AreEqualWithinPrecision(float f, IntFloat i)
        {
            Assert.True(Math.Abs(i.toFloat - f) < IntFloat.Epsilon);
        }
    }
}