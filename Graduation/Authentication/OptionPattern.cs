namespace Graduation.Authentication
{
    public class OptionPattern
    {
        public static string SectionName { get; } = "jwt";
        public string key { get; set; } = string.Empty;
        public string? issuer { get; set; } = string.Empty;
        public string? audience { get; set; } = string.Empty;
        public int durationInMinutes { get; set; } 
    }
}
