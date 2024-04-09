using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.views.Editor;
using SFML.Graphics;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.components.Background
{
    public class UIBackground : BaseUIComponent
    {
        private UIImage image;
        private RectangleShape backgoundDimRectangle;
        private float _backgroundDim;

        public float backgroundDim
        {
            get
            {
                return _backgroundDim;
            }
            set
            {
                if (value != _backgroundDim)
                {
                    _backgroundDim = value;
                    if (backgoundDimRectangle != null)
                    {
                        backgoundDimRectangle.FillColor = new Color(0, 0, 0, (byte)Convert.ToInt32(value * 255));
                    }
                }
            }
        }

        public UIBackground(string imagePath): base(new Vector2i(0, 0))
        {
            try { 
                image = new UIImage(imagePath, new Vector2i(0, 0), new Vector2i(853 * 2, 480 * 2));
                backgoundDimRectangle = new RectangleShape(new Vector2f(853 * 2, 480 * 2));
                backgoundDimRectangle.Position = new Vector2f(0, 0);
                backgroundDim = EditorData.backgroundDim;
            } catch { }

            EditorData.BackgroundDimChanged += (sender, e) => backgroundDim = EditorData.backgroundDim;
        }

        public override void Draw()
        {
            if (image != null)
            {
                image.Draw();
                window.Draw(backgoundDimRectangle);
            }
        }

        public override void Update()
        {
   
        }
    }
}
