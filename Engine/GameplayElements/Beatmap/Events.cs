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
    public class Events
    {
        //Background and Video events
        /// <summary>
        /// Set with SetBackground();
        /// </summary>
        public string Background { get; private set; }
        /// <summary>
        /// Sets the background.
        /// </summary>
        /// <param name="fileName">The name of the file to set as background. (Allowed formats: png,jpg,?)</param>
        /// <param name="offsetX">Offset in osu! pixels from the centre of the screen.</param>
        /// <param name="offsetY">Offset in osu! pixels from the centre of the screen.</param>
        public void SetBackground(string fileName, int offsetX = 0, int offsetY = 0)
        {
            Background = $"0,0,{fileName},{offsetX},{offsetY}";
        }


        /// <summary>
        /// Set with SetVideo();
        /// </summary>
        public string Video { get; private set; }
        /// <summary>
        /// Sets the video string to the correct format, including offset.
        /// </summary>
        /// <param name="fileName">The name of the file to set as video. (Allowed formats: mp4,flv,?)</param>
        /// <param name="offset">The offset when the video should start playing. Can be negative.</param>
        public void SetVideo(string fileName, int offset = 0)
        {
            Video = $"Video,{offset},{fileName}";
        }


        /// <summary>
        /// Set with AddBreakPeriod();
        /// </summary>
        public List<string> BreakPeriods { get; private set; } = new();
        /// <summary>
        /// Adds break period.
        /// </summary>
        /// <param name="start">Position in miliseconds when the break starts.</param>
        /// <param name="end">Position in miliseconds when the break ends.</param>
        public void AddBreakPeriod(int start, int end) 
        { 
            BreakPeriods.Add($"Break,{start},{end}"); 
        }

        private string _StoryboardLayerDump;
        public static event PropertyChangedEventHandler StoryboardLayerDumpChanged;
        /// <summary>
        /// Dump for everything in events after break periods.
        /// </summary>
        public string StoryboardLayerDump{get=>_StoryboardLayerDump;set{var _=_StoryboardLayerDump!=value?new Func<bool>(()=>{_StoryboardLayerDump=value;Utils.C(StoryboardLayerDumpChanged,StoryboardLayerDump);return true;})():false;}}
    }
}
