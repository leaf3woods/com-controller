using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Repos.Model
{
    [Owned]
    public class Setting : BaseEntity
    {
        #region apn setting
        [Required]
        public string AccessPoint { get; set; } = null!;
        [Required]
        public string ApUserId { get; set; } = null!;
        [Required]
        public string ApPassword { get; set; } = null!;

        #endregion apn setting

        #region mqtt setting
        [Required]
        public string MqttServer { get; set; } = null!;

        [Required]
        public string MqttPort { get; set; } = null!;
        [Required]
        public string MqttUserName { get; set; } = null!;
        [Required]
        public string MqttPassword { get; set; } = null!;

        #endregion mqtt setting
    }
}