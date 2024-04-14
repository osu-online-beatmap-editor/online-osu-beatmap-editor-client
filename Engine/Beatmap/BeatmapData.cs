using online_osu_beatmap_editor_client.Engine.GameplayElements.Colours;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Timing;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class BeatmapData
    {
        public static List<Colour> colours { get; set; } = new();
        public static Dictionary<int, List<TimingPoint>> timingPoints { get; set; } = new();
        public static Dictionary<int, List<HitObject>> hitObjects { get; set; } = new();
    }
}
