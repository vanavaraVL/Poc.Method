namespace Poc.Method.ContextStorageAccess.Models
{
    public record PersonCreateRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AdditionInfo { get; set; }
    }
}
