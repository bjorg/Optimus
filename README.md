Optimus
=======

The objective of the Optimus library is to complement or replace standard functionality found in the .Net/Mono framework 
with more performant alternatives. Each method implementation must be provided with a rational and evidence that it is
and improvement over the existing alternatives.

Origin
------
I have a passion for fast code and I obsess on squeezing out every last ounce of performance when I set my mind to it.  
However, the .Net frameworks was really not designed for people like me.  Instead, many interfaces and methods seem to 
have been designed for ease of comprehension and use.

For many years, I have tried to fit my needs into this framework and step out of its design only when absolutely needed.  
My motivation was that by restraining myself to stay within the boundaries of the standard framework, it would make it 
easier for others to maintain my code.

Alas, this approach is leading to a death by a million cuts.  It is not that the .Net framework implementations are bad, 
though some are.  It is that the sum of the poor patterns and poor implementations eventually leads to significant 
degradation of performance that then requires enourmouse effort to fix.

Optimus is my attempt at providing missing or alternative implementations that are designed to be efficient.  I want 
Optimus to follow the *[Kaizen](http://en.wikipedia.org/wiki/Kaizen)* philosophy of *continuous improvement of practices 
and efficiency*.  This also means that if a new pattern is found to be better than an old one, but is not backwards 
compatible, I will have no reservations to making the change.  Optimus is about discovering and docummeting optimal 
patterns for .Net/Mono.

I invite you to join me on this journey and I look forward learning from everyone on it.

\- [@bjorg](http://twitter.com/bjorg)

Rules of Engagement
-------------------
I welcome all contributions to Optimus.  However, to achieve our objective, I'm setting down some requirements that
will help us get there.  All pull-requests will be measured against these.  Please make sure that your submission
passes these requirements before you submit the pull-request.

* Code must be formatted to be induistinguishable from other code.
* Every implementation has unit-test coverage.
* Every implementation has a time and memory performance test.
* Every implementation has a rationale for its existence.

Code Formatting
---------------
* Always use 4 spaces for indentation.
* Never use tabs.
* Always place { at the end of the same line, preceeded by a space.
* Always indent the matching } to the same column as the first character on the line where the opening { was.
* Always use { } after if, else, switch, foreach, for, while, do, try, catch, finally, using, unsafe, fixed.
* Never use a space between if/switch/foreach/for/while/catch/using/fixed and (.

License
-------
Optimus is an open source library with an Apache 2.0 license.

Performance Status
==================

This section shows the current status for provided implementations.  Note that since this library is not yet released, 
none of the implementations are considered production ready.

## Optimus.Text.StringRange

### Split() - FAIL

StringRange.Split() uses the **yield** operator to create an an implicit iterator.  From the results below, this seems 
to create an *enormous* amount of memory overhead.

The native String.Split() method is faster for longer strings, which may have to do with it being natively implemented,
or the managed version suffering from more aggressive bound checks (or the callback overhead).

**Running Optimus.PerfTests.OptimusTest.Text.StringRangeTest.Split.Split\_long\_strings ...**

        --- String.Split(long-strings) with loop ---
        #0: 4,096 bytes, 0.236 ms
        #1: 118,784 bytes, 1.373 ms
        #2: 8,192 bytes, 17.748 ms
        #3: 8,192 bytes, 172.521 ms
        #4: 20,480 bytes, 1,729.911 ms
        #5: 212,992 bytes, 17,223.379 ms

        --- StringRange.Split(long-strings) with loop ---
        #0: 0 bytes, 0.239 ms
        #1: 0 bytes, 2.116 ms
        #2: 118,784 bytes, 20.724 ms
        #3: 425,984 bytes, 213.324 ms
        #4: 180,224 bytes, 2,150.069 ms
        #5: 335,872 bytes, 21,793.517 ms

**Running Optimus.PerfTests.OptimusTest.Text.StringRangeTest.Split.Split\_short\_strings ...**
        
        --- String.Split(short-strings) with loop ---
        #0: 0 bytes, 0.064 ms
        #1: 45,056 bytes, 0.582 ms
        #2: 8,192 bytes, 7.631 ms
        #3: 122,880 bytes, 73.628 ms
        #4: 73,728 bytes, 732.697 ms
        #5: 286,720 bytes, 7,351.504 ms

        --- StringRange.Split(short-strings) with loop ---
        #0: 0 bytes, 0.112 ms
        #1: 4,096 bytes, 0.657 ms
        #2: 114,688 bytes, 5.611 ms
        #3: 327,680 bytes, 59.026 ms
        #4: 344,064 bytes, 602.991 ms
        #5: 507,904 bytes, 6,033.464 ms
