using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.common
{
    public static class Utils
    {
        /// <summary>
        /// Call to invoke a propchange event.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public static void C(PropertyChangedEventHandler arg1, object arg2)
        {
            arg1?.Invoke(null,new PropertyChangedEventArgs(nameof(arg2)));
        }
    }
}
