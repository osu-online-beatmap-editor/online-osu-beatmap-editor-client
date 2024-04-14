using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.ComponentModel;

namespace online_osu_beatmap_editor_client.components.Slider
{
    public class UISlider : BaseUIComponent
    {
        private RectangleShape track;
        private RectangleShape knob;
        private float _sliderValue = 0.5f;

        public event PropertyChangedEventHandler ValueChanged;
        public float sliderValue
        {
            get { return _sliderValue; }
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    if (knob != null) {
                        knob.Position = new Vector2f(pos.X + sliderValue * size.X - 10, pos.Y + size.Y / 4 - 5);
                    }
                }
            }
        }

        public UISlider(Vector2i pos, int width, int height = 20) : base(pos)
        {
            size = new Vector2i(width, height);

            track = new RectangleShape(new Vector2f(size.X, 10));
            track.FillColor = StyleVariables.colorBgTertiary;
            track.Position = new Vector2f(pos.X, pos.Y + size.Y / 4);

            knob = new RectangleShape(new Vector2f(20, 20));
            knob.FillColor = StyleVariables.colorPrimary;
            knob.Position = new Vector2f(pos.X + sliderValue * size.X, pos.Y + size.Y / 4 - 5);
        }

        public override void HandleSizeUpdate(Vector2i size)
        {
            if (track != null)
            {
                track.Size = new Vector2f(size.X, 20);
                track.Position = new Vector2f(-size.X, -size.Y);
            }
            if (knob != null)
            {
                knob.Size = new Vector2f(20, 20);
            }
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            if (track != null)
            {
                track.Position = new Vector2f(pos.X, pos.Y + size.Y / 4);
            }
            if (knob != null)
            {
                knob.Position = new Vector2f(pos.X + sliderValue * size.X - 10, pos.Y + size.Y / 4 - 5);
            }
        }

        public override void Update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(window);
                if (mousePos.X >= pos.X && mousePos.X <= pos.X + size.X &&
                    mousePos.Y >= pos.Y && mousePos.Y <= pos.Y + size.Y)
                {
                    sliderValue = (mousePos.X - pos.X) / (float)size.X;
                    knob.Position = new Vector2f(pos.X + sliderValue * size.X - 10, pos.Y + size.Y / 4 - 5);
                    ValueChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(sliderValue)));
                }
            }
        }

        public override void Draw()
        {
            window.Draw(track);
            window.Draw(knob);
        }
    }
}