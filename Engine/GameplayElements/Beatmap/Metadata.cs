using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public class Metadata
    {
        //Metadata
        public string Title;
        public string TitleUnicode;
        public string Artist;
        public string ArtistUnicode;
        public string Creator;
        public string Version;  //diffName
        public string Source;
        public string Tags;
        public int BeatmapID;
        public int BeatmapSetID;
    }
}
