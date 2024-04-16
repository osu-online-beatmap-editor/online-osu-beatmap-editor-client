using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class Events
    {
        //Background and Video events
        
        private static string _Background;
        public static event PropertyChangedEventHandler BackgroundChanged;
        public static string Background{get=>_Background;set{var _=_Background!=value?new Func<bool>(()=>{_Background = value;Utils.C(BackgroundChanged,Background);return true;})():false;}}

        private static Vector2i _BackgroundOffset;
        public static event PropertyChangedEventHandler BackgroundOffsetChanged;
        public static Vector2i BackgroundOffset{get=>_BackgroundOffset;set{var _=_BackgroundOffset!=value?new Func<bool>(()=>{_BackgroundOffset = value;Utils.C(BackgroundOffsetChanged,BackgroundOffset);return true;})():false;}}

        private static string _Video;
        public static event PropertyChangedEventHandler VideoChanged;
        public static string Video{get=>_Video;set{var _=_Video!=value?new Func<bool>(()=>{_Video = value;Utils.C(VideoChanged,Video);return true;})():false;}}

        private static int _VideoOffset;
        public static event PropertyChangedEventHandler VideoOffsetChanged;
        public static int VideoOffset{get=>_VideoOffset;set{var _=_VideoOffset!=value?new Func<bool>(()=>{_VideoOffset = value;Utils.C(VideoOffsetChanged,VideoOffset);return true;})():false;}}

        private static string _StoryboardLayerDump;
        public static event PropertyChangedEventHandler StoryboardLayerDumpChanged;
        /// <summary>
        /// Dump for everything in events after break periods.
        /// </summary>
        public static string StoryboardLayerDump{get=>_StoryboardLayerDump;set{var _=_StoryboardLayerDump!=value?new Func<bool>(()=>{_StoryboardLayerDump=value;Utils.C(StoryboardLayerDumpChanged,StoryboardLayerDump);return true;})():false;}}
    }
}
