using System.ComponentModel.DataAnnotations;

namespace Domain.Repos.Model
{
    public class BaseEntity
    {
        /// <summary>
        ///     实体唯一Id
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}