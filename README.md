**Q. What is this?**

A. A simple math library that uses integers to represent floating point numbers. The extent of the representation is determined by the scale.
E.g. if the scale is 100, then integer 100 represents a float of 1; if scale is 1000, then integer 1000 represents a float of 1, etc.

**Q. Why you do this?**

A. Floating point operations are (generally) not deterministic across different platforms and hardware architectures.
Certain applications, such as making a lockstep RTS game, require that the results of float calculations be exactly the same on different machines.
Because int math is deterministic, we can perform calculations on integer representation of floats that will give the same results.

**Q. Isn't this just fixed-point math?**

A. Yes indeed. Traditional fixed point implementations use a scaling factor that are powers of 2.
I've just changed it to powers of 10, because it's more easily convertible from a decimal representation.
Also, this means there are no ridiculous bit shifting operations for multiplication/division.

**Q. Isn't this just `decimal` type?**

A. Not quite, `System.Decimal` uses a certain number of bits to represent the mantissa, and the rest the exponent (plus a sign bit).
Here, we use a scale variable to represent the exponent instead. And because this is fixed point, the scale is const.

**Q. This has rather limited range/precision?**

A. True, but it's enough for my uses. Should you wish to expand the range, you can always change to `long` instead of `int` to store the raw value.
You can also change the scale to increase precision at the expense of range.

**Q. This is so simple?**

A. Yeah, I'm not a very smart guy, so I need something dumb and easy to use :)

**Q. What's missing?**

A. Some trig operations. Feel free to pull request.

**Q. How fast is this?**

I haven't profiled the performance, but aside from the trig/vector operations, the most complex calculations are long arithmetic. 
So theoretically, it's as fast as long math is on your machine, plus the overhead of a function call.