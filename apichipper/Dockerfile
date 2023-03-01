# Use the latest version of Ubuntu
FROM ubuntu:latest

# Set the working directory
WORKDIR /app

# Install the .NET Core runtime
RUN apt-get update \
    && apt-get install -y curl gnupg \
    && curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/microsoft-prod.list \
    && apt-get update \
    && apt-get install -y aspnetcore-runtime-3.1

# Copy the web API files to the working directory
COPY ./bin/Release/net7.0/publish/ .

# Set the ASPNETCORE_URLS environment variable
ENV ASPNETCORE_URLS=http://+:80

# Expose port 80 for incoming traffic
EXPOSE 80

# Start the web API
ENTRYPOINT ["dotnet", "apichipper.dll"]
