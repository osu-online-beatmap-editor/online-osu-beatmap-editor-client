using online_osu_beatmap_editor_client.gameplay_elements.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.gameplay_elements.Objects
{
    public class HitObject
    {
        /// <summary>
        /// Position of the object on the playfield.
        /// </summary>
        private int _x;
        public static event PropertyChangedEventHandler xChanged;
        public int x{get=>_x;set{var _=_x!=value?(object)(()=>{_x=value;xChanged?.Invoke(null,new PropertyChangedEventArgs(nameof(x)));return true;}):false;}}

        private int _y;
        public static event PropertyChangedEventHandler yChanged; 
        public int y{get=>_y;set{var _=_y!=value?new Func<bool>(()=>{_y=value;yChanged?.Invoke(null,new PropertyChangedEventArgs(nameof(y)));return true;})():false;}}

        /// <summary>
        /// The time at which the HitObject starts.
        /// </summary>
        private int _startTime;
        public static event PropertyChangedEventHandler StartTimeChanged;
        public int StartTime
        {
            get => StartTime;
            set => StartTime = value;
        }

        public int _type { get; private set; } = 0;
        public void SetType(params int[] flags)
        {
            _type = 0;
            foreach (int flag in flags)
            {
                _type |= flag;
            }
        }

        public int _hitSound { get; private set; } = 0;
        public void SetHitSound(params int[] flags)
        {
            _hitSound = 0;
            foreach (int flag in flags)
            {
                _hitSound |= flag;
            }
        }

        /// <summary>
        /// The hit object's type parameter is an 8-bit integer where each bit is a flag with special meaning.
        /// - ColourHAX: A 3-bit integer specifying how many combo colours to skip, a practice referred to as "colour hax". 
        ///   Only relevant if the object starts a new combo.
        /// </summary>
        [Flags]
        public enum ObjectType
        {
            CIRCLE       = 0b00000001,
            SLIDER       = 0b00000010,
            NEWCOMBO     = 0b00000100,
            SPINNER      = 0b00001000,
            COLOURHAX1   = 0b00010000,
            COLOURHAX2   = 0b00100000,
            COLOURHAX3   = 0b01000000,
            OSUMANIAHOLD = 0b10000000
        }

        /// <summary>
        /// The hit object's type parameter is an 8-bit integer where each bit is a flag with special meaning.
        /// - ColourHAX: A 3-bit integer specifying how many combo colours to skip, a practice referred to as "colour hax". 
        ///   Only relevant if the object starts a new combo.
        /// </summary>
        [Flags]
        public enum HitSoundType
        {
            NORMAL  = 0b0001,
            WHISTLE = 0b0010,
            FINISH  = 0b0100,
            CLAP    = 0b1000
        }

        public double SpinnerEndTime
        {
            get => SpinnerEndTime;
            set => SpinnerEndTime = value;
        }

        public List<HitSample> hitSamples = [new HitSample()];
    }

    public class SliderParams
    {
        public SliderParams(CurveType curveType, List<Tuple<int, int>> curvePoints, 
            List<int> edgeSounds, List<string> edgeSets, int slides = 1) 
        {
            this.curveType = curveType;
            this.curvePoints = curvePoints;
            this.edgeSounds = edgeSounds;
            this.edgeSets = edgeSets;
            this.slides = slides;
            this.length = 0;    //how do you calculate this (and why is this needed)
        }

        public CurveType curveType;
        public enum CurveType
        {
            B,  //bezier
            C,  //catmull
            L,  //linear
            P   //perfect circle
        }

        /// <summary>
        /// Anchor points used to construct the slider. Each point is in the format x:y.
        /// </summary>
        public List<Tuple<int, int>> curvePoints = new();

        /// <summary>
        /// Amount of times the player has to follow the slider's curve back-and-forth before the slider is complete. 
        /// It can also be interpreted as the repeat count plus one.
        /// </summary>
        public int slides;

        /// <summary>
        /// Visual length of the slider in osu pixels.
        /// </summary>
        public float length;

        public List<int> edgeSounds = new();

        public List<string> edgeSets = new();
    }

    public class HitSample
    {
        public HitSample(int normalSet = 0, int additionSet = 0, int index = 0, int volume = 0, string filename = "")
        {
            this.normalSet = normalSet;
            this.additionSet = additionSet;
            this.index = index;
            this.volume = volume;
            this.filename = filename;
        }

        public int normalSet = 0;
        public int additionSet = 0;

        /// <summary>
        /// If this is 0, the timing point's sample index will be used instead.
        /// </summary>
        public int index = 0;

        /// <summary>
        /// If this is 0, the timing point's volume will be used instead.
        /// </summary>
        public int volume = 0;

        public string filename;
    }

}