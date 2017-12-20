namespace TagsCloudVisualization
{
    public interface IConfig
    {
        CloudConfiguration CloudConfiguration { get; set; }
        SerizlizeViewConfiguration ViewConfiguration { get; set; }
        IgnoreWordsConfiguration IgnoreWordsConfiguration { get; set; }
    }

    public class Config : IConfig
    {
        public CloudConfiguration CloudConfiguration { get; set; }
        public SerizlizeViewConfiguration ViewConfiguration { get; set; }
        public IgnoreWordsConfiguration IgnoreWordsConfiguration { get; set; }
    }
}