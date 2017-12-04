using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;

namespace TagsCloudVisualization
{
    static class Program
    {
        private static IContainer Container { get; set; }

        private static void ContainerConfiguration()
        {
            var builder = new ContainerBuilder();
            builder.Register(x => new CloudConfiguration
            {
                Path = "../../WarAndPeaceWords.txt",
                MinFontSize = 10,
                MaxFontSize = 30,
                NumberOfWordsInTheCloud = 50
            }).As<ICloudConfiguration>();
            builder.Register(x => new IgnoreWordsConfiguration
            {
                Paths = new[]
                {
                    "../../ignore.txt"
                }
            }).As<IIgnoreWordsConfiguration>();
            builder.RegisterType<DefaultViewConfiguration>().As<IViewConfiguration>();
            builder.Register(x => new Point(200, 200));
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
            ContainerConfiguration();
            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<ICloudWorker>().View();
            }
        }
    }
}
