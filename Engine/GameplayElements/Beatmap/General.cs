using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class General
    {
        //always "osu file format v14" i think but it's here just in case
        public static string gameFileVersion;

        //[General]
        public static string AudioFilename;
        public static int AudioLeadIn;
        public static int PreviewTime;
        public static int Countdown;
        public static string SampleSet;
        public static int StackLeniency;
        public static int Mode;
        public static int LetterboxInBreaks;
        public static int WidescreenStoryboard;
    }
}
