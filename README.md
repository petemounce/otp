# One time passwords

## Getting started

```shell
git clone https://github.com/petemounce/otp.git
cd otp
nuget restore
start otp.sln
# hit F5 to debug, and you should see some swagger docs in your browser.
# if you don't, browse to /swagger/ui/index to see them.

# explore the tests project.
```

## Functionality

[See spec](src/Otp.Web/OneTimePasswords/README.md)

## Comments on infrastructure

[See infrastructure](src/Otp.Web/Infrastructure/README.md)

## Comments on testing approach

[See tests](src/Otp.Tests/README.md)
