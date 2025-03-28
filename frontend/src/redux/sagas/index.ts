import { all, fork } from 'redux-saga/effects';
import { watchCryptoSagas } from './cryptoSagas';

export default function* rootSaga() {
  yield all([
    fork(watchCryptoSagas),
    // Add more sagas here as needed
  ]);
}