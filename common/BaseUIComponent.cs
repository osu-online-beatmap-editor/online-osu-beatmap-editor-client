using SFML.Graphics;

namespace online_osu_beatmap_editor_client.common
{
    public abstract class BaseUIComponent
    {
        public int posX;
        public int posY;
        public int width;
        public int height;

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

        public void SetPosition(int posX, int poxY)
        {
            this.posX = posX;
            this.posY = poxY;
            this.HandlePositionUpdate(posX, poxY);
        }

        public virtual void HandlePositionUpdate(int posX, int poxY) { }

        public abstract void Update();

        public abstract void Draw();
    }
}