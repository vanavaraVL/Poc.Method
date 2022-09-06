namespace Poc.Method.ExternalAppRedAccess.Models
{
    public record CompanyEmployeesRequest
    {
        public int CompanyId { get; set; }

        public int Page { get; set; }
    }
}
