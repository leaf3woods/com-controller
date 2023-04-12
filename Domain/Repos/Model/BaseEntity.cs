using System.ComponentModel.DataAnnotations;

namespace Domain.Repos.Model
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}