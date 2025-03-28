// src/types/crypto.ts

export interface Cryptocurrency {
  id: number;
  symbol: string;
  name: string;
  currentPrice: number;
  marketCap: number;
  volume24h: number;
  circulatingSupply: number;
  priceChangePercentage24h: number;
  imageUrl: string;
}

export interface PriceHistoryPoint {
  timestamp: string;
  price: number;
}

export interface CryptoPriceHistory {
  id: number;
  symbol: string;
  name: string;
  priceHistory: PriceHistoryPoint[];
  timeRange: TimeRange;
}

export enum TimeRange {
  DAY = '1d',
  WEEK = '7d',
  MONTH = '30d',
  YEAR = '1y',
  ALL = 'all'
}

export interface CryptoState {
  cryptoList: Cryptocurrency[];
  selectedCrypto: Cryptocurrency | null;
  priceHistory: CryptoPriceHistory | null;
  loading: boolean;
  error: string | null;
  timeRange: TimeRange;
}