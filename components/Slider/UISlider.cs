using online_osu_beatmap_editor_client.common;
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
                    ValueChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(sliderValue)));
                }
            }
        }

        public UISlider(Vector2i pos, int width) : base(pos)
        {
            size = new Vector2i(width, 20);

            track = new RectangleShape(new Vector2f(size.X, 20));
            track.FillColor = Color.White;
            track.Position = new Vector2f(pos.X, pos.Y);

            knob = new RectangleShape(new Vector2f(20, 20));
            knob.FillColor = Color.Blue;
            knob.Position = new Vector2f(pos.X + sliderValue * size.X, pos.Y);
        }

        public override void HandleSizeUpdate(Vector2i size)
        {
            if (track != null)
            {
                track.Size = new Vector2f(size.X, 20);
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
                track.Position = new Vector2f(pos.X, pos.Y);
            }
            if (knob != null)
            {
                knob.Position = new Vector2f(pos.X + sliderValue * size.X - 10, pos.Y);
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
                    knob.Position = new Vector2f(pos.X + sliderValue * size.X - 10, pos.Y);
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