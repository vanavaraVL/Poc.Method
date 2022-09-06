namespace Poc.Method.PersonManager.Models
{
    public record CreatePersonRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AdditionInfo { get; set; }

        public int CompanyId { get; set; }
    }
}
