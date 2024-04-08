using SFML.Graphics;
using SFML.System;
using System.Runtime.InteropServices;

namespace online_osu_beatmap_editor_client.common
{
    public class UIImage : BaseUIComponent
    {
        private Sprite sprite;
        private Texture texture;
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
                    sprite.Color = value;
                }
            }
        }

        public UIImage(string imagePath, int posX, int posY, [Optional]int width, [Optional] int height, [Optional]Color color)
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
            sprite.Position = new Vector2f(posX - width / 2, posY - width / 2);

            this.color = color;

        }

        public override void Draw()
        {
            window.Draw(sprite);
        }

        public override void Update()
        {

        }
    }
}
