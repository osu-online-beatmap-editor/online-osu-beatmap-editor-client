using online_osu_beatmap_editor_client.common;
using SFML.System;

namespace online_osu_beatmap_editor_client.components
{
    public class UISpacer : BaseUIComponent
    {
        public UISpacer(Vector2i pos, Vector2i size) : base(pos)
        {
            this.size = size; 
        }
        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}