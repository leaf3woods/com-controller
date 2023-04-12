using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repos.Model
{
    [Table("device_info")]
    [Index(nameof(Uri), IsUnique = true), Index(nameof(Limsi), IsUnique = true)]
    public class Device : BaseEntity
    {
        /// <summary>
        ///     设备Uri
        /// </summary>
        [Column("uri"), Required]
        public string Uri { get; set; } = null!;

        /// <summary>
        ///     设备Limsi码
        /// </summary>
        [Column("limsi"), Required]
        public string Limsi { get; set; } = null!;

        /// <summary>
        ///     设备操作记录
        /// </summary>
        public IQueryable<OperationLog> Logs = null!;
    }
}