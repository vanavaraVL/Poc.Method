namespace Poc.Method.ExternalAppYellowAccess.Models
{
    public record CompanyCreateRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string AdditionInfo { get; set; }
    }
}
