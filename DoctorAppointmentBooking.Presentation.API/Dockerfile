﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedicalAppointment.Presentation.API/MedicalAppointment.Presentation.API.csproj", "MedicalAppointment.Presentation.API/"]
COPY ["DoctorAppointmentBooking.Application/DoctorAppointmentBooking.Application.csproj", "DoctorAppointmentBooking.Application/"]
COPY ["DoctorAppointmentBooking.Domain/DoctorAppointmentBooking.Domain.csproj", "DoctorAppointmentBooking.Domain/"]
COPY ["DoctorAppointmentBooking.Infrastructure/DoctorAppointmentBooking.Infrastructure.csproj", "DoctorAppointmentBooking.Infrastructure/"]
RUN dotnet restore "MedicalAppointment.Presentation.API/MedicalAppointment.Presentation.API.csproj"
COPY . .
WORKDIR "/src/MedicalAppointment.Presentation.API"
RUN dotnet build "MedicalAppointment.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MedicalAppointment.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalAppointment.Presentation.API.dll"]
