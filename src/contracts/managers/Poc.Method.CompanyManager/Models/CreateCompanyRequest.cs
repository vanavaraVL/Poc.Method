namespace Poc.Method.CompanyManager.Models
{
    public record CreateCompanyRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string AdditionInfo { get; set; }
    }
}
