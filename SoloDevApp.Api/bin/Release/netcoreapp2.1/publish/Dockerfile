FROM mcr.microsoft.com/dotnet/aspnet:2.1
WORKDIR /app
COPY . /app

RUN apt-get update \
&& apt-get install -y --allow-unauthenticated \
    libc6-dev \
    libgdiplus \
    libx11-dev \
 && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "SoloDevApp.Api.dll"]