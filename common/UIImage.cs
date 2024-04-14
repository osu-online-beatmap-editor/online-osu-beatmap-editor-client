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

        public UIImage(string imagePath, Vector2i pos, [Optional]Vector2i size, [Optional]Color color)
            : base(pos)
        {
            texture = new Texture(imagePath);

            this.size = new Vector2i((int)texture.Size.X, (int)texture.Size.Y);
            if (size != null)
            {
                this.size = size;
            }

            sprite = new Sprite(texture);
            sprite.Scale = new Vector2f((float)this.size.X / texture.Size.X, (float)this.size.Y / texture.Size.Y);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width * this.origin.X, sprite.GetLocalBounds().Height * this.origin.Y);
            sprite.Position = (Vector2f)pos;

            this.color = color;

        }

        public override void HandleSizeUpdate(Vector2i size)
        {
            base.HandleSizeUpdate(size);
            if (sprite != null)
            {
                sprite.Scale = new Vector2f((float)size.X / texture.Size.X, (float)size.Y / texture.Size.Y);
                sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width * this.origin.X, sprite.GetLocalBounds().Height * this.origin.Y);
            }
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            if (sprite != null)
            {
                sprite.Position = (Vector2f)pos;
            }
        }

        public override void HandleOriginUpdate(Vector2f origin)
        {
            base.HandleOriginUpdate(origin);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width * this.origin.X, sprite.GetLocalBounds().Height * this.origin.Y);
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
