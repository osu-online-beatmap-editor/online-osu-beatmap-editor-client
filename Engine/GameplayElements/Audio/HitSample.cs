using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.gameplay_elements.Audio
{
    public class HitSample
    {
        public HitSample(int normalSet = 0, int additionSet = 0, int index = 0, int volume = 0, string filename = "")
        {
            NormalSet = normalSet;
            AdditionSet = additionSet;
            Index = index;
            Volume = volume;
            Filename = filename;
        }

        private int _NormalSet;
        public static event PropertyChangedEventHandler NormalSetChanged;
        public int NormalSet{get=>_NormalSet;set{var _=_NormalSet!=value?new Func<bool>(()=>{_NormalSet=value;Utils.C(NormalSetChanged,NormalSet);return true;})():false;}}
        
        private int _AdditionSet;
        public static event PropertyChangedEventHandler AdditionSetChanged;
        public int AdditionSet{get=>_AdditionSet;set{var _=_AdditionSet!=value?new Func<bool>(()=>{_AdditionSet=value;Utils.C(AdditionSetChanged,AdditionSet);return true;})():false;}}

        /// <summary>
        /// If this is 0, the timing point's sample index will be used instead.
        /// </summary>
        private int _Index;
        public static event PropertyChangedEventHandler IndexChanged;
        public int Index{get=>_Index;set{var _=_Index!=value?new Func<bool>(()=>{_Index=value;Utils.C(IndexChanged,Index);return true;})():false;}}

        /// <summary>
        /// If this is 0, the timing point's volume will be used instead.
        /// </summary>
        public int volume = 0;
        private int _Volume;
        public static event PropertyChangedEventHandler VolumeChanged;
        public int Volume{get=>_Volume;set{var _=_Volume!=value?new Func<bool>(()=>{_Volume=value;Utils.C(VolumeChanged,Volume);return true;})():false;}}

        private string _Filename;
        public static event PropertyChangedEventHandler FilenameChanged;
        public string Filename{get=>_Filename;set{var _=_Filename!=value?new Func<bool>(()=>{_Filename=value;Utils.C(FilenameChanged,Filename);return true;})():false;}}
    }
}
