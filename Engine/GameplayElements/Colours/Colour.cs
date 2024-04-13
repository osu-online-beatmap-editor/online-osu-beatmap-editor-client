using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Colours
{
    public class Colour
    {
        public Colour(int red = 0, int green = 0, int blue = 0, bool sliderBorder = false, bool sliderTrackOverride = false)
        {
            Red = red;
            Green = green;
            Blue = blue;
            SliderBorder = sliderBorder;
            SliderTrackOverride = sliderTrackOverride;
        }

        private int _Red;
        public static event PropertyChangedEventHandler RedChanged;
        public int Red{get=>_Red;set{var _=_Red!=value?new Func<bool>(()=>{_Red=OsuMath.Clamp(value, 0, 255);Utils.C(RedChanged,Red);return true;})():false;}}

        private int _Green;
        public static event PropertyChangedEventHandler GreenChanged;
        public int Green{get=>_Green;set{var _=_Green!=value?new Func<bool>(()=>{_Green=OsuMath.Clamp(value, 0, 255);Utils.C(GreenChanged,Green);return true;})():false;}}

        private int _Blue;
        public static event PropertyChangedEventHandler BlueChanged;
        public int Blue{get=>_Blue;set{var _=_Blue!=value?new Func<bool>(()=>{_Blue=OsuMath.Clamp(value, 0, 255);Utils.C(BlueChanged,Blue);return true;})():false;}}

        private bool _SliderBorder;
        public static event PropertyChangedEventHandler SliderBorderChanged;
        public bool SliderBorder{get=>_SliderBorder;set{var _=_SliderBorder!=value?new Func<bool>(()=>{_SliderBorder=value;Utils.C(SliderBorderChanged,SliderBorder);return true;})():false;}}

        private bool _SliderTrackOverride;
        public static event PropertyChangedEventHandler SliderTrackOverrideChanged;
        public bool SliderTrackOverride{get=>_SliderTrackOverride;set{var _=_SliderTrackOverride!=value?new Func<bool>(()=>{_SliderTrackOverride=value;Utils.C(SliderTrackOverrideChanged,SliderTrackOverride);return true;})():false;}}
    }
}
