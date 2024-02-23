#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["erasnap-backend/erasnap-backend.csproj", "erasnap-backend/"]
#RUN dotnet restore "erasnap-backend/erasnap-backend.csproj"
#COPY . .
#WORKDIR "/src/erasnap-backend"
#RUN dotnet build "erasnap-backend.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "erasnap-backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "erasnap-backend.dll"]
#
#-----------------------------------------------------------------

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG TARGETARCH
WORKDIR /src
EXPOSE 5001

COPY ["EraSnapBackend/EraSnapBackend.csproj", "EraSnapBackend/"]
RUN dotnet restore "EraSnapBackend/EraSnapBackend.csproj"  -a $TARGETARCH

COPY . .
WORKDIR "/src/EraSnapBackend"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EraSnapBackend.csproj" -a $TARGETARCH --self-contained -p:PublishSingleFile=true -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine as final
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["/app/EraSnapBackend"]

