using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repos.Model
{
    public class OperationLog : BaseEntity
    {
        /// <summary>
        ///     烧入时间
        /// </summary>
        [Required]
        public DateTime OperationTime { get; set; }

        /// <summary>
        ///     上次烧入状态
        /// </summary>
        public bool? FlashInState { get; set; } = null!;

        /// <summary>
        ///     导航属性
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        ///     外键属性
        /// </summary>
        [ForeignKey("DeviceId")]
        public Device Device { get; set; } = null!;
    }
}