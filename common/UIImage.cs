using SFML.Graphics;
using SFML.System;
using System;
using System.Runtime.InteropServices;

namespace online_osu_beatmap_editor_client.common
{
    public class UIImage : BaseUIComponent
    {
        private Sprite sprite;
        private Texture texture;
        private float _opacity;

        public float opacity
        {
            get { return _opacity; }
            set
            {
                int opacity = (int)(value * 255);
                sprite.Color = new Color(255, 255, 255, (byte)opacity);
            }
        }

        public UIImage(string imagePath, int posX, int posY, [Optional]int width, [Optional] int height, float opacity = 1)
            : base(posX, posY)
        {
            texture = new Texture(imagePath);
            this.width = (int)texture.Size.X;
            this.height = (int)texture.Size.Y;

            if (width != 0)
            {
                this.width = width;
            }

            if (height != 0) {
                this.height = height;
            }

            sprite = new Sprite(texture);
            sprite.Scale = new Vector2f((float)this.width / texture.Size.X, (float)this.height / texture.Size.Y);
            sprite.Position = new Vector2f(posX, posY);
            this.opacity = opacity;
        }

        public override void Draw()
        {
            window.Draw(sprite);
        }

        public override void Update()
        {
            // Implement if needed
        }
    }
}
