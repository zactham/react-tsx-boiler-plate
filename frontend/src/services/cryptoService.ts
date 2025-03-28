import axios from 'axios';
import { Cryptocurrency, CryptoPriceHistory, TimeRange } from '../types/crypto';

// Use environment variable for API URL
const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || 'http://localhost:5283/api';

// Create axios instance with common configuration
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Fetch list of cryptocurrencies
export const fetchCryptoList = async (): Promise<Cryptocurrency[]> => {
  try {
    const response = await apiClient.get<Cryptocurrency[]>('/cryptocurrencies');
    return response.data;
  } catch (error) {
    console.error('Error fetching cryptocurrency list:', error);
    throw new Error('Failed to fetch cryptocurrency list');
  }
};

// Fetch price history for a specific cryptocurrency
export const fetchCryptoPriceHistory = async (
  cryptoId: number,
  timeRange: TimeRange
): Promise<CryptoPriceHistory> => {
  try {
    const response = await apiClient.get<CryptoPriceHistory>(
      `/cryptocurrencies/${cryptoId}/history/${timeRange}`
    );
    return response.data;
  } catch (error) {
    console.error('Error fetching price history:', error);
    throw new Error('Failed to fetch price history');
  }
};

// Fetch single cryptocurrency details
export const fetchCryptoDetails = async (
  cryptoId: number
): Promise<Cryptocurrency> => {
  try {
    const response = await apiClient.get<Cryptocurrency>(
      `/cryptocurrencies/${cryptoId}`
    );
    return response.data;
  } catch (error) {
    console.error('Error fetching cryptocurrency details:', error);
    throw new Error('Failed to fetch cryptocurrency details');
  }
};