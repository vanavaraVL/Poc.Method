namespace Poc.Method.Core.Dtos.Companies
{
    public record CompanyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AdditionInfo { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
