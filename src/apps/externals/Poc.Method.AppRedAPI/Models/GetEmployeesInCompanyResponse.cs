namespace Poc.Method.AppRedAPI.Models
{
    public class GetEmployeesInCompanyResponse
    {
        public ICollection<EmployeModel> Employees { get; set; } = new List<EmployeModel>();

        public int CompanyId { get; set; }

        public bool IsSuccess { get; set; }
    }
}
