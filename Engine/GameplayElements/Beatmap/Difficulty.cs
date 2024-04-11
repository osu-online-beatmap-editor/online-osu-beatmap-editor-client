using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements
{
    public class Difficulty
    {
        //Difficulty
        private float _HPDrainRate;
        public static event PropertyChangedEventHandler HPDrainRateChanged;
        public float HPDrainRate{get=>_HPDrainRate;set{var _=_HPDrainRate!=value?new Func<bool>(()=>{_HPDrainRate=(float)Math.Round(value, 2);Utils.C(HPDrainRateChanged,HPDrainRate);return true;})():false;}}

        private float _CircleSize;
        public static event PropertyChangedEventHandler CircleSizeChanged;
        public float CircleSize{get=>_CircleSize;set{var _=_CircleSize!=value?new Func<bool>(()=>{_CircleSize=(float)Math.Round(value, 2);Utils.C(CircleSizeChanged,CircleSize);return true;})():false;}}

        private float _OverallDifficulty;
        public static event PropertyChangedEventHandler OverallDifficultyChanged;
        public float OverallDifficulty{get=>_OverallDifficulty;set{var _=_OverallDifficulty!=value?new Func<bool>(()=>{_OverallDifficulty=(float)Math.Round(value, 2);Utils.C(OverallDifficultyChanged,OverallDifficulty);return true;})():false;}}

        private float _ApproachRate;
        public static event PropertyChangedEventHandler ApproachRateChanged;
        public float ApproachRate{get=>_ApproachRate;set{var _=_ApproachRate!=value?new Func<bool>(()=>{_ApproachRate=(float)Math.Round(value, 2);Utils.C(ApproachRateChanged,ApproachRate);return true;})():false;}}

        private float _SliderMultiplier;
        public static event PropertyChangedEventHandler SliderMultiplierChanged;
        public float SliderMultiplier{get=>_SliderMultiplier;set{var _=_SliderMultiplier!=value?new Func<bool>(()=>{_SliderMultiplier=(float)Math.Round(value, 1);Utils.C(SliderMultiplierChanged,SliderMultiplier);return true;})():false;}}

        private float _SliderTickRate;
        public static event PropertyChangedEventHandler SliderTickRateChanged;
        public float SliderTickRate{get=>_SliderTickRate;set{var _=_SliderTickRate!=value?new Func<bool>(()=>{_SliderTickRate=(float)Math.Round(value, 1);Utils.C(SliderTickRateChanged,SliderTickRate);return true;})():false;}}
    }
}
