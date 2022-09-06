namespace Poc.Method.ExternalAppYellowAccess.Models
{
    public record CompanyAssignEmployeeRequest
    {
        public int CompanyId { get; set; }

        public int PersonId { get; set; }
    }
}
