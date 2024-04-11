using online_osu_beatmap_editor_client.gameplay_elements.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_osu_beatmap_editor_client.gameplay_elements.Objects
{
    public class HitCircle
    {
        /// <summary>
        /// The time at which the HitObject starts.
        /// </summary>
        public double StartTime
        {
            get => StartTime;
            set => StartTime = value;
        }

        /// <summary>
        /// The samples to be played when this hit object is hit.
        /// <para>
        /// In the case of <see cref="IHasRepeats"/> types, this is the sample of the curve body
        /// and can be treated as the default samples for the hit object.
        /// </para>
        /// </summary>
        public List<HitSample> Samples
        {
            get => Samples;
            set
            {
                Samples.Clear();
                Samples.AddRange(value);
            }
        }
    }
}
