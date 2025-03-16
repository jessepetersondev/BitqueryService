namespace BitqueryService.Models
{
    public class Token
    {
        public string Address { get; set; }
        public int Decimals { get; set; }
        public double Liquidity { get; set; }
        public string LogoURI { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Volume24hUSD { get; set; }
        public int Rank { get; set; }
    }
}
