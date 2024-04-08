using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.common
{
    public abstract class BaseUIComponent
    {
        private Vector2i _pos;
        private Vector2i _size;
        private Vector2f _origin = new Vector2f(0, 0);

        public Vector2i pos
        {
            get { return _pos; }
            set
            {
                if (value != _pos)
                {
                    _pos = value;
                    HandlePositionUpdate(_pos);
                }
            }
        }

        public Vector2i size
        {
            get { return _size; }
            set
            {
                if (value != _size)
                {
                    _size = value;
                    HandleSizeUpdate(_size);
                }
            }
        }

        public Vector2f origin
        {
            get { return _origin; }
            set
            {
                if (value != _origin)
                {
                    _origin = value;
                    HandleOriginUpdate(_origin);
                }
            }
        }

        protected static RenderWindow window;

        public static void SetWindow(RenderWindow _window)
        {
            window = _window;
        }

        public BaseUIComponent(Vector2i pos)
        {
            this.pos = new Vector2i(0, 0);
            this.pos = pos;
        }

        public virtual void HandlePositionUpdate(Vector2i pos) { }
        public virtual void HandleSizeUpdate(Vector2i size) { }
        public virtual void HandleOriginUpdate(Vector2f origin) { }

        public abstract void Update();

        public abstract void Draw();
    }
}