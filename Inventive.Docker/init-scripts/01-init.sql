-- PostgreSQL Initialization Script for Inventive System
-- This script runs automatically when the container is first created

-- Create extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Set timezone to UTC
SET timezone = 'UTC';

-- Database is already created by POSTGRES_DB environment variable
-- Additional initialization can be added here

\echo 'PostgreSQL initialization complete for Inventive database';
