using System.Collections.Generic;
using System;
using System.IO;

namespace online_osu_beatmap_editor_client.config
{
    public class AppConfigParser
    {
        public AppConfigParser() {
            string filePath = "config.cfg";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                Dictionary<string, string> config = new Dictionary<string, string>();

                foreach (string line in lines)
                {
                    if (!line.StartsWith("#"))
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            config[key] = value;
                        }
                    }
                }

                AppConfig.osuFolderPath = config["osuFolder"];
                AppConfig.defaultBackgroundDim = float.Parse(config["defaultBackgroundDim"]);
            }
            else
            {
                Console.WriteLine("Configuration file does not exist");
            }
        }
    }
}
