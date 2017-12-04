namespace TagsCloudVisualization
{
    public interface ICloudConfiguration
    {
        string Path { get; set; }
        int MinFontSize { get; set; }
        int MaxFontSize { get; set; }
        int NumberOfWordsInTheCloud { get; set; }
    }
}