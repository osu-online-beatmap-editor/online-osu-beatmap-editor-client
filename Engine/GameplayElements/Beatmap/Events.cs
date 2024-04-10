using SFML.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class Events
    {
        //Background and Video events
        public static string Background { get; private set; }
        /// <summary>
        /// Sets the background.
        /// </summary>
        /// <param name="fileName">The name of the file to set as background. (Allowed formats: png,jpg,?)</param>
        /// <param name="offsetX">Offset in osu! pixels from the centre of the screen.</param>
        /// <param name="offsetY">Offset in osu! pixels from the centre of the screen.</param>
        public static void SetBackground(string fileName, int offsetX = 0, int offsetY = 0)
        {
            Background = $"0,0,\"{fileName}\",{offsetX},{offsetY}";
        }


        public static string Video { get; private set; }
        /// <summary>
        /// Sets the video string to the correct format, including offset.
        /// </summary>
        /// <param name="fileName">The name of the file to set as video. (Allowed formats: mp4,?)</param>
        /// <param name="offset">The offset when the video should start playing. Can be negative.</param>
        public static void SetVideo(string fileName, int offset = 0)
        {
            Video = $"Video,{offset}\",{fileName}\"";
        }


        public static List<string> BreakPeriods { get; private set; } = new();
        /// <summary>
        /// Adds break period.
        /// </summary>
        /// <param name="start">Position in miliseconds when the break starts.</param>
        /// <param name="end">Position in miliseconds when the break ends.</param>
        public static void AddBreakPeriod(int start, int end) 
        { 
            BreakPeriods.Add($"Break,{start},{end}"); 
        }
    }
}
