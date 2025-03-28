import { combineReducers } from 'redux';
import cryptoReducer from './cryptoReducer';

export const rootReducer = combineReducers({
  crypto: cryptoReducer,
  // Add more reducers here as needed
});