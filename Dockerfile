FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY *.sln .
COPY Terminal/*.csproj ./Terminal/
COPY TerminalUnitTests/*.csproj ./TerminalUnitTests/
RUN dotnet restore

COPY . .
RUN dotnet build

# run the unit tests
FROM build AS test
WORKDIR /app/TerminalUnitTests/
RUN dotnet test --logger:"console;verbosity=detailed"

