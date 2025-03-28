-- Create tables
DROP TABLE IF EXISTS "PriceDataPoints";
DROP TABLE IF EXISTS "Cryptocurrencies";
DROP TABLE IF EXISTS "__EFMigrationsHistory";

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE IF NOT EXISTS "Cryptocurrencies" (
    "Id" SERIAL PRIMARY KEY,
    "Symbol" VARCHAR(10) NOT NULL,
    "Name" VARCHAR(100) NOT NULL,
    "CurrentPrice" DECIMAL(18,8) NOT NULL,
    "MarketCap" DECIMAL(24,2) NOT NULL,
    "Volume24h" DECIMAL(24,2) NOT NULL,
    "CirculatingSupply" DECIMAL(24,2) NOT NULL,
    "PriceChangePercentage24h" DECIMAL(10,2) NOT NULL,
    "ImageUrl" VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS "PriceDataPoints" (
    "Id" SERIAL PRIMARY KEY,
    "CryptocurrencyId" INT NOT NULL,
    "Timestamp" TIMESTAMP WITH TIME ZONE NOT NULL,
    "Price" DECIMAL(18,8) NOT NULL,
    "Volume" DECIMAL(24,2) NOT NULL,
    "MarketCap" DECIMAL(24,2) NOT NULL,
    CONSTRAINT "FK_PriceDataPoints_Cryptocurrencies_CryptocurrencyId" FOREIGN KEY ("CryptocurrencyId") REFERENCES "Cryptocurrencies"("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_PriceDataPoints_CryptocurrencyId_Timestamp" 
ON "PriceDataPoints" ("CryptocurrencyId", "Timestamp");

-- Insert migration history
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250314174204_InitialCreate', '9.0.3'),
       ('20250314182712_FixSeedData', '9.0.3');
