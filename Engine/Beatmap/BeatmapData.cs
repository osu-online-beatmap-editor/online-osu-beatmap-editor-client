﻿using online_osu_beatmap_editor_client.Engine.GameplayElements.Colours;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Timing;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class BeatmapData
    {
        public static List<Colour> colours { get; set; } = new();
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

        public static void RemoveHitObjectById(int id)
        {
            foreach (var i in hitObjects)
            {
                i.Value.RemoveAll(hitObject => hitObject.Id == id);
            }
        }

        public static HitObject GetHitObjectByTimeAndId(int id, int time)
        {
            var hitObjectList = hitObjects[time];
            for (int j = 0; j < hitObjectList.Count; j++)
            {
                if (hitObjectList[j].Id == id)
                {
                    return hitObjectList[j];
                }
            }

            return null;
        }
    }
}
