import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { 
  CryptoState, 
  Cryptocurrency, 
  CryptoPriceHistory, 
  TimeRange 
} from '../../types/crypto';

const initialState: CryptoState = {
  cryptoList: [],
  selectedCrypto: null,
  priceHistory: null,
  loading: false,
  error: null,
  timeRange: TimeRange.WEEK,
};

const cryptoSlice = createSlice({
  name: 'crypto',
  initialState,
  reducers: {
    // Fetch cryptocurrencies
    fetchCryptocurrencies: (state) => {
      state.loading = true;
      state.error = null;
    },
    fetchCryptocurrenciesSuccess: (
      state,
      action: PayloadAction<Cryptocurrency[]>
    ) => {
      state.loading = false;
      state.cryptoList = action.payload;
    },
    fetchCryptocurrenciesFailure: (state, action: PayloadAction<string>) => {
      state.loading = false;
      state.error = action.payload;
    },

    // Select cryptocurrency
    selectCryptocurrency: (state, action: PayloadAction<number>) => {
      state.loading = true;
      state.error = null;
      const selected = state.cryptoList.find(
        (crypto) => crypto.id === action.payload
      );
      if (selected) {
        state.selectedCrypto = selected;
      }
    },

    // Fetch price history
    fetchPriceHistory: (
      state,
      action: PayloadAction<{ cryptoId: number; timeRange: TimeRange }>
    ) => {
      state.loading = true;
      state.error = null;
      state.timeRange = action.payload.timeRange;
    },
    fetchPriceHistorySuccess: (
      state,
      action: PayloadAction<CryptoPriceHistory>
    ) => {
      state.loading = false;
      state.priceHistory = action.payload;
    },
    fetchPriceHistoryFailure: (state, action: PayloadAction<string>) => {
      state.loading = false;
      state.error = action.payload;
    },

    // Change time range
    changeTimeRange: (state, action: PayloadAction<TimeRange>) => {
      state.timeRange = action.payload;
    },

    // Clear selected cryptocurrency
    clearSelectedCrypto: (state) => {
      state.selectedCrypto = null;
      state.priceHistory = null;
    },
  },
});

export const {
  fetchCryptocurrencies,
  fetchCryptocurrenciesSuccess,
  fetchCryptocurrenciesFailure,
  selectCryptocurrency,
  fetchPriceHistory,
  fetchPriceHistorySuccess,
  fetchPriceHistoryFailure,
  changeTimeRange,
  clearSelectedCrypto,
} = cryptoSlice.actions;

export default cryptoSlice.reducer;