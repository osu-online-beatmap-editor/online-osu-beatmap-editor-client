using online_osu_beatmap_editor_client.gameplay_elements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class BeatmapData
    {
        public static General general { get; set; } = new();
        public static Editor editor { get; set; } = new();
        public static Metadata metadata { get; set; } = new();
        public static Difficulty difficulty { get; set; } = new();
        public static Events events { get; set; } = new();
        public static Dictionary<int, List<TimingPoint>> timingPoints { get; set; } = new();
        public static Dictionary<int, List<HitObject>> hitObjects { get; set; } = new();
    }
}
