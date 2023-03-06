# Use the official PostgreSQL image as the base image
FROM postgres:12

# Set the working directory to /var/lib/postgresql
WORKDIR /var/lib/postgresql

# Create a data volume for the database
VOLUME ["/var/lib/postgresql/data"]

# Set the environment variable for the default user and database
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=turhan99
ENV POSTGRES_DB=postgres

# Copy the SQL script that creates the schema and sample data
#COPY schema.sql /docker-entrypoint-initdb.d/

# Expose the default PostgreSQL port
EXPOSE 5432