using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Color
{
    public class Colour(int red = 0, int green = 0, int blue = 0, bool sliderOverride = false)
    {
        private static int _ID = 0;
        public static event PropertyChangedEventHandler IDChanged;
        public int ID{get=>_ID;set{var _=_ID!=value?new Func<bool>(()=>{_ID = sliderOverride ? -1 : ++_ID;Utils.C(IDChanged,ID);return true;})():false;}}

        private int _Red;
        public static event PropertyChangedEventHandler RedChanged;
        public int Red{get=>_Red;set{var _=_Red!=value?new Func<bool>(()=>{_Red=OsuMath.Clamp(red, 0, 255);Utils.C(RedChanged,Red);return true;})():false;}}

        private int _Green;
        public static event PropertyChangedEventHandler GreenChanged;
        public int Green{get=>_Green;set{var _=_Green!=value?new Func<bool>(()=>{_Green=OsuMath.Clamp(Green, 0, 255);Utils.C(GreenChanged,Green);return true;})():false;}}

        private int _Blue;
        public static event PropertyChangedEventHandler BlueChanged;
        public int Blue{get=>_Blue;set{var _=_Blue!=value?new Func<bool>(()=>{_Blue=OsuMath.Clamp(Blue, 0, 255);Utils.C(BlueChanged,Blue);return true;})():false;}}
    }
}
