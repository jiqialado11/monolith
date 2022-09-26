#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update
RUN apt-get -y install gss-ntlmssp
ADD /SubContractors.API/dacertca.crt /usr/local/share/ca-certificates/rootca.crt
RUN chmod 644 /usr/local/share/ca-certificates/rootca.crt 
RUN update-ca-certificates || exit 0
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SubContractors.API/SubContractors.API.csproj", "SubContractors.API/"]
COPY ["SubContractors.Application/SubContractors.Application.csproj", "SubContractors.Application/"]
COPY ["SubContractors.Infrastructure/SubContractors.Infrastructure.csproj", "SubContractors.Infrastructure/"]
COPY ["SubContractors.Domain/SubContractors.Domain.csproj", "SubContractors.Domain/"]
COPY ["SubContractors.Common/SubContractors.Common.csproj", "SubContractors.Common/"]
RUN dotnet restore "SubContractors.API/SubContractors.API.csproj"
COPY . .
WORKDIR "/src/SubContractors.API"
RUN dotnet build "SubContractors.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SubContractors.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SubContractors.API.dll"]