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
        public static General general { get; private set; } = new General();
        public static Metadata metadata { get; private set; } = new Metadata();
        public static Dictionary<int, HitObject> hitObjects { get; private set; } = new();
        public static Dictionary<int, TimingPoint> timingPoints { get; private set; } = new();
    }
}
