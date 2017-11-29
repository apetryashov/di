namespace WindowsFormsApp1
{
    public class CloudConfiguration : ICloudConfiguration
    {
        public string Path { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }
        public int WordsInCloud { get; set; }
    }
}