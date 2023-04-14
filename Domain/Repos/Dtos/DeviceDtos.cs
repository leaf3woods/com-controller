namespace Domain.Repos.Dtos
{
    public class DeviceReadDto : ReadableDto
    {
        /// <summary>
        ///     查询Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     设备Uri
        /// </summary>
        public string Uri { get; set; } = null!;

        /// <summary>
        ///     设备Limsi码
        /// </summary>
        public string Limsi { get; set; } = null!;
    }

    public class DeviceCreateDto : CreateableDto
    {
        /// <summary>
        ///     设备Uri
        /// </summary>
        public string Uri { get; set; } = null!;

        /// <summary>
        ///     设备Limsi码
        /// </summary>
        public string Limsi { get; set; } = null!;
    }

    public class DeviceUpdateDto : UpdateableDto
    {
        /// <summary>
        ///     查询Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     设备Uri
        /// </summary>
        public string? Uri { get; set; } = null!;

        /// <summary>
        ///     设备Limsi码
        /// </summary>
        public string? Limsi { get; set; } = null!;
    }
}