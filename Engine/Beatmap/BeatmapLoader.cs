using online_osu_beatmap_editor_client.config;
using online_osu_beatmap_editor_client.Engine.Beatmap;
using System;
using System.IO;
using System.Windows.Forms;

namespace online_osu_beatmap_editor_client.Engine
{
    public class BeatmapLoader
    {
        private string beatmapFileExtension = ".osu"; 

        public void OpenBeatmapLoadingDialog()
        {
            string selectedFile = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = $"Osu beatmap file (*{beatmapFileExtension})|*{beatmapFileExtension}";
            openFileDialog.InitialDirectory = Path.Combine(AppConfig.osuFolderPath, "songs");

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedFile = openFileDialog.FileName;
            }

            LoadBeatmapFromFile(selectedFile);
        }

        private void LoadBeatmapFromFile(string filePath)
        {
            if (filePath == null || !File.Exists(filePath)) {
                return;
            }

            BeatmapParser.ParseBeatmap(File.ReadAllLines(filePath));
        }
    }
}
