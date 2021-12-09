FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /var/app
COPY *.csproj /var/app/
RUN dotnet restore
COPY . /var/app/
RUN dotnet publish -c release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /var/app
COPY --from=build /var/app/publish .
EXPOSE 5000
CMD [ "dotnet","EchoServer.dll" ]