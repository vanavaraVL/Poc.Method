using System.ComponentModel.DataAnnotations;

namespace Poc.Method.Dal.Sql.Entities
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
            Employees = new List<PersonEntity>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AdditionInfo { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<PersonEntity> Employees { get; set; }
    }
}
