using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace TagsCloudVisualization
{
    public interface IConfigReader
    {
        Result<ICloudConfiguration> GetCloudConfiguration();
        Result<IIgnoreWordsConfiguration> GetIgnoreWordsConfiguration();
        Result<IViewConfiguration> GetViewConfiguration();
    }

    public interface ISerializeViewConfiguration
    {
        string Color { get; set; }
        string FontFamily { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }

    public class SerizlizeViewConfiguration : ISerializeViewConfiguration
    {
        public string Color { get; set; }
        public string FontFamily { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public class XmlConfigReader : IConfigReader
    {
        public string PathToConfFiel { get; }

        public XmlConfigReader(string pathToConfFiel)
        {
            PathToConfFiel = pathToConfFiel;
        }

        public Result<Config> GetConfig()
        {
            var serializer = new XmlSerializer(typeof(Config));
            return Result.Of(() =>
            {
                var reader = new StreamReader(PathToConfFiel);
                var result = (Config)serializer.Deserialize(reader);
                return result;
            });
        }

        public Result<ICloudConfiguration> GetCloudConfiguration()
        {
            return GetConfig()
                .Then(x => (ICloudConfiguration) x.CloudConfiguration);
        }

        public Result<IIgnoreWordsConfiguration> GetIgnoreWordsConfiguration()
        {
            return GetConfig()
                .Then(x => (IIgnoreWordsConfiguration)x.IgnoreWordsConfiguration);
        }

        public Result<IViewConfiguration> GetViewConfiguration()
        {
            return GetConfig()
                .Then(x =>
                {
                    var conf = x.ViewConfiguration;
                    return (IViewConfiguration) new ViewConfiguration
                    {
                        Color = Color.FromName(conf.Color),
                        FontFamily = new FontFamily(conf.FontFamily),
                        Height = conf.Height,
                        Width = conf.Width
                    };
                });
        }
    }
}