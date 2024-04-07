using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.components
{
    public class HitCircle : BaseUIComponent
    {
        private CircleShape circle;

        public HitCircle(int posX, int posY, float radius) : base(posX, posY)
        {
            circle = new CircleShape(radius);
            circle.Origin = new Vector2f(radius, radius);
            circle.FillColor = Color.White;
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            circle.Position = new Vector2f(posX, posY);
            window.Draw(circle);
        }
    }
}