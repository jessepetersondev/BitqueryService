namespace BitqueryService.Models
{
    public class Sell
    {
        public string Amount { get; set; }
        public string AmountInUSD { get; set; }
        public Currency Currency { get; set; }
    }
}
