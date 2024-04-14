using online_osu_beatmap_editor_client.common;
using SFML.System;
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
        public CurvePoint(Vector2i position//, AnchorType anchorType
            ) 
        {
            Position = position;
            //Type = anchorType;
        }

        private Vector2i _Position;
        public static event PropertyChangedEventHandler PositionChanged;
        public Vector2i Position{get=>_Position;set{var _=_Position!=value?new Func<bool>(()=>{_Position=value;Utils.C(PositionChanged,Position);return true;})():false;}}

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
