FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY src/ClunkerBot/*.csproj ./
RUN dotnet restore

COPY src/ClunkerBot/. ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ClunkerBot.dll"]