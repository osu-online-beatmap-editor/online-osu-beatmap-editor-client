using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public class General
    {
        //always "osu file format v14" i think but it's here just in case
        public string gameFileVersion = "osu file format v14";

        //[General]
        private string _AudioFileName;
        public static event PropertyChangedEventHandler AudioFileNameChanged;
        public string AudioFileName{get=>_AudioFileName;set{var _=_AudioFileName!=value?new Func<bool>(()=>{_AudioFileName=value;Utils.C(AudioFileNameChanged,AudioFileName);return true;})():false;}}

        private int _AudioLeadIn;
        public static event PropertyChangedEventHandler AudioLeadInChanged;
        public int AudioLeadIn{get=>_AudioLeadIn;set{var _=_AudioLeadIn!=value?new Func<bool>(()=>{_AudioLeadIn=value;Utils.C(AudioLeadInChanged,AudioLeadIn);return true;})():false;}}

        private int _PreviewTime;
        public static event PropertyChangedEventHandler PreviewTimeChanged;
        public int PreviewTime{get=>_PreviewTime;set{var _=_PreviewTime!=value?new Func<bool>(()=>{_PreviewTime=value;Utils.C(PreviewTimeChanged,PreviewTime);return true;})():false;}}

        private int _Countdown;
        public static event PropertyChangedEventHandler CountdownChanged;
        public int Countdown{get=>_Countdown;set{var _=_Countdown!=value?new Func<bool>(()=>{_Countdown=value;Utils.C(CountdownChanged,Countdown);return true;})():false;}}

        private string _SampleSet;
        public static event PropertyChangedEventHandler SampleSetChanged;
        public string SampleSet{get=>_SampleSet;set{var _=_SampleSet!=value?new Func<bool>(()=>{_SampleSet=value;Utils.C(SampleSetChanged,SampleSet);return true;})():false;}}

        private int _StackLeniency;
        public static event PropertyChangedEventHandler StackLeniencyChanged;
        public int StackLeniency{get=>_StackLeniency;set{var _=_StackLeniency!=value?new Func<bool>(()=>{_StackLeniency=value;Utils.C(StackLeniencyChanged,StackLeniency);return true;})():false;}}

        private int _Mode;
        public static event PropertyChangedEventHandler ModeChanged;
        public int Mode{get=>_Mode;set{var _=_Mode!=value?new Func<bool>(()=>{_Mode=value;Utils.C(ModeChanged,Mode);return true;})():false;}}

        private int _LetterboxInBreaks;
        public static event PropertyChangedEventHandler LetterboxInBreaksChanged;
        public int LetterboxInBreaks{get=>_LetterboxInBreaks;set{var _=_LetterboxInBreaks!=value?new Func<bool>(()=>{_LetterboxInBreaks=value;Utils.C(LetterboxInBreaksChanged,LetterboxInBreaks);return true;})():false;}}

        private int _WidescreenStoryboard;
        public static event PropertyChangedEventHandler WidescreenStoryboardChanged;
        public int WidescreenStoryboard{get=>_WidescreenStoryboard;set{var _=_WidescreenStoryboard!=value?new Func<bool>(()=>{_WidescreenStoryboard=value;Utils.C(WidescreenStoryboardChanged,WidescreenStoryboard);return true;})():false;}}
    }
}
