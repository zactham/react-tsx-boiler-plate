using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CryptoTracker.API.New.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptocurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "numeric(18,8)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "numeric(24,2)", nullable: false),
                    Volume24h = table.Column<decimal>(type: "numeric(24,2)", nullable: false),
                    CirculatingSupply = table.Column<decimal>(type: "numeric(24,2)", nullable: false),
                    PriceChangePercentage24h = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceDataPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CryptocurrencyId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,8)", nullable: false),
                    Volume = table.Column<decimal>(type: "numeric(24,2)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "numeric(24,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDataPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceDataPoints_Cryptocurrencies_CryptocurrencyId",
                        column: x => x.CryptocurrencyId,
                        principalTable: "Cryptocurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cryptocurrencies",
                columns: new[] { "Id", "CirculatingSupply", "CurrentPrice", "ImageUrl", "MarketCap", "Name", "PriceChangePercentage24h", "Symbol", "Volume24h" },
                values: new object[,]
                {
                    { 1, 19401525m, 53000.00m, "https://assets.coingecko.com/coins/images/1/large/bitcoin.png", 1028502937621m, "Bitcoin", 2.34m, "btc", 31578397452m },
                    { 2, 120105764m, 2950.15m, "https://assets.coingecko.com/coins/images/279/large/ethereum.png", 354021576329m, "Ethereum", 3.56m, "eth", 14935903245m },
                    { 3, 97881458867m, 1.00m, "https://assets.coingecko.com/coins/images/325/large/Tether.png", 97944489305m, "Tether", 0.01m, "usdt", 83140364432m },
                    { 4, 157204722m, 605.73m, "https://assets.coingecko.com/coins/images/825/large/bnb-icon2_2x.png", 95276514971m, "BNB", 0.92m, "bnb", 1904643035m },
                    { 5, 498073742m, 151.12m, "https://assets.coingecko.com/coins/images/4128/large/solana.png", 75234781241m, "Solana", 4.27m, "sol", 2718456192m }
                });

            migrationBuilder.InsertData(
                table: "PriceDataPoints",
                columns: new[] { "Id", "CryptocurrencyId", "MarketCap", "Price", "Timestamp", "Volume" },
                values: new object[,]
                {
                    { 1, 1, 802630168607m, 42243.69m, new DateTime(2025, 3, 14, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30053015099.8630631m },
                    { 2, 1, 814090712263m, 42846.88m, new DateTime(2025, 3, 13, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32513854568.13213m },
                    { 3, 1, 805767842494m, 42408.83m, new DateTime(2025, 3, 12, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 31681464637.48136m },
                    { 4, 1, 824281422162m, 43383.23m, new DateTime(2025, 3, 11, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32063653542.68982m },
                    { 5, 1, 819994520167m, 43157.61m, new DateTime(2025, 3, 10, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30177993428.044949m },
                    { 6, 1, 834869962461m, 43940.52m, new DateTime(2025, 3, 9, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32592583629.57862m },
                    { 7, 1, 843745887432m, 44407.68m, new DateTime(2025, 3, 8, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 33089713297.36044m },
                    { 8, 1, 854427909777m, 44969.89m, new DateTime(2025, 3, 7, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30460705485.409454m },
                    { 9, 1, 849059460304m, 44687.34m, new DateTime(2025, 3, 6, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 33126571666.04259m },
                    { 10, 1, 868558200660m, 45713.59m, new DateTime(2025, 3, 5, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30105358056.773133m },
                    { 11, 1, 884068336590m, 46529.91m, new DateTime(2025, 3, 4, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 31538294766.81459m },
                    { 12, 1, 873224679367m, 45959.19m, new DateTime(2025, 3, 3, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 33220737866.69445m },
                    { 13, 1, 871667792616m, 45877.25m, new DateTime(2025, 3, 2, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30015819443.3971399m },
                    { 14, 1, 857895770814m, 45152.41m, new DateTime(2025, 3, 1, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32332641146.30066m },
                    { 15, 1, 866202669011m, 45589.61m, new DateTime(2025, 2, 28, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30166039865.54129m },
                    { 16, 1, 880002049067m, 46315.90m, new DateTime(2025, 2, 27, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32065654856.18341m },
                    { 17, 1, 871608903210m, 45874.15m, new DateTime(2025, 2, 26, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 33624851038.50479m },
                    { 18, 1, 887234826805m, 46696.57m, new DateTime(2025, 2, 25, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32844618359.04262m },
                    { 19, 1, 907928926792m, 47785.73m, new DateTime(2025, 2, 24, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32159882328.54748m },
                    { 20, 1, 893798774827m, 47042.04m, new DateTime(2025, 2, 23, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32820608520.33114m },
                    { 21, 1, 912499884618m, 48026.31m, new DateTime(2025, 2, 22, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30608772320.956351m },
                    { 22, 1, 917201896568m, 48273.78m, new DateTime(2025, 2, 21, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32308787415.87921m },
                    { 23, 1, 903018306512m, 47527.28m, new DateTime(2025, 2, 20, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30141275269.97648m },
                    { 24, 1, 902231060297m, 47485.85m, new DateTime(2025, 2, 19, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 31040978540.21982m },
                    { 25, 1, 907632854772m, 47770.15m, new DateTime(2025, 2, 18, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 31280932749.28673m },
                    { 26, 1, 907378627939m, 47756.77m, new DateTime(2025, 2, 17, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 31029285591.57498m },
                    { 27, 1, 919582011913m, 48399.05m, new DateTime(2025, 2, 16, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 33045002234.65497m },
                    { 28, 1, 934836146013m, 49201.90m, new DateTime(2025, 2, 15, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32051691166.14931m },
                    { 29, 1, 924463281352m, 48655.96m, new DateTime(2025, 2, 14, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 31050370701.14648m },
                    { 30, 1, 940047662987m, 49476.19m, new DateTime(2025, 2, 13, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 32091057104.10097m },
                    { 31, 1, 957985057131m, 50420.27m, new DateTime(2025, 2, 12, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 30563629193.493924m },
                    { 32, 2, 302061273109m, 2517.18m, new DateTime(2025, 3, 14, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15439494494.55342m },
                    { 33, 2, 310881422458m, 2590.68m, new DateTime(2025, 3, 13, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16858125684.7168m },
                    { 34, 2, 313792015707m, 2614.93m, new DateTime(2025, 3, 12, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15048434040.5317182m },
                    { 35, 2, 310977686363m, 2591.48m, new DateTime(2025, 3, 11, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16979657166.62801m },
                    { 36, 2, 310009340936m, 2583.41m, new DateTime(2025, 3, 10, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15578949541.123095m },
                    { 37, 2, 306062068642m, 2550.52m, new DateTime(2025, 3, 9, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16344086586.19229m },
                    { 38, 2, 301250085851m, 2510.42m, new DateTime(2025, 3, 8, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16974043183.94793m },
                    { 39, 2, 301018192685m, 2508.48m, new DateTime(2025, 3, 7, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16183324923.35854m },
                    { 40, 2, 306242809610m, 2552.02m, new DateTime(2025, 3, 6, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15269387151.240086m },
                    { 41, 2, 312839676562m, 2607.00m, new DateTime(2025, 3, 5, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16552220879.84542m },
                    { 42, 2, 316001291453m, 2633.34m, new DateTime(2025, 3, 4, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15904303002.592317m },
                    { 43, 2, 323709271857m, 2697.58m, new DateTime(2025, 3, 3, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15324725612.217898m },
                    { 44, 2, 319668029115m, 2663.90m, new DateTime(2025, 3, 2, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15665668529.76832m },
                    { 45, 2, 311124330763m, 2592.70m, new DateTime(2025, 3, 1, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16763361321.65201m },
                    { 46, 2, 320362057184m, 2669.68m, new DateTime(2025, 2, 28, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16470210304.23707m },
                    { 47, 2, 318703417589m, 2655.86m, new DateTime(2025, 2, 27, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16814516757.52854m },
                    { 48, 2, 322625788336m, 2688.55m, new DateTime(2025, 2, 26, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15801397985.220606m },
                    { 49, 2, 322000484033m, 2683.34m, new DateTime(2025, 2, 25, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16584572554.37252m },
                    { 50, 2, 323223224256m, 2693.53m, new DateTime(2025, 2, 24, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15283760198.52411m },
                    { 51, 2, 325709680966m, 2714.25m, new DateTime(2025, 2, 23, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15088887631.0032269m },
                    { 52, 2, 323446729638m, 2695.39m, new DateTime(2025, 2, 22, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15750665883.883213m },
                    { 53, 2, 324201592296m, 2701.68m, new DateTime(2025, 2, 21, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16857941533.37271m },
                    { 54, 2, 316352614442m, 2636.27m, new DateTime(2025, 2, 20, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15069078066.4184494m },
                    { 55, 2, 313453611366m, 2612.11m, new DateTime(2025, 2, 19, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15127546192.206231m },
                    { 56, 2, 323086619696m, 2692.39m, new DateTime(2025, 2, 18, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15025793644.6116276m },
                    { 57, 2, 316835370512m, 2640.29m, new DateTime(2025, 2, 17, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15017905818.3067971m },
                    { 58, 2, 316843035641m, 2640.36m, new DateTime(2025, 2, 16, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16026992477.95483m },
                    { 59, 2, 318003503891m, 2650.03m, new DateTime(2025, 2, 15, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16595618653.85418m },
                    { 60, 2, 327594821312m, 2729.96m, new DateTime(2025, 2, 14, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15928187156.528322m },
                    { 61, 2, 336251075143m, 2802.09m, new DateTime(2025, 2, 13, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 16436349488.53233m },
                    { 62, 2, 345184572647m, 2876.54m, new DateTime(2025, 2, 12, 17, 42, 4, 257, DateTimeKind.Utc).AddTicks(2120), 15534876821.811719m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceDataPoints_CryptocurrencyId_Timestamp",
                table: "PriceDataPoints",
                columns: new[] { "CryptocurrencyId", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceDataPoints");

            migrationBuilder.DropTable(
                name: "Cryptocurrencies");
        }
    }
}
