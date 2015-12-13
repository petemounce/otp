# One time passwords

## Getting started

```shell
git clone https://github.com/petemounce/otp.git
cd otp
nuget restore
start otp.sln
# hit F5 to debug
start "http://localhost:45457/swagger/ui/index"
# should see swagger docs there in your browser
# explore the tests project.
```

## Functionality

[See spec](src/Otp.Web/OneTimePasswords/README.md)

## Comments on infrastructure

[See infrastructure](src/Otp.Web/Infrastructure/README.md)

## Comments on testing approach

[See tests](src/Otp.Tests/README.md)
