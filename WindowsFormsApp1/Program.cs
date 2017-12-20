using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Autofac;
using Autofac.Core;
using Newtonsoft.Json;

namespace TagsCloudVisualization
{
    static class Program
    {
        private static IContainer Container { get; set; }

        private static void ContainerConfiguration()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<XmlConfigReader>().As<IConfigReader>();
            builder.Register(x => new Point(200, 200));
            builder.Register(x => "config.xml");
            builder.RegisterType<TagsCloudWorker>().As<ICloudWorker>();
            builder.RegisterType<CloudCombiner>().As<ICloudCombiner>();
            builder.RegisterType<TxtTextReader>().As<ITextReader>();
            builder.RegisterType<TagStatisticsGenerator>().As<ITagStatisticsGenerator>();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => typeof(ITagManipulator).IsAssignableFrom(t))
                .AsImplementedInterfaces();
            builder.RegisterType<ArchimedeanCircularCloudLayouter>().As<ICircularCloudLayouter>();
            builder.RegisterType<WinFormCloudVisualizer>().As<ICloudVisualizer>();
            Container = builder.Build();
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var config = new Config
            //{
            //    CloudConfiguration = new CloudConfiguration
            //    {
            //        MinFontSize = 10,
            //        MaxFontSize = 20,
            //        NumberOfWordsInTheCloud = 50,
            //        Path = "../../WarAndPeaceWords.txt"
            //    },
            //    IgnoreWordsConfiguration = new IgnoreWordsConfiguration
            //    {
            //        Paths = new[]
            //        {
            //            "../../ignore.txt",
            //        }
            //    },
            //    ViewConfiguration = new SerizlizeViewConfiguration()
            //    {
            //        Color = "Red",
            //        FontFamily = "Times New Roman",
            //        Height = 500,
            //        Width = 500
            //    }
            //};
            //var serializer = new XmlSerializer(typeof(Config));
            //var stream = new StreamWriter("config.xml");

            //serializer.Serialize(stream, config);

            ContainerConfiguration();
            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<ICloudWorker>().View();
            }
        }
    }

    //public interface IConfig
    //{
    //    CloudConfiguration CloudConfiguration { get; set; }
    //    SerizlizeViewConfiguration ViewConfiguration { get; set; }
    //    IgnoreWordsConfiguration IgnoreWordsConfiguration { get; set; }
    //}

    //public class Config : IConfig
    //{
    //    public CloudConfiguration CloudConfiguration { get; set; }
    //    public SerizlizeViewConfiguration ViewConfiguration { get; set; }
    //    public IgnoreWordsConfiguration IgnoreWordsConfiguration { get; set; }
    //}
}
