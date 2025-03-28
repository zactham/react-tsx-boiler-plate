-- Clear existing data (if needed)
TRUNCATE TABLE "Cryptocurrencies" RESTART IDENTITY CASCADE;
TRUNCATE TABLE "PriceDataPoints" RESTART IDENTITY CASCADE;

-- Insert sample cryptocurrencies
INSERT INTO "Cryptocurrencies" ("Symbol", "Name", "CurrentPrice", "MarketCap", "Volume24h", "CirculatingSupply", "PriceChangePercentage24h", "ImageUrl") VALUES
('btc', 'Bitcoin', 66824.56, 1312485639226, 24635791924, 19642525, 1.76, 'https://assets.coingecko.com/coins/images/1/large/bitcoin.png'),
('eth', 'Ethereum', 3521.48, 423417891345, 11736924567, 120237582, 3.24, 'https://assets.coingecko.com/coins/images/279/large/ethereum.png'),
('usdt', 'Tether', 1.00, 99456789123, 76542198345, 99432547125, 0.01, 'https://assets.coingecko.com/coins/images/325/large/Tether.png'),
('bnb', 'BNB', 612.35, 93578451245, 1543879542, 152850450, 2.45, 'https://assets.coingecko.com/coins/images/825/large/bnb-icon2_2x.png'),
('sol', 'Solana', 156.42, 68947521564, 2584762145, 441043217, 5.67, 'https://assets.coingecko.com/coins/images/4128/large/solana.png'),
('xrp', 'XRP', 0.5324, 29456123784, 1423578945, 55321547895, -1.24, 'https://assets.coingecko.com/coins/images/44/large/xrp-symbol-white-128.png'),
('usdc', 'USD Coin', 1.00, 32154789632, 3541278965, 32154789632, 0.02, 'https://assets.coingecko.com/coins/images/6319/large/usdc.png'),
('ada', 'Cardano', 0.4512, 15987456321, 312547896, 35432156987, -0.78, 'https://assets.coingecko.com/coins/images/975/large/cardano.png'),
('doge', 'Dogecoin', 0.1423, 18957423687, 914578235, 133217854369, 2.54, 'https://assets.coingecko.com/coins/images/5/large/dogecoin.png'),
('dot', 'Polkadot', 6.87, 9874563215, 254136987, 1437856214, 1.12, 'https://assets.coingecko.com/coins/images/12171/large/polkadot.png');

-- Create function to generate price history data
CREATE OR REPLACE FUNCTION generate_price_history(
    p_crypto_id INT, 
    p_base_price DECIMAL, 
    p_volatility DECIMAL DEFAULT 0.05
) RETURNS VOID AS $$
DECLARE
    current_date TIMESTAMP := NOW();
    days_back INT;
    price DECIMAL;
    vol DECIMAL;
    mcap DECIMAL;
    random_change DECIMAL;
BEGIN
    -- Generate 90 days of price history
    FOR days_back IN 0..90 LOOP
        -- Calculate random price change based on volatility
        random_change := (random() * p_volatility * 2) - p_volatility;
        
        -- Calculate price for this day with some trend
        price := p_base_price * (1 + random_change + (days_back * 0.001));
        
        -- Generate random volume
        vol := p_base_price * 1000000 * (random() * 10 + 5);
        
        -- Calculate market cap based on circulating supply (simplified)
        mcap := price * (SELECT "CirculatingSupply" FROM "Cryptocurrencies" WHERE "Id" = p_crypto_id);
        
        -- Insert the price point
        INSERT INTO "PriceDataPoints" ("CryptocurrencyId", "Timestamp", "Price", "Volume", "MarketCap")
        VALUES (
            p_crypto_id,
            current_date - (days_back || ' days')::INTERVAL,
            price,
            vol,
            mcap
        );
    END LOOP;
END;
$$ LANGUAGE plpgsql;

-- Generate price history for each cryptocurrency
SELECT generate_price_history(1, 66000, 0.03); -- Bitcoin
SELECT generate_price_history(2, 3400, 0.04);  -- Ethereum
SELECT generate_price_history(3, 1, 0.001);    -- Tether
SELECT generate_price_history(4, 600, 0.035);  -- BNB
SELECT generate_price_history(5, 150, 0.06);   -- Solana
SELECT generate_price_history(6, 0.53, 0.05);  -- XRP
SELECT generate_price_history(7, 1, 0.001);    -- USDC
SELECT generate_price_history(8, 0.45, 0.04);  -- Cardano
SELECT generate_price_history(9, 0.14, 0.07);  -- Dogecoin
SELECT generate_price_history(10, 6.8, 0.045); -- Polkadot

-- Drop the function after use
DROP FUNCTION generate_price_history;

-- Verify data
SELECT COUNT(*) FROM "Cryptocurrencies";
SELECT COUNT(*) FROM "PriceDataPoints";
