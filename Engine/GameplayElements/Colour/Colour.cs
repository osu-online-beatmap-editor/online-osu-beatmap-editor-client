using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Color
{
    public class Colour(int red = 0, int green = 0, int blue = 0, bool sliderOverride = false)
    {
        private static int _ID = 0;
        public int ID { get; private set; } = sliderOverride ? -1 : ++_ID;
        public int Red { get; private set; } = OsuMath.Clamp(red, 0, 255);
        public int Green { get; private set; } = OsuMath.Clamp(green, 0, 255);
        public int Blue { get; private set; } = OsuMath.Clamp(blue, 0, 255);
    }
}
