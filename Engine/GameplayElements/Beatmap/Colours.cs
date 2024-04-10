using online_osu_beatmap_editor_client.Engine.GameplayElements.Color;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements.Beatmap
{
    public static class Colours
    {
        public static List<Colour> colours { get; private set; }
        public static void AddTimingPoint(Colour colour)
        {
            (colours ??= new()).Add(colour);
        }

        public static Colour SliderBorder { get; private set; }
        public static void AddSliderBorderColour(int red = 0, int green = 0, int blue = 0)
        {
            SliderBorder = new(red,green,blue,true);
        }

        public static Colour SliderTrackOverride { get; private set; }
        public static void AddSliderTrackOverride(int red = 0, int green = 0, int blue = 0)
        {
            SliderTrackOverride = new(red, green, blue, true);
        }
    }
}
