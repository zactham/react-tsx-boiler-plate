// src/components/TimeRangeSelector.tsx

import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { 
  ToggleButtonGroup, 
  ToggleButton,
  styled
} from '@mui/material';
import { RootState } from '../redux/store';
import { fetchPriceHistory } from '../redux/actions/cryptoActions';
import { TimeRange } from '../types/crypto';

const StyledToggleButtonGroup = styled(ToggleButtonGroup)(({ theme }) => ({
  '& .MuiToggleButtonGroup-grouped': {
    margin: theme.spacing(0.5),
    border: 0,
    borderRadius: theme.shape.borderRadius,
    '&.Mui-selected': {
      backgroundColor: theme.palette.primary.main,
      color: theme.palette.primary.contrastText,
      '&:hover': {
        backgroundColor: theme.palette.primary.dark,
      },
    },
  },
}));

const TimeRangeSelector: React.FC = () => {
  const dispatch = useDispatch();
  const { selectedCrypto, timeRange } = useSelector(
    (state: RootState) => state.crypto
  );

  const handleChange = (
    _: React.MouseEvent<HTMLElement>,
    newTimeRange: TimeRange
  ) => {
    if (newTimeRange !== null && selectedCrypto) {
      dispatch(
        fetchPriceHistory({
          cryptoId: selectedCrypto.id,
          timeRange: newTimeRange,
        })
      );
    }
  };

  return (
    <StyledToggleButtonGroup
      value={timeRange}
      exclusive
      onChange={handleChange}
      aria-label="time range"
      size="small"
    >
      <ToggleButton value={TimeRange.DAY} aria-label="1 day">
        1D
      </ToggleButton>
      <ToggleButton value={TimeRange.WEEK} aria-label="1 week">
        1W
      </ToggleButton>
      <ToggleButton value={TimeRange.MONTH} aria-label="1 month">
        1M
      </ToggleButton>
      <ToggleButton value={TimeRange.YEAR} aria-label="1 year">
        1Y
      </ToggleButton>
      <ToggleButton value={TimeRange.ALL} aria-label="all time">
        ALL
      </ToggleButton>
    </StyledToggleButtonGroup>
  );
};

export default TimeRangeSelector;