-- This script runs automatically when PostgreSQL container starts for the first time

CREATE DATABASE "Inventive";

-- Grant all privileges to postgres user
GRANT ALL PRIVILEGES ON DATABASE "Inventive" TO postgres;
