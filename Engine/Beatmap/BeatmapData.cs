using online_osu_beatmap_editor_client.gameplay_elements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void AppendHitObject (int time, HitObject hitObject)
        {
            List<HitObject> data;
            bool isExist = hitObjects.TryGetValue(time, out data);

            if (!isExist)
            {
                data = new();
            }

            data.Add(hitObject);
            hitObjects[time] = data;
        }

        public static List<HitObject> GetHitObjectsInRange(int timeMin, int timeMax)
        {
            return hitObjects.Where(i => i.Key >= timeMin && i.Key <= timeMax)
                             .SelectMany(i => i.Value)
                             .ToList();
        }
    }
}
