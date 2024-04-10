using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class TimingPoints
    {
        public static List<TimingPoint> timingPoints { get; private set; } = new();
        public static void AddTimingPoint(TimingPoint timingPoint) 
        { 
            timingPoints.Add(timingPoint); 
        }
    }
}
