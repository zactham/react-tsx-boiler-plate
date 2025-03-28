// src/components/CryptoTable.tsx

import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TableSortLabel,
  Paper,
  Avatar,
  Box,
  Typography,
  Chip,
} from '@mui/material';
import { ArrowUpward, ArrowDownward } from '@mui/icons-material';
import { Cryptocurrency } from '../types/crypto';
import { selectCryptocurrency } from '../redux/actions/cryptoActions';

interface CryptoTableProps {
  cryptoList: Cryptocurrency[];
}

type SortField = 'name' | 'currentPrice' | 'marketCap' | 'volume24h' | 'priceChangePercentage24h';

interface SortState {
  field: SortField;
  direction: 'asc' | 'desc';
}

const CryptoTable: React.FC<CryptoTableProps> = ({ cryptoList }) => {
  const dispatch = useDispatch();
  const [sort, setSort] = useState<SortState>({
    field: 'marketCap',
    direction: 'desc',
  });

  const handleSort = (field: SortField) => {
    setSort({
      field,
      direction: sort.field === field && sort.direction === 'desc' ? 'asc' : 'desc',
    });
  };

  const handleRowClick = (cryptoId: number) => {
    dispatch(selectCryptocurrency(cryptoId));
  };

  // Sort the crypto list based on current sort state
  const sortedList = [...cryptoList].sort((a, b) => {
    const factor = sort.direction === 'asc' ? 1 : -1;
    
    switch (sort.field) {
      case 'name':
        return factor * a.name.localeCompare(b.name);
      case 'currentPrice':
        return factor * (a.currentPrice - b.currentPrice);
      case 'marketCap':
        return factor * (a.marketCap - b.marketCap);
      case 'volume24h':
        return factor * (a.volume24h - b.volume24h);
      case 'priceChangePercentage24h':
        return factor * (a.priceChangePercentage24h - b.priceChangePercentage24h);
      default:
        return 0;
    }
  });

  // Format numbers with appropriate suffixes and decimal places
  const formatCurrency = (value: number): string => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: value < 1 ? 4 : 2,
      maximumFractionDigits: value < 1 ? 6 : 2,
    }).format(value);
  };

  const formatLargeNumber = (value: number): string => {
    if (value >= 1e12) return `$${(value / 1e12).toFixed(2)}T`;
    if (value >= 1e9) return `$${(value / 1e9).toFixed(2)}B`;
    if (value >= 1e6) return `$${(value / 1e6).toFixed(2)}M`;
    return formatCurrency(value);
  };

  return (
    <TableContainer component={Paper} sx={{ maxHeight: 600 }}>
      <Table stickyHeader sx={{ minWidth: 650 }} size="medium">
        <TableHead>
          <TableRow>
            <TableCell sx={{ width: '40px' }}>#</TableCell>
            <TableCell>
              <TableSortLabel
                active={sort.field === 'name'}
                direction={sort.field === 'name' ? sort.direction : 'asc'}
                onClick={() => handleSort('name')}
              >
                Name
              </TableSortLabel>
            </TableCell>
            <TableCell align="right">
              <TableSortLabel
                active={sort.field === 'currentPrice'}
                direction={sort.field === 'currentPrice' ? sort.direction : 'asc'}
                onClick={() => handleSort('currentPrice')}
              >
                Price
              </TableSortLabel>
            </TableCell>
            <TableCell align="right">
              <TableSortLabel
                active={sort.field === 'priceChangePercentage24h'}
                direction={sort.field === 'priceChangePercentage24h' ? sort.direction : 'asc'}
                onClick={() => handleSort('priceChangePercentage24h')}
              >
                24h %
              </TableSortLabel>
            </TableCell>
            <TableCell align="right">
              <TableSortLabel
                active={sort.field === 'marketCap'}
                direction={sort.field === 'marketCap' ? sort.direction : 'asc'}
                onClick={() => handleSort('marketCap')}
              >
                Market Cap
              </TableSortLabel>
            </TableCell>
            <TableCell align="right">
              <TableSortLabel
                active={sort.field === 'volume24h'}
                direction={sort.field === 'volume24h' ? sort.direction : 'asc'}
                onClick={() => handleSort('volume24h')}
              >
                Volume (24h)
              </TableSortLabel>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {sortedList.map((crypto, index) => (
            <TableRow
              key={crypto.id}
              hover
              onClick={() => handleRowClick(crypto.id)}
              sx={{ 
                cursor: 'pointer',
                '&:hover': {
                  backgroundColor: 'rgba(0, 0, 0, 0.04)'
                } 
              }}
            >
              <TableCell>{index + 1}</TableCell>
              <TableCell>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <Avatar 
                    src={crypto.imageUrl} 
                    alt={crypto.name}
                    sx={{ width: 24, height: 24, mr: 1 }}
                  />
                  <Typography variant="body1" component="span" sx={{ mr: 1 }}>
                    {crypto.name}
                  </Typography>
                  <Typography 
                    variant="body2" 
                    component="span" 
                    color="text.secondary"
                  >
                    {crypto.symbol.toUpperCase()}
                  </Typography>
                </Box>
              </TableCell>
              <TableCell align="right">
                {formatCurrency(crypto.currentPrice)}
              </TableCell>
              <TableCell align="right">
                <Chip
                  icon={crypto.priceChangePercentage24h >= 0 ? <ArrowUpward fontSize="small" /> : <ArrowDownward fontSize="small" />}
                  label={`${crypto.priceChangePercentage24h >= 0 ? '+' : ''}${crypto.priceChangePercentage24h.toFixed(2)}%`}
                  color={crypto.priceChangePercentage24h >= 0 ? 'success' : 'error'}
                  variant="outlined"
                  size="small"
                />
              </TableCell>
              <TableCell align="right">
                {formatLargeNumber(crypto.marketCap)}
              </TableCell>
              <TableCell align="right">
                {formatLargeNumber(crypto.volume24h)}
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default CryptoTable;