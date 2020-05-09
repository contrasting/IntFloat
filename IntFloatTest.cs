using System;
using Xunit;

namespace IntFloat
{
    public class IntFloatTest
    {
        [Fact]
        public void AdditionTest()
        {
            IntFloat two = new IntFloat(2000);
            IntFloat three = new IntFloat(3500);
            Assert.Equal(5.5f, (two + three).toFloat);
        }

        [Fact]
        public void DivisionTestOne()
        {
            IntFloat three = new IntFloat(3000);
            IntFloat two = new IntFloat(2000);
            Assert.Equal(1.5f, (three / two).toFloat);
        }           
        
        [Fact]
        public void DivisionTestTwo()
        {
            IntFloat a = new IntFloat(200);
            IntFloat b = new IntFloat(10000);
            Assert.Equal(0.02f, (a / b).toFloat);
        }       
        
        [Fact]
        public void DivisionTestThree()
        {
            IntFloat a = new IntFloat(10000);
            IntFloat b = new IntFloat(200);
            Assert.Equal(50f, (a / b).toFloat);
        }
        
        [Fact]
        public void DivisionTestFour()
        {
            IntFloat a = new IntFloat(212);
            IntFloat b = new IntFloat(9814);
            AreEqualWithinPrecision(0.212f / 9.814f, a / b);
        }         
        
        [Fact]
        public void DivisionTestFive()
        {
            IntFloat a = new IntFloat(12000);
            IntFloat b = new IntFloat(2001);
            AreEqualWithinPrecision(12f / 2.001f, a / b);
        }       

        [Fact]
        public void MultiplicationTest()
        {
            IntFloat a = new IntFloat(10000);
            IntFloat b = new IntFloat(200);
            Assert.Equal(2f, (a * b).toFloat);
        }

        [Fact]
        public void MultiplicationTestTwo()
        {
            IntFloat a = new IntFloat(36504);
            IntFloat b = new IntFloat(212);
            AreEqualWithinPrecision(36.504f * 0.212f, a * b);
        }

        public static void AreEqualWithinPrecision(float f, IntFloat i)
        {
            Assert.True(Math.Abs(i.toFloat - f) < IntFloat.Epsilon);
        }
    }
}