using BitqueryService.Models;
using BitqueryService.Models.RaydiumMigrated;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BitqueryService.Services
{
    public class BitqueryServiceWrapper
    {
        private readonly string _bitqueryApiEndpoint;
        private readonly string _bitqueryApiKey;
        private readonly string _bearerToken;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="configuration"></param>
        public BitqueryServiceWrapper(IConfiguration configuration)
        {
            _bitqueryApiEndpoint = configuration["Bitquery:ApiEndpoint"].ToString();
            _bitqueryApiKey = configuration["Bitquery:ApiKey"].ToString();
            _bearerToken = configuration["Bitquery:BearerToken"].ToString();
        }

        /// <summary>
        /// Method to get coins with rising 10K market cap
        /// </summary>
        /// <returns></returns>
        public async Task<List<_10KToken>> Get10KMarketCapTokensAsync()
        {
            using var httpClient = new HttpClient();

            // Set the Bitquery API key header
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", _bitqueryApiEndpoint);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);


            string query = @"
            {
              Solana {
                DEXTrades(
                  limitBy: { count: 1, by: Trade_Buy_Currency_MintAddress }
                  limit: { count: 3 }
                  orderBy: { descending: Block_Time }
                  where: {
                    Trade: {
                      Buy: {
                        PriceInUSD: { gt: 0.00001 },
                        Currency: { MintAddress: { notIn: [""11111111111111111111111111111111""] } }
                      },
                      Sell: { AmountInUSD: { gt: ""16"", lt: ""29"" } },
                      Dex: { ProtocolName: { is: ""pump"" } }
                    },
                    Transaction: { Result: { Success: true } }
                  }
                ) {
                  Trade {
                    Buy {
                      Currency {
                        Name
                        Symbol
                        MintAddress
                        Decimals
                        Fungible
                        Uri
                      }
                      Price
                      PriceInUSD
                    }
                    Sell {
                      Amount
                      AmountInUSD
                      Currency {
                        Name
                        Symbol
                        MintAddress
                        Decimals
                        Fungible
                        Uri
                      }
                    }
                  }
                }
              }
            }";


            var gqlQuery = new GraphQLQuery
            {
                Query = query,
                Variables = new { } // empty object, as in the docs example 
            };

            string jsonQuery = JsonSerializer.Serialize(gqlQuery);
            using var content = new StringContent(jsonQuery, Encoding.UTF8, "application/json");

            // Send the POST request to Bitquery
            HttpResponseMessage response = await httpClient.PostAsync(_bitqueryApiEndpoint, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // Deserialize into our new model
            var gqlResponse = JsonSerializer.Deserialize<GraphQLResponse<SolanaRoot>>(jsonResponse, options);

            // Access the DEXTrades collection from the Solana property
            var dexTrades = gqlResponse?.Data?.Solana?.DEXTrades;
            if (dexTrades == null)
                return new List<_10KToken>();

            var newTokens = dexTrades
            .Select(dt => new
            {
                // Group by the unique MintAddress of the coin being bought.
                TokenAddress = dt.Trade.Buy.Currency.MintAddress,
                Symbol = dt.Trade.Buy.Currency.Symbol,
                MigrationTime = DateTime.Now
            })
            .GroupBy(t => t.TokenAddress)
            .Select(g => new _10KToken
            {
                TokenAddress = g.Key,
                Symbol = g.First().Symbol,
                MigrationTime = g.Max(x => x.MigrationTime),
                Uri = g.Key
            })
            .ToList();

            return newTokens;
        }
        
        /// <summary>
        /// Method to get coins that are migrated / migrating to Raydium
        /// </summary>
        /// <returns></returns>
        public async Task<List<Token>> GetNewRaydiumMigratedTokensAsync()
        {
            using var httpClient = new HttpClient();

            // Set the Bitquery API key header
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", _bitqueryApiKey);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            string query = @"
            {
              Solana {
                DEXTrades(
                  limitBy: { count: 1, by: Trade_Buy_Currency_MintAddress }
                  limit: { count: 3 }
                  orderBy: { descending: Block_Time }
                  where: {
                    Trade: {
                      Buy: {
                        PriceInUSD: { gt: 0.00001 },
                        Currency: { MintAddress: { notIn: [""11111111111111111111111111111111""] } }
                      },
                      Sell: { AmountInUSD: { gt: ""16"", lt: ""29"" } },
                      Dex: { ProtocolName: { is: ""pump"" } }
                    },
                    Transaction: { Result: { Success: true } }
                  }
                ) {
                  Trade {
                    Buy {
                      Currency {
                        Name
                        Symbol
                        MintAddress
                        Decimals
                        Fungible
                        Uri
                      }
                      Price
                      PriceInUSD
                    }
                    Sell {
                      Amount
                      AmountInUSD
                      Currency {
                        Name
                        Symbol
                        MintAddress
                        Decimals
                        Fungible
                        Uri
                      }
                    }
                  }
                }
              }
            }";


            var gqlQuery = new GraphQLQuery
            {
                Query = query,
                Variables = new { } // empty object, as in the docs example 
            };

            string jsonQuery = JsonSerializer.Serialize(gqlQuery);
            using var content = new StringContent(jsonQuery, Encoding.UTF8, "application/json");

            // Send the POST request to Bitquery
            HttpResponseMessage response = await httpClient.PostAsync(_bitqueryApiEndpoint, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            // Deserialize the JSON into our root response.
            RaydiumMigratedRootResponse root = JsonSerializer.Deserialize<RaydiumMigratedRootResponse>(jsonResponse);

            List<Token> tokens = new List<Token>();

            // Navigate to the list of DEXTrades.
            if (root?.Data?.Solana?.DEXTrades != null)
            {
                foreach (var dexTrade in root.Data.Solana.DEXTrades)
                {
                    // Map the Buy side's Currency data into your Token object.
                    var currency = dexTrade.Trade?.Buy?.Currency;
                    if (currency != null)
                    {
                        Token newToken = new Token
                        {
                            Address = currency.MintAddress,
                            Name = currency.Name,
                            Symbol = currency.Symbol,
                            LogoURI = dexTrade.Trade?.Buy.Uri
                        };
                        tokens.Add(newToken);
                    }
                }
            }

            return tokens;
        }
    }
}
