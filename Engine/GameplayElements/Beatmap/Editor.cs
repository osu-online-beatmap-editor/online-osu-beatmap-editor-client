using SFML.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public class Editor
    {
        public Dictionary<int, int> Bookmarks { get; set; } = new();

        public float DistanceSpacing { get; private set; }
        public void SetDistanceSpacing(float distanceSpacing)
        {
            DistanceSpacing = (float)Math.Round(distanceSpacing, 1);
        }

        public int BeatDivisor { get; set; }
        public int GridSize { get; set; }

        public float TimelineZoom { get; private set; }
    }
}
