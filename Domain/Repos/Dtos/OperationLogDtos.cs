namespace Domain.Repos.Dtos
{
    public class OperationLogReadDto : ReadableDto
    {
        /// <summary>
        ///     查询Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     烧入时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        ///     烧入状态
        /// </summary>
        public bool? FlashInState { get; set; } = null!;

        /// <summary>
        ///     设备信息
        /// </summary>
        public DeviceReadDto Device { get; set; } = null!;
    }

    public class OperationLogUpdateDto : UpdateableDto
    {
        /// <summary>
        ///     查询Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     烧入时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        ///     烧入状态
        /// </summary>
        public bool? FlashInState { get; set; } = null!;

        /// <summary>
        ///     设备Id
        /// </summary>
        public Guid DeviceId { get; set; }
    }

    public class OperationLogCreateDto : CreateableDto
    {
        /// <summary>
        ///     烧入时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        ///     上次烧入状态
        /// </summary>
        public bool? FlashInState { get; set; } = null!;

        /// <summary>
        ///     设备Id
        /// </summary>
        public Guid DeviceId { get; set; }
    }
}