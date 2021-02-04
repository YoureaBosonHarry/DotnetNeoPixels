#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim-amd64 AS build
WORKDIR /src
COPY ["DotnetNeoPixels.csproj", "DotnetNeoPixels/"]
RUN dotnet restore "DotnetNeoPixels/DotnetNeoPixels.csproj"
COPY . .
WORKDIR "/src/DotnetNeoPixels"
COPY . .
RUN dotnet build "DotnetNeoPixels.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetNeoPixels.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7 AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get -y update
RUN apt-get install -y scons git gcc
RUN git clone https://github.com/jgarff/rpi_ws281x.git
RUN cd rpi_ws281x && scons && gcc -shared -o ws2811.so *.o && cp ws2811.so /usr/lib
ENTRYPOINT ["dotnet", "DotnetNeoPixels.dll"]