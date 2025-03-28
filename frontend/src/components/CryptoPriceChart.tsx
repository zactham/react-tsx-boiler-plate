// src/components/CryptoPriceChart.tsx

import React from 'react';
import { useTheme } from '@mui/material/styles';
import { Box, useMediaQuery } from '@mui/material';
import { 
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  TimeScale,
  ChartOptions
} from 'chart.js';
import 'chartjs-adapter-date-fns';
import { Line } from 'react-chartjs-2';
import { CryptoPriceHistory } from '../types/crypto';

// Register ChartJS components
ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  TimeScale
);

interface CryptoPriceChartProps {
  priceHistory: CryptoPriceHistory;
}

const CryptoPriceChart: React.FC<CryptoPriceChartProps> = ({ priceHistory }) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  
  // Calculate price change percentage
  const firstPrice = priceHistory.priceHistory[0]?.price || 0;
  const lastPrice = priceHistory.priceHistory[priceHistory.priceHistory.length - 1]?.price || 0;
  const priceChange = lastPrice - firstPrice;
  const percentChange = firstPrice !== 0 ? (priceChange / firstPrice) * 100 : 0;
  
  // Set chart colors based on price change
  const isPriceUp = percentChange >= 0;
  const mainColor = isPriceUp 
    ? theme.palette.success.main 
    : theme.palette.error.main;
  const bgColor = isPriceUp 
    ? theme.palette.success.light 
    : theme.palette.error.light;
  
  const data = {
    datasets: [
      {
        label: `${priceHistory.name} Price`,
        data: priceHistory.priceHistory.map((point) => ({
          x: new Date(point.timestamp),
          y: point.price,
        })),
        borderColor: mainColor,
        backgroundColor: bgColor,
        borderWidth: 2,
        fill: {
          target: 'origin',
          above: bgColor + '33', // Add transparency
        },
        tension: 0.1,
        pointRadius: isMobile ? 0 : 2,
        pointHoverRadius: 5,
      },
    ],
  };

  const options: ChartOptions<'line'> = {
    responsive: true,
    maintainAspectRatio: false,
    interaction: {
      mode: 'index',
      intersect: false,
    },
    plugins: {
      legend: {
        display: false,
      },
      tooltip: {
        callbacks: {
          label: (context) => {
            return `Price: $${context.parsed.y.toFixed(2)}`;
          },
          title: (tooltipItems) => {
            const date = new Date(tooltipItems[0].parsed.x);
            return date.toLocaleString();
          },
        },
      },
    },
    scales: {
      x: {
        type: 'time',
        time: {
          unit: getTimeUnit(priceHistory.timeRange),
        },
        grid: {
          display: false,
        },
      },
      y: {
        position: 'right',
        grid: {
          color: theme.palette.divider,
        },
        ticks: {
          callback: (value) => {
            return '$' + formatYAxisTick(Number(value));
          },
        },
      },
    },
  };

  return (
    <Box sx={{ height: 400, width: '100%' }}>
      <Line data={data} options={options} />
    </Box>
  );
};

// Helper functions
function getTimeUnit(timeRange: string): 'hour' | 'day' | 'week' | 'month' {
  switch (timeRange) {
    case '1d':
      return 'hour';
    case '7d':
      return 'day';
    case '30d':
      return 'day';
    case '1y':
      return 'month';
    case 'all':
      return 'month';
    default:
      return 'day';
  }
}

function formatYAxisTick(value: number): string {
  if (value >= 1000) {
    return value.toLocaleString();
  }
  
  if (value < 1) {
    return value.toFixed(4);
  }
  
  return value.toFixed(2);
}

export default CryptoPriceChart;