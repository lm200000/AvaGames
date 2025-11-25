using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CrossGames.Common
{
    public static class Appsetting
    {
        private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "Games.dll.config");

        public static int maxScore { get; set; }

        static Appsetting()
        {
            Load();
        }

        public static void Load()
        {
            if (!File.Exists(ConfigPath))
            {
                maxScore = 0;
                return;
            }

            var doc = XDocument.Load(ConfigPath);
            var value = doc.Root?
                .Element("appSettings")?
                .Elements("add")
                .FirstOrDefault(e => e.Attribute("key")?.Value == "maxScore")
                ?.Attribute("value")?.Value;

            if (int.TryParse(value, out int score))
                maxScore = score;
            else
                maxScore = 0;
        }

        public static void Save()
        {
            XDocument doc;
            if (File.Exists(ConfigPath))
            {
                doc = XDocument.Load(ConfigPath);
            }
            else
            {
                doc = new XDocument(
                    new XElement("configuration",
                        new XElement("appSettings",
                            new XElement("add", new XAttribute("key", "maxScore"), new XAttribute("value", maxScore))
                        )
                    )
                );
                doc.Save(ConfigPath);
                return;
            }

            var appSettings = doc.Root?.Element("appSettings");
            if (appSettings == null)
            {
                appSettings = new XElement("appSettings");
                doc.Root?.Add(appSettings);
            }

            var maxScoreElement = appSettings.Elements("add")
                .FirstOrDefault(e => e.Attribute("key")?.Value == "maxScore");

            if (maxScoreElement != null)
            {
                maxScoreElement.SetAttributeValue("value", maxScore);
            }
            else
            {
                appSettings.Add(new XElement("add", new XAttribute("key", "maxScore"), new XAttribute("value", maxScore)));
            }

            doc.Save(ConfigPath);
        }
    }
}