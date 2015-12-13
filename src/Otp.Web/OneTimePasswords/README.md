# OneTimePasswords

Write a program or API that can generate a one-time password and verify if one password it is valid for one user. The input of the program should be the: User Id to generate the password ad the User Id and the password to verify the valid of the password. Every generated password should be valid for 30 seconds.

## Meta

I like to organise projects along functional lines rather than component lines. So - directories/namespaces for units of functionality. I like to do that because it means
* it subtly promotes Single Responsibility Principle but at a level above classes (and hopefully permeates down to the class unit level too)
* they can be worked on in isolation, and hopefully changes only happen within one unit at a time
  * this means changes are easier to review, and therefore faster to deliver
* it means the units of functionality are easier to pick up and move when the need to do component- or architecture-level refactoring arises. It seems like a no-effort expense for a large payback possibility, so I do it.
