namespace WindowsFormsApp1
{
    public interface ICloudConfiguration
    {
        string Path { get; set; }
        int MinFontSize { get; set; }
        int MaxFontSize { get; set; }
        int WordsInCloud { get; set; }
    }
}