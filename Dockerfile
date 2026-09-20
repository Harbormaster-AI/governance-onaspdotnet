FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY governanceonaspdotnet/Api.csproj governanceonaspdotnet/
RUN dotnet restore governanceonaspdotnet/Api.csproj

COPY governanceonaspdotnet/ governanceonaspdotnet/

RUN dotnet publish governanceonaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]