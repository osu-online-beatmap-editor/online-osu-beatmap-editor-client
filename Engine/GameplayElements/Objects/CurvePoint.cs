using online_osu_beatmap_editor_client.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Objects
{
    public class CurvePoint
    {
        public CurvePoint(int x, int y//, AnchorType anchorType
            ) 
        {
            X = x;
            Y = y;
            //Type = anchorType;
        }

        private int _X;
        public static event PropertyChangedEventHandler XChanged;
        public int X{get=>_X;set{var _=_X!=value?new Func<bool>(()=>{_X=value;Utils.C(XChanged,X);return true;})():false;}}

        private int _Y;
        public static event PropertyChangedEventHandler YChanged; 
        public int Y{get=>_Y;set{var _=_Y!=value?new Func<bool>(()=>{_Y=value;Utils.C(YChanged,Y);return true;})():false;}}

        //private AnchorType _Type;
        //public static event PropertyChangedEventHandler TypeChanged; 
        //public AnchorType Type{get=>_Type;set{var _=_Type!=value?new Func<bool>(()=>{_Type=value;Utils.C(TypeChanged,Type);return true;})():false;}}

        //public enum AnchorType
        //{
        //    GRAY = 0,
        //    RED = 1
        //}
    }
}
