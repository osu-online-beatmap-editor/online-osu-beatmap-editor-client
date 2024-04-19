﻿using online_osu_beatmap_editor_client.Engine.GameplayElements.Colours;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Timing;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class BeatmapData
    {
        public static List<Colour> colours { get; set; } = new();
        public static List<BreakPeriod> breakPeriods { get; set; } = new();
        public static Dictionary<int, List<TimingPoint>> timingPoints { get; set; } = new();


        public static event PropertyChangedEventHandler HitObjectsChanged;
        private static Dictionary<int, List<HitObject>> _hitObjects = new();

        public static Dictionary<int, List<HitObject>> hitObjects
        {
            get { return _hitObjects; }
            set
            {
                if (_hitObjects != value)
                {
                    _hitObjects = value;
                    HitObjectsChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(hitObjects)));
                }
            }
        }

        public static void MapHitObjects ()
        {
            var sortedEntries = hitObjects.OrderBy(entry => entry.Key).ToList();
            int distance = 0;

            foreach (var hitObjectList in sortedEntries)
            {
                for (int j = hitObjectList.Value.Count - 1; j >= 0; j--)
                {
                    distance += 1;
                    if (hitObjectList.Value[j].IsNewCombo)
                    {
                        distance = 1;
                    }
                    hitObjectList.Value[j].Number = distance;
                }
            }
        }

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
            MapHitObjects();
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

        public static int DistanceToLastNewCombo(int time)
        {
            int distance = 0;

            foreach (var hitObjectList in hitObjects)
            {
                for (int j = hitObjectList.Value.Count - 1; j >= 0; j--)
                {
                    if (hitObjectList.Value[j].Time <= time)
                    {
                        distance += 1;
                        if (hitObjectList.Value[j].IsNewCombo)
                        {
                            distance = 1;
                        }
                    }
                    else
                    {
                        return distance;
                    }
                }
            }

            return distance;
        }
    }
}
