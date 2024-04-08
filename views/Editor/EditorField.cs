using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorField : BaseUIComponent
    {
        private Vector2i baseEditorFielSize = new Vector2i(512, 384); 
        private RectangleShape fieldShape;
        private Color fieldColor = Color.White;

        public EditorField(int posX, int posY) : base(posX, posY)
        {
            this.width = (int)(baseEditorFielSize.X * 2.3f);
            this.height = (int)(baseEditorFielSize.Y * 2.3f);

            fieldShape = new RectangleShape(new Vector2f(width, height));
            fieldShape.Origin = new Vector2f(width / 2, height / 2); 
            fieldShape.Position = new Vector2f(posX, posY);
            fieldShape.FillColor = fieldColor;
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            window.Draw(fieldShape);
        }
    }
}
