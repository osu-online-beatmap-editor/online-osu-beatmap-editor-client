using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.Engine;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.components
{
    public class HitCircle : BaseUIComponent
    {
        public int id;
        public int StartTime;
        private SelectionOutline selectionOutline;
        private UIImage backgrond;
        private UIText numberText;
        public bool isSelected = false;

        private int _number;

        public int number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value != _number)
                {
                    _number = value;
                    numberText.label = value.ToString();
                }
            }
        }

        private Color _color;
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    backgrond.color = value;
                }
            }
        }

        public HitCircle(Vector2i pos, int number, float CS, Color color) : base(pos)
        {
            int size = (int)(OsuMath.GetCircleWidthByCS(CS) * 2.3f);
            this.size = new Vector2i(size, size);

            float fontSize = size / 2.5f;
            backgrond = new UIImage("assets/baseSkin/hitcircle.png", pos, this.size, color);
            backgrond.origin = new Vector2f(0.5f, 0.5f);
            numberText = new UIText(number.ToString(), pos, (uint)fontSize);
            numberText.origin = new Vector2f(0.5f, 0.5f);

            selectionOutline = new SelectionOutline(pos, this.size);
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
            if (selectionOutline != null)
            {
                selectionOutline.pos = pos;
            }
        }

        public override void Draw()
        {
            backgrond.Draw();
            numberText.Draw();

            if (isSelected)
            {
                selectionOutline.Draw();
            }
        }

        public bool IsMouseOver(Vector2i mousePosition)
        {
            bool isInsideX = mousePosition.X >= pos.X - size.X / 2 && mousePosition.X <= pos.X + size.X - size.X / 2;
            bool isInsideY = mousePosition.Y >= pos.Y - size.Y / 2 && mousePosition.Y <= pos.Y + size.Y - size.Y / 2;

            return isInsideX && isInsideY;
        }
    }
}