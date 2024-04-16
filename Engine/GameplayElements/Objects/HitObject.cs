using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.gameplay_elements.Audio;
using SFML.Graphics.Glsl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SFML.System;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Objects
{
    public class HitObject
    {
        public HitObject(Vector2i position, int time, int type, int hitSound, HitSample hitSample, 
            int spinnerEndTime = 0, SliderParams sliderParams = null)
        {
            Position = position;
            Time = time;
            TypeFlags = (ObjectFlags)type; SetTypeAndBoolsFromFlags();
            HitSoundTypeFlags = (HitSoundFlags)hitSound; SetHitsoundBoolsFromFlags();
            Sample = hitSample ?? new HitSample();
            SpinnerEndTime = spinnerEndTime;
            SliderParameters = sliderParams;
        }

        public HitObject(int x, int y, int time, ObjectType type, int hitSound = 0, HitSample hitSample = null,
            int spinnerEndTime = 0, SliderParams sliderParams = null)
        {
            X = x;
            Y = y;
            Time = time;
            TypeFlags = (ObjectFlags)type; SetTypeAndBoolsFromFlags();
            HitSoundTypeFlags = (HitSoundFlags)hitSound; SetHitsoundBoolsFromFlags();
            Sample = hitSample ?? new HitSample();
            SpinnerEndTime = spinnerEndTime;
            SliderParameters = sliderParams;
        }

        public int Id;

        /// <summary>
        /// Position of the object on the playfield.
        /// </summary>
        private Vector2i _Position;
        public static event PropertyChangedEventHandler PositionChanged;
        public Vector2i Position{get=>_Position;set{var _=_Position!=value?new Func<bool>(()=>{_Position=value;Utils.C(PositionChanged,Position);return true;})():false;}}

        /// <summary>
        /// The time at which the HitObject starts.
        /// </summary>
        private int _Time;
        public static event PropertyChangedEventHandler TimeChanged;
        public int Time{get=>_Time;set{var _=_Time!=value?new Func<bool>(()=>{_Time=value;Utils.C(TimeChanged,Time);return true;})():false;}}

        /// <summary>
        /// Set with SetTypeFlags(ObjectFlags[]).
        /// </summary>
        public ObjectFlags TypeFlags { get; private set; } = 0;
        public void SetTypeFlags(params ObjectFlags[] flags)
        {
            TypeFlags = 0;
            foreach (ObjectFlags flag in flags)
            {
                TypeFlags |= flag;
            }
        }
        public void SetTypeAndBoolsFromFlags()
        {
            if(TypeFlags.HasFlag(ObjectFlags.NEWCOMBO))
            {
                IsNewCombo = true;
            }
            if (TypeFlags.HasFlag(ObjectFlags.CIRCLE))
            {
                Type = ObjectType.CIRCLE;
            }
            else if (TypeFlags.HasFlag(ObjectFlags.SLIDER))
            {
                Type = ObjectType.SLIDER;
            }
            else if (TypeFlags.HasFlag(ObjectFlags.SPINNER))
            {
                Type = ObjectType.SPINNER;
            }
            if(TypeFlags.HasFlag(ObjectFlags.COLOURHAX3))
            {
                ColourHax += 4;
            }
            if(TypeFlags.HasFlag(ObjectFlags.COLOURHAX2))
            {
                ColourHax += 2;
            }
            if(TypeFlags.HasFlag(ObjectFlags.COLOURHAX1))
            {
                ColourHax += 1;
            }
        }

        /// <summary>
        /// Set with SetHitSoundTypeFlags(HitSoundFlags[]).
        /// </summary>
        public HitSoundFlags HitSoundTypeFlags { get; private set; } = 0;
        public void SetHitSoundTypeFlags(params HitSoundFlags[] flags)
        {
            HitSoundTypeFlags = 0;
            foreach (HitSoundFlags flag in flags)
            {
                HitSoundTypeFlags |= flag;
            }
        }
        public void SetHitsoundBoolsFromFlags()
        {
            if(HitSoundTypeFlags.HasFlag(HitSoundFlags.NORMAL))
            {
                HasNormalHitsound = true;
            }
            if (HitSoundTypeFlags.HasFlag(HitSoundFlags.WHISTLE))
            {
                HasWhistleHitsound = true;
            }
            if (HitSoundTypeFlags.HasFlag(HitSoundFlags.FINISH))
            {
                HasFinishHitsound = true;
            }
            if (HitSoundTypeFlags.HasFlag(HitSoundFlags.CLAP))
            {
                HasClapHitsound = true;
            }
        }

        private bool _IsNewCombo;
        public static event PropertyChangedEventHandler IsNewComboChanged;
        public bool IsNewCombo{get=>_IsNewCombo;set{var _=_IsNewCombo!=value?new Func<bool>(()=>{_IsNewCombo=value;Utils.C(IsNewComboChanged,IsNewCombo);return true;})():false;}}

        private ObjectType _Type;
        public static event PropertyChangedEventHandler typeChanged;
        public ObjectType Type{get=>_Type;set{var _=_Type!=value?new Func<bool>(()=>{_Type =value;Utils.C(typeChanged,Type);return true;})():false;}}

        private bool _HasNormalHitsound;
        public static event PropertyChangedEventHandler HasNormalHitsoundChanged;
        public bool HasNormalHitsound{get=>_HasNormalHitsound;set{var _=_HasNormalHitsound!=value?new Func<bool>(()=>{_HasNormalHitsound=value;Utils.C(HasNormalHitsoundChanged,HasNormalHitsound);return true;})():false;}}

        private bool _HasWhistleHitsound;
        public static event PropertyChangedEventHandler HasWhistleHitsoundChanged;
        public bool HasWhistleHitsound{get=>_HasWhistleHitsound;set{var _=_HasWhistleHitsound!=value?new Func<bool>(()=>{_HasWhistleHitsound=value;Utils.C(HasWhistleHitsoundChanged,HasWhistleHitsound);return true;})():false;}}

        private bool _HasFinishHitsound;
        public static event PropertyChangedEventHandler HasFinishHitsoundChanged;
        public bool HasFinishHitsound{get=>_HasFinishHitsound;set{var _=_HasFinishHitsound!=value?new Func<bool>(()=>{_HasFinishHitsound=value;Utils.C(HasFinishHitsoundChanged,HasFinishHitsound);return true;})():false;}}

        private bool _HasClapHitsound;
        public static event PropertyChangedEventHandler HasClapHitsoundChanged;
        public bool HasClapHitsound{get=>_HasClapHitsound;set{var _=_HasClapHitsound!=value?new Func<bool>(()=>{_HasClapHitsound=value;Utils.C(HasClapHitsoundChanged,HasClapHitsound);return true;})():false;}}

        public enum ObjectType
        {
            CIRCLE,
            SLIDER,
            SPINNER
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

        private HitSample _Sample;
        public static event PropertyChangedEventHandler SampleChanged;
        public HitSample Sample{get=>_Sample;set{var _=_Sample!=value?new Func<bool>(()=>{_Sample=value;Utils.C(SampleChanged, Sample);return true;})():false;}}
    
        private int _ColourHax;
        public static event PropertyChangedEventHandler ColourHaxChanged;
        public int ColourHax{get=>_ColourHax;set{var _=_ColourHax!=value?new Func<bool>(()=>{_ColourHax=value;Utils.C(ColourHaxChanged, ColourHax);return true;})():false;}}
    
        private SliderParams _SliderParameters;
        public static event PropertyChangedEventHandler SliderParametersChanged;
        public SliderParams SliderParameters{get=>_SliderParameters;set{var _=_SliderParameters!=value?new Func<bool>(()=>{_SliderParameters=value;Utils.C(SliderParametersChanged, SliderParameters);return true;})():false;}}
    }

    public class SliderParams
    {
        public SliderParams(Curve curveType, List<CurvePoint> curvePoints, int slides,
            float length, List<int> edgeSounds, List<string> edgeSets) 
        {
            CurveType = curveType;
            CurvePoints = curvePoints;
            Slides = slides;
            Length = length;    //how do you calculate this (needed for calc slider length in ms apparently)
            EdgeSounds = edgeSounds;
            EdgeSets = edgeSets;
        }

        private Curve _CurveType;
        public static event PropertyChangedEventHandler CurveTypeChanged;
        public Curve CurveType{get=>_CurveType;set{var _=_CurveType!=value?new Func<bool>(()=>{_CurveType=value;Utils.C(CurveTypeChanged, CurveType);return true;})():false;}}

        public enum Curve
        {
            B = 'B',  //bezier
            C = 'C',  //catmull
            L = 'L',  //linear
            P = 'P'   //perfect circle
        }

        /// <summary>
        /// Anchor points used to construct the slider. Each point is in the format x:y.
        /// </summary>
        private List<CurvePoint> _CurvePoints;
        public static event PropertyChangedEventHandler CurvePointsChanged;
        public List<CurvePoint> CurvePoints{get=>_CurvePoints;set{var _=_CurvePoints!=value?new Func<bool>(()=>{_CurvePoints=value;Utils.C(CurvePointsChanged, CurvePoints);return true;})():false;}}

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