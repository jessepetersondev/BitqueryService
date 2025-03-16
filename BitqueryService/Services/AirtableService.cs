using AirtableApiClient;
using BitqueryService.Models;
using Microsoft.Extensions.Configuration;

namespace BitqueryService.Services
{
    public class AirtableService
    {
        private readonly string _airtableApiKey;
        private readonly string _airtableBaseId;
        private readonly string _airtableTableName;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="configuration"></param>
        public AirtableService(IConfiguration configuration)
        {
            _airtableApiKey = configuration["Airtable:ApiKey"].ToString();
            _airtableBaseId = configuration["Airtable:BaseId"].ToString();
            _airtableTableName = configuration["Airtable:TableName"].ToString();
        }

        /// <summary>
        /// Method to insert data into airtable
        /// </summary>
        /// <param name="tokenData"></param>
        /// <returns></returns>
        public async Task InsertTokenDataAsync(Token tokenData)
        {
            try
            {
                var airtableBase = new AirtableBase(_airtableApiKey, _airtableBaseId);

                // Check if record already exists
                string filterFormula = $"{{Mint}} = '{tokenData.Address}'";
                var existingRecords = await airtableBase.ListRecords(
                    _airtableTableName,
                    filterByFormula: filterFormula
                );

                var fields = new Dictionary<string, object>
                {
                    { "Name", tokenData.Name },
                    { "Symbol", tokenData.Symbol },
                    { "Status", new string[] { "New" } },
                    { "Mint", tokenData.Address },
                    { "CreateDate", DateTime.UtcNow },
                    { "Uri", tokenData.LogoURI },
                    { "SourceApplication", "BirdeyeTrending" }
                };
                Fields fieldsToInsert = new Fields { FieldsCollection = fields };

                if (existingRecords?.Records != null && existingRecords.Records.Any())
                {
                    Console.WriteLine($"Skipped inserting new token, already exists: {tokenData.Symbol}");
                }
                else
                {
                    // New record, never seen before
                    fields["Status"] = new string[] { "New" };
                    await airtableBase.CreateRecord(_airtableTableName, fieldsToInsert);
                    Console.WriteLine($"Successfully inserted new token: {tokenData.Symbol}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {tokenData.Symbol}: {ex.Message}");
            }
        }

    }
}
