FROM mcr.microsoft.com/dotnet/core/sdk:3.1 As build-env
WORKDIR /home/app

COPY . .

RUN dotnet restore 

RUN dotnet build --configuration Release

RUN dotnet publish -c Release -o /home/app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /home/app
COPY --from=build-env /home/app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]