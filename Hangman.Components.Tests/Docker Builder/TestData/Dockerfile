# Use the official PostgreSQL image from Docker Hub
FROM postgres:latest

# Set environment variables, if needed (optional)
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=mypassword
ENV POSTGRES_DB=testDatabase


# Copy 'filea.sql' and 'fileb.sql' from the current directory to '/docker-entrypoint-initdb.d/'
COPY TestData/createDatabase.sql /docker-entrypoint-initdb.d/
COPY TestData/populateDatabase.sql /docker-entrypoint-initdb.d/

# Expose port 5432 (PostgreSQL's default port)
EXPOSE 5432

CMD ["postgres"]