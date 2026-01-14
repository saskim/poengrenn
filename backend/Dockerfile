FROM mcr.microsoft.com/dotnet/sdk:10.0.101-noble-arm64v8 AS dev

WORKDIR /src

RUN curl -sSL https://dot.net/v1/dotnet-install.sh \
    | bash /dev/stdin --version 10.0.101 --install-dir /usr/share/dotnet

RUN dotnet tool install -g dotnet-ef --version 10.0.0-preview.7.*
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY backend/Poengrenn.API/Poengrenn.API.csproj backend/Poengrenn.API/
COPY backend/Poengrenn.DAL/Poengrenn.DAL.csproj backend/Poengrenn.DAL/
COPY backend/Poengrenn.sln backend/

RUN dotnet restore backend/Poengrenn.API/Poengrenn.API.csproj

COPY . .

WORKDIR /src/backend/Poengrenn.API

ENV ASPNETCORE_URLS=http://0.0.0.0:8080

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"]
