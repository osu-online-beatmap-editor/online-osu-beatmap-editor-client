using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Timing
{
    public class BreakPeriod
    {
        public BreakPeriod(int start, int end)
        {
            Start = start;
            End = end;
        }

        private int _Start;
        public static event PropertyChangedEventHandler StartChanged;
        public int Start{get=>_Start;set{var _=_Start!=value?new Func<bool>(()=>{_Start=value;Utils.C(StartChanged,Start);return true;})():false;}}
       
        private int _End;
        public static event PropertyChangedEventHandler EndChanged;
        public int End{get=>_End;set{var _=_End!=value?new Func<bool>(()=>{_End=value;Utils.C(EndChanged,End);return true;})():false;}}
    }
}
