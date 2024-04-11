using online_osu_beatmap_editor_client.Engine.GameplayElements;
using online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap;
using online_osu_beatmap_editor_client.gameplay_elements.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace online_osu_beatmap_editor_client.Engine.Beatmap
{
    public static class BeatmapParser
    {
        public static void ParseBeatmap(string[] lines)
        {
            BeatmapData.general.gameFileVersion = lines[0];

            IEnumerable<Action<string[]>> Segments()
            {
                yield return (_)=>{ };
                yield return General;
                yield return Editor;
                yield return Metadata;
                yield return Difficulty;
                yield return Events;
                yield return TimingPoints;
                yield return Colours;
                yield return HitObjects;
            }
            IEnumerator<Action<string[]>> enumerator = Segments().GetEnumerator();
            List<string> segment = new();
            for (int i=3; i< lines.Length; i++)
            {
                string l = lines[i].Trim().TrimEnd('\r','\n');

                if (l == "")
                {
                    enumerator.Current([.. segment]);
                    segment.Clear();
                    enumerator.MoveNext();
                }
                else
                {
                    segment.Add(l);
                }
            }
        }

        private static void General(string[] lines)
        { 
            char delimiter = ',';
            int i=0;
            string[] l = lines[i].Split(':');
            BeatmapData.general.AudioFileName = l[i++];
            BeatmapData.general.AudioLeadIn = int.Parse(l[i++]);
            BeatmapData.general.PreviewTime = int.Parse(l[i++]);
            BeatmapData.general.Countdown = int.Parse(l[i++]);
            BeatmapData.general.SampleSet = l[i++];
            BeatmapData.general.StackLeniency = int.Parse(l[i++]);
            BeatmapData.general.Mode = int.Parse(l[i++]);
            BeatmapData.general.LetterboxInBreaks = int.Parse(l[i++]);
            BeatmapData.general.WidescreenStoryboard = int.Parse(l[i++]);
            
        }

        private static void Editor(string[] lines)
        { 
            
        }

        private static void Metadata(string[] lines)
        { 
            
        }

        private static void Difficulty(string[] lines)
        { 
            
        }

        private static void Events(string[] lines)
        { 
            
        }

        private static void TimingPoints(string[] lines)
        { 
            
        }

        private static void Colours(string[] lines)
        { 
            
        }

        private static void HitObjects(string[] lines)
        { 
            
        }

        public static T ConvertValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
