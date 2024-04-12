﻿using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.Engine;
using online_osu_beatmap_editor_client.views.Editor;
using SFML.Graphics;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.components
{
    public class HitCircle : BaseUIComponent
    {
        public int id;
        public int StartTime;
        private SelectionOutline selectionOutline;
        private UIImage backgrond;
        private UIText numberText;
        private UIImage approachCircle;
        public bool isSelected = false;

        private float maxApproachCircleSizeMultiplier = 2f;

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
                    if (backgrond != null)
                    {
                        backgrond.color = value;
                    }
                    if (approachCircle != null)
                    {
                        approachCircle.color = value;
                    }
                }
            }
        }

        public HitCircle(Vector2i pos, int number, float CS, Color color) : base(pos)
        {
            this.color = color;
            int size = (int)(OsuMath.GetCircleWidthByCS(CS) * 2.3f);
            this.size = new Vector2i(size, size);

            float fontSize = size / 2.5f;
            backgrond = new UIImage("assets/baseSkin/hitcircle.png", pos, this.size, color);
            backgrond.origin = new Vector2f(0.5f, 0.5f);
            numberText = new UIText(number.ToString(), pos, (uint)fontSize);
            numberText.origin = new Vector2f(0.5f, 0.5f);
            approachCircle = new UIImage("assets/baseSkin/approachcircle.png", pos, this.size, color);
            approachCircle.origin = new Vector2f(0.5f, 0.5f);

            selectionOutline = new SelectionOutline(pos, this.size);
        }

        public override void Update()
        {
            int minTime = (int)(StartTime - OsuMath.CalculateHitObjectDuration(EditorData.AR));
            int maxTime = StartTime;
            int size = (int)OsuMath.RemapNumbers(EditorData.currentTime, minTime, maxTime, this.size.X * maxApproachCircleSizeMultiplier, this.size.X);
            int opacity = (int)OsuMath.RemapNumbers(EditorData.currentTime, minTime, maxTime, 0, 255);
            approachCircle.size = new Vector2i(size, size);

            color = new Color(color.R, color.G, color.B, (byte)opacity);
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
            if (approachCircle != null)
            {
                approachCircle.pos = pos;
            }
        }

        public override void Draw()
        {
            backgrond.Draw();
            numberText.Draw();
            approachCircle.Draw();

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