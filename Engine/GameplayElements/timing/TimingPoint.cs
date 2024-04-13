using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Timing
{
    public class TimingPoint 
    {
        public TimingPoint(int time, float beatLength, int volume, bool uninherited = true, int meter = 4, int sampleSet = 0, 
        int sampleIndex = 0, int effects = 0)
        {
            Time = time;
            BeatLength = beatLength;
            Volume = volume;
            Uninherited = uninherited;
            Meter = meter;
            SampleSet = sampleSet;
            SampleIndex = sampleIndex;
            Effects = effects;
        }

        private int _Time;
        public static event PropertyChangedEventHandler TimeChanged;
        /// <summary>
        /// Start time of the timing section, in milliseconds from the beginning of the beatmap's audio. 
        /// The end of the timing section is the next timing point's time (or never, if this is the last timing point).
        /// </summary>
        public int Time{get=>_Time;set{var _=_Time!=value?new Func<bool>(()=>{_Time=value;Utils.C(TimeChanged,Time);return true;})():false;}}
        
        private float _BeatLength;
        public static event PropertyChangedEventHandler BeatLengthChanged;
        /// <summary>
        /// BeatLength has two meanings:
        /// - For uninherited timing points, the duration of a beat, in milliseconds.
        /// - For inherited timing points, a negative inverse slider velocity multiplier, as a percentage. 
        ///   For example, -50 would make all sliders in this timing section twice as fast as SliderMultiplier.
        /// Additionaly: BeatLength duration is calculated as [ BeatLength = 60000 / BPM ]
        /// </summary>
        public float BeatLength{get=>_BeatLength;set{var _=_BeatLength!=value?new Func<bool>(()=>{_BeatLength=value;Utils.C(BeatLengthChanged,BeatLength);return true;})():false;}}

        private int _Meter;
        public static event PropertyChangedEventHandler MeterChanged;
        /// <summary>
        /// Either 3 or 4. Inherited timing points ignore this property. 
        /// </summary>
        public int Meter{get=>_Meter;set{var _=_Meter!=value?new Func<bool>(()=>{_Meter=value;Utils.C(MeterChanged,Meter);return true;})():false;}}

        private int _SampleSet;
        public static event PropertyChangedEventHandler SampleSetChanged;
        /// <summary>
        /// Default sample set for hit objects (0 = beatmap default, 1 = normal, 2 = soft, 3 = drum).
        /// </summary>
        public int SampleSet{get=>_SampleSet;set{var _=_SampleSet!=value?new Func<bool>(()=>{_SampleSet=value;Utils.C(SampleSetChanged,SampleSet);return true;})():false;}}

        private int _SampleIndex;
        public static event PropertyChangedEventHandler SampleIndexChanged;
        /// <summary>
        /// Custom sample index for hit objects. 0 indicates osu!'s default hitsounds.
        /// </summary>
        public int SampleIndex{get=>_SampleIndex;set{var _=_SampleIndex!=value?new Func<bool>(()=>{_SampleIndex=value;Utils.C(SampleIndexChanged,SampleIndex);return true;})():false;}}

        private int _Volume;
        public static event PropertyChangedEventHandler VolumeChanged;
        /// <summary>
        /// Volume percentage for hit objects.
        /// </summary>
        public int Volume{get=>_Volume;set{var _=_Volume!=value?new Func<bool>(()=>{_Volume=value;Utils.C(VolumeChanged,Volume);return true;})():false;}}

        private bool _Uninherited;
        public static event PropertyChangedEventHandler UninheritedChanged;
        /// <summary>
        /// (0 or 1): Whether or not the timing point is uninherited.
        /// </summary>
        public bool Uninherited{get=>_Uninherited;set{var _=_Uninherited!=value?new Func<bool>(()=>{_Uninherited=value;Utils.C(UninheritedChanged,Uninherited);return true;})():false;}}

        private int _Effects;
        public static event PropertyChangedEventHandler EffectsChanged;
        /// <summary>
        /// Bit flags that give the timing point extra effects. 
        /// 0 - Nothing
        /// 1 - Kiai Time
        /// </summary>
        public int Effects{get=>_Effects;set{var _=_Effects!=value?new Func<bool>(()=>{_Effects=value;Utils.C(EffectsChanged,Effects);return true;})():false;}}

        private bool _Kiai;
        public static event PropertyChangedEventHandler KiaiChanged;
        public bool Kiai{get=>_Kiai;set{var _=_Kiai!=value?new Func<bool>(()=>{_Kiai=value;Utils.C(KiaiChanged,Kiai);return true;})():false;}}
    }
}
