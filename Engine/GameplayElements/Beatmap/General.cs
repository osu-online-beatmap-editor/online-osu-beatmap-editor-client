using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public class General
    {
        //always "osu file format v14" i think but it's here just in case
        public string gameFileVersion;

        //[General]
        public string AudioFilename;
        public int AudioLeadIn;
        public int PreviewTime;
        public int Countdown;
        public string SampleSet;
        public int StackLeniency;
        public int Mode;
        public int LetterboxInBreaks;
        public int WidescreenStoryboard;
    }
}
