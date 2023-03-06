# Use the latest version of Ubuntu
FROM ubuntu:latest

# Set the working directory
WORKDIR /app

# Copy the .NET Core runtime to the image
RUN apt-get update && \
    apt-get install -y wget && \
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-5.0

# Copy the application files to the image
COPY ../../../apichipper .

# Restore the application's dependencies
RUN dotnet restore

# Build the application
RUN dotnet build -c Release

# Publish the application
RUN dotnet publish -c Release -o out

# Set the working directory for the runtime
WORKDIR /app/out

# Expose port 80 for the application
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
