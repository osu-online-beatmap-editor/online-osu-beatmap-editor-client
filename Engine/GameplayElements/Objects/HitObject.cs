using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.gameplay_elements.Audio;
using SFML.Graphics.Glsl;
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
        private int _X;
        public static event PropertyChangedEventHandler XChanged;
        public int X{get=>_X;set{var _=_X!=value?new Func<bool>(()=>{_X=value;Utils.C(XChanged,X);return true;})():false;}}

        private int _Y;
        public static event PropertyChangedEventHandler YChanged; 
        public int Y{get=>_Y;set{var _=_Y!=value?new Func<bool>(()=>{_Y=value;Utils.C(YChanged,Y);return true;})():false;}}

        /// <summary>
        /// The time at which the HitObject starts.
        /// </summary>
        private int _StartTime;
        public static event PropertyChangedEventHandler StartTimeChanged;
        public int StartTime{get=>_StartTime;set{var _=_StartTime!=value?new Func<bool>(()=>{_StartTime=value;Utils.C(StartTimeChanged,StartTime);return true;})():false;}}

        /// <summary>
        /// Set with SetTypeFlags(ObjectFlags[]).
        /// </summary>
        public int TypeFlags { get; private set; } = 0;
        public void SetTypeFlags(params int[] flags)
        {
            TypeFlags = 0;
            foreach (int flag in flags)
            {
                TypeFlags |= flag;
            }
        }

        /// <summary>
        /// Set with SetHitSoundTypeFlags(HitSoundFlags[]).
        /// </summary>
        public int HitSoundTypeFlags { get; private set; } = 0;
        public void SetHitSoundTypeFlags(params int[] flags)
        {
            HitSoundTypeFlags = 0;
            foreach (int flag in flags)
            {
                HitSoundTypeFlags |= flag;
            }
        }

        private bool _IsNewCombo;
        public static event PropertyChangedEventHandler IsNewComboChanged;
        public bool IsNewCombo{get=>_IsNewCombo;set{var _=_IsNewCombo!=value?new Func<bool>(()=>{_IsNewCombo=value;Utils.C(IsNewComboChanged,IsNewCombo);return true;})():false;}}

        private ObjectType _Type;
        public static event PropertyChangedEventHandler typeChanged;
        public ObjectType Type{get=>_Type;set{var _=_Type!=value?new Func<bool>(()=>{_Type =value;Utils.C(typeChanged,Type);return true;})():false;}}

        private List<HitSoundType> _HitSoundTypes;
        public static event PropertyChangedEventHandler HitSoundTypesChanged;
        public List<HitSoundType> HitSoundTypes{get=>_HitSoundTypes;set{var _=_HitSoundTypes!=value?new Func<bool>(()=>{_HitSoundTypes=value;Utils.C(HitSoundTypesChanged,HitSoundTypes);return true;})():false;}}

        public enum ObjectType
        {
            CIRCLE,
            SLIDER,
            SPINNER
        }

        public enum HitSoundType
        {
            NORMAL,
            WHISTLE,
            FINISH,
            CLAP
        }

        /// <summary>
        /// The hit object's type parameter is an 8-bit integer where each bit is a flag with special meaning.
        /// - ColourHAX: A 3-bit integer specifying how many combo colours to skip, a practice referred to as "colour hax". 
        ///   Only relevant if the object starts a new combo.
        /// </summary>
        [Flags]
        public enum ObjectFlags
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
        /// The hitsound's type parameter is an 4-bit integer where each bit is a flag with special meaning.
        /// </summary>
        [Flags]
        public enum HitSoundFlags
        {
            NORMAL  = 0b0001,
            WHISTLE = 0b0010,
            FINISH  = 0b0100,
            CLAP    = 0b1000
        }

        private double _SpinnerEndTime;
        public static event PropertyChangedEventHandler SpinnerEndTimeChanged;
        public double SpinnerEndTime{get=>_SpinnerEndTime;set{var _=_SpinnerEndTime!=value?new Func<bool>(()=>{_SpinnerEndTime=value;Utils.C(SpinnerEndTimeChanged, SpinnerEndTime);return true;})():false;}}

        private List<HitSample> _HitSamples;
        public static event PropertyChangedEventHandler HitSamplesChanged;
        public List<HitSample> HitSamples{get=>_HitSamples;set{var _=_HitSamples!=value?new Func<bool>(()=>{_HitSamples=value;Utils.C(HitSamplesChanged, HitSamples);return true;})():false;}}
    }

    public class SliderParams
    {
        public SliderParams(Curve curveType, List<Tuple<int, int>> curvePoints, 
            List<int> edgeSounds, List<string> edgeSets, int slides = 1) 
        {
            CurveType = curveType;
            CurvePoints = curvePoints;
            EdgeSounds = edgeSounds;
            EdgeSets = edgeSets;
            Slides = slides;
            Length = 0;    //how do you calculate this (and why is this needed)
        }

        private Curve _CurveType;
        public static event PropertyChangedEventHandler CurveTypeChanged;
        public Curve CurveType{get=>_CurveType;set{var _=_CurveType!=value?new Func<bool>(()=>{_CurveType=value;Utils.C(CurveTypeChanged, CurveType);return true;})():false;}}

        public enum Curve
        {
            B,  //bezier
            C,  //catmull
            L,  //linear
            P   //perfect circle
        }

        /// <summary>
        /// Anchor points used to construct the slider. Each point is in the format x:y.
        /// </summary>
        private List<Tuple<int, int>> _CurvePoints;
        public static event PropertyChangedEventHandler CurvePointsChanged;
        public List<Tuple<int, int>> CurvePoints{get=>_CurvePoints;set{var _=_CurvePoints!=value?new Func<bool>(()=>{_CurvePoints=value;Utils.C(CurvePointsChanged, CurvePoints);return true;})():false;}}

        /// <summary>
        /// Amount of times the player has to follow the slider's curve back-and-forth before the slider is complete. 
        /// It can also be interpreted as the repeat count plus one.
        /// </summary>
        private int _Slides;
        public static event PropertyChangedEventHandler SlidesChanged;
        public int Slides{get=>_Slides;set{var _=_Slides!=value?new Func<bool>(()=>{_Slides=value;Utils.C(SlidesChanged, Slides);return true;})():false;}}

        /// <summary>
        /// Visual length of the slider in osu pixels.
        /// </summary>
        private float _Length;
        public static event PropertyChangedEventHandler LengthChanged;
        public float Length{get=>_Length;set{var _=_Length!=value?new Func<bool>(()=>{_Length=value;Utils.C(LengthChanged, Length);return true;})():false;}}


        private List<int> _EdgeSounds;
        public static event PropertyChangedEventHandler EdgeSoundsChanged;
        public List<int> EdgeSounds{get=>_EdgeSounds;set{var _=_EdgeSounds!=value?new Func<bool>(()=>{_EdgeSounds=value;Utils.C(EdgeSoundsChanged, EdgeSounds);return true;})():false;}}

        private List<string> _EdgeSets;
        public static event PropertyChangedEventHandler EdgeSetsChanged;
        public List<string> EdgeSets{get=>_EdgeSets;set{var _=_EdgeSets!=value?new Func<bool>(()=>{_EdgeSets=value;Utils.C(EdgeSetsChanged, EdgeSets);return true;})():false;}}
    }
}