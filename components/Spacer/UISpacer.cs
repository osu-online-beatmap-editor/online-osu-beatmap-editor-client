using online_osu_beatmap_editor_client.common;

namespace online_osu_beatmap_editor_client.components
{
    public class UISpacer : BaseUIComponent
    {
        public UISpacer(int posX, int posY, int width, int height) : base(posX, posY)
        {
            this.width = width;
            this.height = height;   
        }
        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}