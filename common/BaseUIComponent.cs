using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.common
{
    public abstract class BaseUIComponent
    {
        protected int posX;
        protected int posY;

        protected static RenderWindow window; 

        public static void SetWindow(RenderWindow _window)
        {
            window = _window;
        }

        public BaseUIComponent(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }


        public abstract void Update();

        public abstract void Draw();
    }
}