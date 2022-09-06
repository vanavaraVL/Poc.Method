namespace Poc.Method.Core.Dtos.Persons
{
    public record PersonDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string AdditionInfo { get; set; }

        public int? CompanyId { get; set; }
    }
}
