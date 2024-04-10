using online_osu_beatmap_editor_client.gameplay_elements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class HitObjects
    {
        public static List<HitObject> hitObjects { get; private set; } = new();
        public static void AddHitObject(HitObject hitObject)
        {
            hitObjects.Add(hitObject);
        }
    }
}
