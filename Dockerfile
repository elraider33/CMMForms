FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# ENV ASPNETCORE_HTTPS_PORT=5001
# ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 80
EXPOSE 443
# EXPOSE 5000
# EXPOSE 50010

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN "c:\PROGRA~1\\Git\\mingw64\\bin\\curl.exe" -sL https://deb.nodesource.com/setup_12.x | "C:\\Program Files\\Git\usr\\bin\\bash.exe" -
RUN apt install -y build-essential nodejs
WORKDIR /src
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]
COPY ["src/Library/Library.csproj", "src/Library/"]
RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]