namespace Domain.Excel
{
    public class ApnExcelField
    {
        /// <summary>
        ///     客户名称 [column(1)]
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     用户号码 [column(2)]
        /// </summary>
        public string UserId { get; set; } = null!;

        /// <summary>
        ///     ICCID [column(3)]
        /// </summary>
        public string? ICCID { get; set; }

        /// <summary>
        ///     L_IMSI [column(4)]
        /// </summary>
        public string LIMSI { get; set; } = null!;
    }
}