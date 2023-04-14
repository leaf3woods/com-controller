using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Repos.Model
{
    [Index(nameof(Uri), IsUnique = true), Index(nameof(Limsi), IsUnique = true)]
    public class Device : BaseEntity
    {
        /// <summary>
        ///     设备Uri
        /// </summary>
        [Required]
        public string Uri { get; set; } = null!;

        /// <summary>
        ///     设备Limsi码
        /// </summary>
        [Required]
        public string Limsi { get; set; } = null!;

        /// <summary>
        ///     设备设置
        /// </summary>
        public Setting Settings { get; set; } = null!;

        /// <summary>
        ///     设备操作记录
        /// </summary>
        public IQueryable<OperationLog> Logs = null!;
    }
}