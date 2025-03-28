# BOILER PLATE TSX Site - Crypto Tracker

 React/TypeScript, Redux Saga, Material UI, and Chart.js frontend; C# .NET 8.0, Entity Framework Core, and PostgreSQL backend. Features interactive data visualization, responsive design, and RESTful API architecture.

## Features

- Interactive price history charts with multiple time ranges
- Sortable and filterable table
- Responsive design for desktop and mobile
- Secure database configuration using environment variables

## Tech Stack

### Frontend
- **React 18** with TypeScript
- **Redux** with Redux Saga for state management
- **Material UI** for responsive component design
- **Chart.js** for data visualization
- **Axios** for API communication

### Backend
- **C# .NET 8.0** Web API
- **Entity Framework Core** for ORM
- **PostgreSQL** database
- **Swagger** for API documentation

## Project Structure

```
crypto-tracker/
├── frontend/                # React TypeScript frontend
│   ├── public/              # Static files
│   └── src/                 # Source code
│       ├── components/      # Reusable UI components
│       ├── pages/           # Page components
│       ├── redux/           # Redux state management
│       ├── services/        # API services
│       ├── types/           # TypeScript type definitions
│       └── utils/           # Utility functions
│
└── backend/                 # C# .NET backend
    └── CryptoTracker.API.New/
        ├── Controllers/     # API controllers
        ├── Data/            # Database context and repositories
        ├── DTOs/            # Data transfer objects
        ├── Models/          # Entity models
        └── Properties/      # Application properties
```

## Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v16 or higher)
- [PostgreSQL](https://www.postgresql.org/)

### Backend Setup

1. Clone the repository
   ```
   git clone https://github.com/yourusername/crypto-tracker.git
   cd crypto-tracker
   ```

2. Configure environment variables
   - Create a `.env` file in the backend directory with:
   ```
   DB_HOST=localhost
   DB_NAME=cryptodb
   DB_USER=postgres
   DB_PASSWORD=your_password_here
   ```

3. Create the database
   ```
   createdb cryptodb
   ```

4. Run the migrations
   ```
   cd backend/CryptoTracker.API.New
   dotnet ef database update
   ```

5. Seed the database
   ```sql
   psql -U postgres -d cryptodb -f path/to/sample_crypto_data.sql
   ```

6. Start the backend
   ```
   dotnet run
   ```
   The API will be available at `http://localhost:5000`

### Frontend Setup

1. Install dependencies
   ```
   cd frontend
   npm install
   ```

2. Create a `.env` file in the frontend directory
   ```
   REACT_APP_API_BASE_URL=http://localhost:5000/api
   ```

3. Start the frontend development server
   ```
   npm start
   ```
   The application will be available at `http://localhost:3000`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/cryptocurrencies` | Get all cryptocurrencies |
| GET | `/api/cryptocurrencies/{id}` | Get cryptocurrency by ID |
| GET | `/api/cryptocurrencies/symbol/{symbol}` | Get cryptocurrency by symbol |
| GET | `/api/cryptocurrencies/{id}/history/{timeRange}` | Get price history for a cryptocurrency |

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Data structure inspired by CoinGecko API
- Icons from Material UI
- Chart visualization powered by Chart.js