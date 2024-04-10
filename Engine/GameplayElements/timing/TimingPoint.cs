using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public class TimingPoint (int time, float beatLength, int volume, int uninherited = 1, int meter = 4, int sampleSet = 0, 
        int SampleIndex = 0, int effects = 0)
    {
        /// <summary>
        /// Start time of the timing section, in milliseconds from the beginning of the beatmap's audio. 
        /// The end of the timing section is the next timing point's time (or never, if this is the last timing point).
        /// </summary>
        public int Time = time;

        /// <summary>
        /// BeatLength has two meanings:
        /// - For uninherited timing points, the duration of a beat, in milliseconds.
        /// - For inherited timing points, a negative inverse slider velocity multiplier, as a percentage. 
        ///   For example, -50 would make all sliders in this timing section twice as fast as SliderMultiplier.
        /// Additionaly: BeatLength duration is calculated as [ BeatLength = 60000 / BPM ]
        /// </summary>
        public float BeatLength = beatLength;

        /// <summary>
        /// Either 3 or 4. Inherited timing points ignore this property. 
        /// </summary>
        public int Meter = meter;

        /// <summary>
        /// Default sample set for hit objects (0 = beatmap default, 1 = normal, 2 = soft, 3 = drum).
        /// </summary>
        public int SampleSet = sampleSet;

        /// <summary>
        /// Custom sample index for hit objects. 0 indicates osu!'s default hitsounds.
        /// </summary>
        public int SampleIndex = SampleIndex;

        /// <summary>
        /// Volume percentage for hit objects.
        /// </summary>
        public int Volume = volume;

        /// <summary>
        /// (0 or 1): Whether or not the timing point is uninherited.
        /// </summary>
        public int Uninherited = uninherited;

        /// <summary>
        /// Bit flags that give the timing point extra effects. 
        /// 0 - Nothing
        /// 1 - Kiai Time
        /// </summary>
        public int Effects = effects;
    }
}
