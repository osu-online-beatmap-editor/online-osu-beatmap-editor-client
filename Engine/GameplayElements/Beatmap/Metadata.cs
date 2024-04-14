using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class Metadata
    {
        //Metadata
        private static string _Title;
        public static event PropertyChangedEventHandler TitleChanged;
        public static string Title{get=>_Title;set{var _=_Title!=value?new Func<bool>(()=>{_Title=value;Utils.C(TitleChanged,Title);return true;})():false;}}

        private static string _TitleUnicode;
        public static event PropertyChangedEventHandler TitleUnicodeChanged;
        public static string TitleUnicode{get=>_TitleUnicode;set{var _=_TitleUnicode!=value?new Func<bool>(()=>{_TitleUnicode=value;Utils.C(TitleUnicodeChanged,TitleUnicode);return true;})():false;}}
        
        private static string _Artist;
        public static event PropertyChangedEventHandler ArtistChanged;
        public static string Artist{get=>_Artist;set{var _=_Artist!=value?new Func<bool>(()=>{_Artist=value;Utils.C(ArtistChanged,Artist);return true;})():false;}}

        private static string _ArtistUnicode;
        public static event PropertyChangedEventHandler ArtistUnicodeChanged;
        public static string ArtistUnicode{get=>_ArtistUnicode;set{var _=_ArtistUnicode!=value?new Func<bool>(()=>{_ArtistUnicode=value;Utils.C(ArtistUnicodeChanged,ArtistUnicode);return true;})():false;}}

        private static string _Creator;
        public static event PropertyChangedEventHandler CreatorChanged;
        public static string Creator{get=>_Creator;set{var _=_Creator!=value?new Func<bool>(()=>{_Creator=value;Utils.C(CreatorChanged,Creator);return true;})():false;}}

        private static string _Version;
        public static event PropertyChangedEventHandler VersionChanged;
        public static string Version{get=>_Version;set{var _=_Version!=value?new Func<bool>(()=>{_Version=value;Utils.C(VersionChanged,Version);return true;})():false;}}

        private static string _Source;
        public static event PropertyChangedEventHandler SourceChanged;
        public static string Source{get=>_Source;set{var _=_Source!=value?new Func<bool>(()=>{_Source=value;Utils.C(SourceChanged,Source);return true;})():false;}}

        private static string _Tags;
        public static event PropertyChangedEventHandler TagsChanged;
        public static string Tags{get=>_Tags;set{var _=_Tags!=value?new Func<bool>(()=>{_Tags=value;Utils.C(TagsChanged,Tags);return true;})():false;}}

        private static int _BeatmapID;
        public static event PropertyChangedEventHandler BeatmapIDChanged;
        public static int BeatmapID{get=>_BeatmapID;set{var _=_BeatmapID!=value?new Func<bool>(()=>{_BeatmapID=value;Utils.C(BeatmapIDChanged,BeatmapID);return true;})():false;}}

        private static int _BeatmapSetID;
        public static event PropertyChangedEventHandler BeatmapSetIDChanged;
        public static int BeatmapSetID{get=>_BeatmapSetID;set{var _=_BeatmapSetID!=value?new Func<bool>(()=>{_BeatmapSetID=value;Utils.C(BeatmapSetIDChanged,BeatmapSetID);return true;})():false;}}
    }
}
