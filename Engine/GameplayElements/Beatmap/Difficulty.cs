using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.Engine.GameplayElements
{
    public class Difficulty
    {
        //Difficulty
        public float HPDrainRate
        {
            get => HPDrainRate;
            set => HPDrainRate = (float)Math.Round(value, 2);
        }
        public float CircleSize 
        {
            get => CircleSize;
            set => CircleSize = (float)Math.Round(value, 2);
        }
        public float OverallDifficulty 
        {
            get => OverallDifficulty;
            set => OverallDifficulty = (float)Math.Round(value, 2);
        }
        public float ApproachRate
        {
            get => ApproachRate;
            set => ApproachRate = (float)Math.Round(value, 2);
        }
        public float SliderMultiplier
        {
            get => SliderMultiplier;
            set => SliderMultiplier = (float)Math.Round(value, 1);
        }
        public float SliderTickRate
        {
            get => SliderTickRate;
            set => SliderTickRate = (float)Math.Round(value, 1);
        }
    }
}
