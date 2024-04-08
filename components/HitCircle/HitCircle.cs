using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.Engine;

namespace online_osu_beatmap_editor_client.components
{
    public class HitCircle : BaseUIComponent
    {
        private UIImage backgrond;
        private UIText numberText;

        public HitCircle(int posX, int posY, int number, float CS) : base(posX, posY)
        {
            int size = (int)(OsuMath.GetCircleWidthByCS(CS) * 2.3f);

            this.width = size;
            this.height = size;

            backgrond = new UIImage("assets/baseSkin/hitcircle.png", posX, posY, size, size);
            numberText = new UIText(number.ToString(), posX, posY + (int)(5 * 2.3f), (uint)(size / 2.5f));
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            backgrond.Draw();
            numberText.Draw();
        }
    }
}