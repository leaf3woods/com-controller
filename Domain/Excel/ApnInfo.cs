namespace Domain.Excel
{
    public class ApnInfo
    {
        public string? Name { get; set; }
        public string UserId { get; set; } = null!;
        public string LIMSI { get; set; } = null!;
        public string? ICCID { get; set; }
    }
}