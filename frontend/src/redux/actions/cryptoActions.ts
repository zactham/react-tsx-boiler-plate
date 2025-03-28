// src/redux/actions/cryptoActions.ts

import { createAction } from '@reduxjs/toolkit';
import { Cryptocurrency, CryptoPriceHistory, TimeRange } from '../../types/crypto';

// Fetch cryptocurrencies actions
export const fetchCryptocurrencies = createAction('crypto/fetchCryptocurrencies');
export const fetchCryptocurrenciesSuccess = createAction<Cryptocurrency[]>('crypto/fetchCryptocurrenciesSuccess');
export const fetchCryptocurrenciesFailure = createAction<string>('crypto/fetchCryptocurrenciesFailure');

// Select cryptocurrency action
export const selectCryptocurrency = createAction<number>('crypto/selectCryptocurrency');

// Fetch price history actions
export const fetchPriceHistory = createAction<{ cryptoId: number, timeRange: TimeRange }>('crypto/fetchPriceHistory');
export const fetchPriceHistorySuccess = createAction<CryptoPriceHistory>('crypto/fetchPriceHistorySuccess');
export const fetchPriceHistoryFailure = createAction<string>('crypto/fetchPriceHistoryFailure');

// Change time range action
export const changeTimeRange = createAction<TimeRange>('crypto/changeTimeRange');

// Clear selected crypto action
export const clearSelectedCrypto = createAction('crypto/clearSelectedCrypto');