# Tests

I'm a fan of testing at the highest level that makes sense. I want my tests to cover as much actual code as possible, yet still run fast. Now .NET finally has a reasonably way of doing in-memory full-stack tests, I default to that.

So, everything in here gets exercised, and I rely on good logging to debug, not the debugger. If I find myself wanting a debugger, I instead inject logs at class/method boundaries, usually via decorator pattern.
