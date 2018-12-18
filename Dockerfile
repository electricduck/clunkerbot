FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY src/CarPupsTelegramBot/*.csproj ./
RUN dotnet restore

COPY src/CarPupsTelegramBot/. ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CarPupsTelegramBot.dll"]