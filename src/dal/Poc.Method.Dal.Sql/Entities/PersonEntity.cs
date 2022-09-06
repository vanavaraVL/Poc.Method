using System.ComponentModel.DataAnnotations;

namespace Poc.Method.Dal.Sql.Entities
{
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string AdditionInfo { get; set; }

        public int? EmployerId { get; set; }

        public virtual CompanyEntity Employer { get; set; }
    }
}
