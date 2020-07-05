FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TutorsOfMogilev_NetCore/TutorsOfMogilev_NetCore.csproj", "TutorsOfMogilev_NetCore/"]
COPY ["DataEntity/DataEntity.csproj", "DataEntity/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Modules/Modules.csproj", "Modules/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "TutorsOfMogilev_NetCore/TutorsOfMogilev_NetCore.csproj"
COPY . .
WORKDIR /src/TutorsOfMogilev_NetCore
RUN dotnet build "TutorsOfMogilev_NetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TutorsOfMogilev_NetCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED true
ENV ASPNETCORE_ENVIRONMENT Development
ENV DOTNET_RUNNING_IN_CONTAINER	true

EXPOSE 80

ENTRYPOINT ["dotnet", "TutorsOfMogilev_NetCore.dll"]