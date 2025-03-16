using BitqueryService.Services;
using BitqueryService.Models;
using Microsoft.Extensions.Configuration;

namespace BitqueryService
{
    public class Program
    {
        /// <summary>
        /// The main entry method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main()
        {
            try
            {
                // Build configuration from appsettings.json
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Create an instance of AirtableService using the configuration
                var airtableService = new AirtableService(configuration);
                var bitqueryService = new BitqueryServiceWrapper(configuration);
                var tokens10KToAdd = bitqueryService.Get10KMarketCapTokensAsync().Result;

                // Process each trending token from Birdeye
                foreach (var token in tokens10KToAdd)
                {
                    Token newToken = new Token 
                    {
                        Address = token.TokenAddress,
                        Name = token.Symbol,
                        Symbol = token.Symbol
                    };
                    await airtableService.InsertTokenDataAsync(newToken);
                }

                var tokensRaydiumMigrated = bitqueryService.GetNewRaydiumMigratedTokensAsync().Result;
                foreach (var token in tokensRaydiumMigrated)
                {
                    Token newToken = new Token
                    {
                        Address = token.Address,
                        Name = token.Name,
                        Symbol = token.Symbol,
                        LogoURI = token.LogoURI
                    };
                    await airtableService.InsertTokenDataAsync(newToken);
                }

                Console.WriteLine("Data successfully processed and saved to Airtable!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}