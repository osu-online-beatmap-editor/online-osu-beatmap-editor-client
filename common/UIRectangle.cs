using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.common
{
    public class UIRectangle : BaseUIComponent
    {
        private RectangleShape rectangleShape;

        public UIRectangle(Vector2i pos, Vector2i size, Color color) : base(pos)
        {
            this.size = size;
            rectangleShape = new RectangleShape(new Vector2f(size.X, size.Y));
            rectangleShape.Position = new Vector2f(pos.X, pos.Y);
            rectangleShape.FillColor = color; 
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            if (rectangleShape != null) {
                rectangleShape.Position = new Vector2f(pos.X, pos.Y);
            }
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            window.Draw(rectangleShape);
        }
    }
}
