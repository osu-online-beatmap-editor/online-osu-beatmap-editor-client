using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.Engine;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.components
{
    public class HitCircle : BaseUIComponent
    {
        private UIImage backgrond;
        private UIText numberText;

        public HitCircle(Vector2i pos, int number, float CS, Color color) : base(pos)
        {
            int size = (int)(OsuMath.GetCircleWidthByCS(CS) * 2.3f);
            this.size = new Vector2i(size, size);

            float fontSize = size / 2.5f;
            backgrond = new UIImage("assets/baseSkin/hitcircle.png", pos, this.size, color);
            backgrond.origin = new Vector2f(0.5f, 0.5f);
            numberText = new UIText(number.ToString(), pos, (uint)fontSize);
            numberText.origin = new Vector2f(0.5f, 0.5f);
        }

        public override void Update()
        {

        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            if (backgrond != null)
            {
                backgrond.pos = pos;
            }
            if (numberText != null)
            {
                numberText.pos = pos;
            }
        }

        public override void Draw()
        {
            backgrond.Draw();
            numberText.Draw();
        }
    }
}