// src/pages/Dashboard.tsx

import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { 
  Container, 
  Grid, 
  Typography, 
  Paper, 
  Box,
  CircularProgress,
  Alert
} from '@mui/material';
import { RootState } from '../redux/store';
import { fetchCryptocurrencies } from '../redux/actions/cryptoActions';
import CryptoTable from '../components/CryptoTable';
import CryptoPriceChart from '../components/CryptoPriceChart';
import TimeRangeSelector from '../components/TimeRangeSelector';

const Dashboard: React.FC = () => {
  const dispatch = useDispatch();
  const { 
    cryptoList, 
    selectedCrypto, 
    priceHistory, 
    loading, 
    error 
  } = useSelector((state: RootState) => state.crypto);

  useEffect(() => {
    dispatch(fetchCryptocurrencies());
  }, [dispatch]);

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Cryptocurrency Dashboard
      </Typography>
      
      {error && <Alert severity="error">{error}</Alert>}
      
      <Grid container spacing={3}>
        {/* Cryptocurrency Table */}
        <Grid item xs={12}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            <Typography variant="h6" component="h2" gutterBottom>
              Top Cryptocurrencies
            </Typography>
            {loading && !cryptoList.length ? (
              <Box display="flex" justifyContent="center" p={3}>
                <CircularProgress />
              </Box>
            ) : (
              <CryptoTable cryptoList={cryptoList} />
            )}
          </Paper>
        </Grid>
        
        {/* Price Chart */}
        {selectedCrypto && (
          <>
            <Grid item xs={12}>
              <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                  <Typography variant="h6" component="h2">
                    {selectedCrypto.name} ({selectedCrypto.symbol.toUpperCase()}) Price Chart
                  </Typography>
                  <TimeRangeSelector />
                </Box>
                
                {loading && !priceHistory ? (
                  <Box display="flex" justifyContent="center" p={3}>
                    <CircularProgress />
                  </Box>
                ) : priceHistory ? (
                  <CryptoPriceChart priceHistory={priceHistory} />
                ) : (
                  <Typography variant="body2" color="text.secondary">
                    No price data available
                  </Typography>
                )}
              </Paper>
            </Grid>
          </>
        )}
      </Grid>
    </Container>
  );
};

export default Dashboard;