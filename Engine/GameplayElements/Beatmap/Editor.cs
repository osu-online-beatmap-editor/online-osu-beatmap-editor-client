using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public class Editor
    {
        private Dictionary<int,int> _Bookmarks;
        public static event PropertyChangedEventHandler BookmarksChanged;
        public Dictionary<int,int> Bookmarks{get=>_Bookmarks;set{var _=_Bookmarks!=value?new Func<bool>(()=>{_Bookmarks=value;Utils.C(BookmarksChanged,Bookmarks);return true;})():false;}}

        private float _DistanceSpacing;
        public static event PropertyChangedEventHandler DistanceSpacingChanged;
        public float DistanceSpacing{get=>_DistanceSpacing;set{var _=_DistanceSpacing!=value?new Func<bool>(()=>{_DistanceSpacing = (float)Math.Round(value, 1);Utils.C(DistanceSpacingChanged,DistanceSpacing);return true;})():false;}}

        private int _BeatDivisor;
        public static event PropertyChangedEventHandler BeatDivisorChanged;
        public int BeatDivisor{get=>_BeatDivisor;set{var _=_BeatDivisor!=value?new Func<bool>(()=>{_BeatDivisor=value;Utils.C(BeatDivisorChanged,BeatDivisor);return true;})():false;}}

        private int _GridSize;
        public static event PropertyChangedEventHandler GridSizeChanged;
        public int GridSize{get=>_GridSize;set{var _=_GridSize!=value?new Func<bool>(()=>{_GridSize=value;Utils.C(GridSizeChanged,GridSize);return true;})():false;}}

        private float _TimelineZoom;
        public static event PropertyChangedEventHandler TimelineZoomChanged;
        public float TimelineZoom{get=>_TimelineZoom;set{var _=_TimelineZoom!=value?new Func<bool>(()=>{_TimelineZoom=value;Utils.C(TimelineZoomChanged,TimelineZoom);return true;})():false;}}
    }
}
