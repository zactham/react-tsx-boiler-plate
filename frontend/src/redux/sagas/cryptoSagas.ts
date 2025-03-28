import { call, put, takeLatest, select } from 'redux-saga/effects';
import { PayloadAction } from '@reduxjs/toolkit';
import { 
  fetchCryptocurrencies, 
  fetchCryptocurrenciesSuccess, 
  fetchCryptocurrenciesFailure,
  fetchPriceHistory,
  fetchPriceHistorySuccess,
  fetchPriceHistoryFailure,
  selectCryptocurrency
} from '../reducers/cryptoReducer';
import { Cryptocurrency, CryptoPriceHistory, TimeRange } from '../../types/crypto';
import { fetchCryptoList, fetchCryptoPriceHistory } from '../../services/cryptoService';
import { RootState } from '../store';

// Worker Sagas
function* fetchCryptocurrenciesSaga() {
  try {
    const cryptocurrencies: Cryptocurrency[] = yield call(fetchCryptoList);
    yield put(fetchCryptocurrenciesSuccess(cryptocurrencies));
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : 'An unknown error occurred';
    yield put(fetchCryptocurrenciesFailure(errorMessage));
  }
}

function* fetchPriceHistorySaga(
  action: PayloadAction<{ cryptoId: number; timeRange: TimeRange }>
) {
  try {
    const { cryptoId, timeRange } = action.payload;
    const priceHistory: CryptoPriceHistory = yield call(
      fetchCryptoPriceHistory,
      cryptoId,
      timeRange
    );
    yield put(fetchPriceHistorySuccess(priceHistory));
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : 'An unknown error occurred';
    yield put(fetchPriceHistoryFailure(errorMessage));
  }
}

function* selectCryptocurrencySaga(action: PayloadAction<number>) {
  try {
    const state: RootState = yield select();
    const timeRange = state.crypto.timeRange;
    
    yield put(fetchPriceHistory({ 
      cryptoId: action.payload, 
      timeRange 
    }));
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : 'An unknown error occurred';
    yield put(fetchPriceHistoryFailure(errorMessage));
  }
}

// Watcher Saga
export function* watchCryptoSagas() {
  yield takeLatest(fetchCryptocurrencies.type, fetchCryptocurrenciesSaga);
  yield takeLatest(fetchPriceHistory.type, fetchPriceHistorySaga);
  yield takeLatest(selectCryptocurrency.type, selectCryptocurrencySaga);
}