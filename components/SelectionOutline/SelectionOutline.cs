
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.components
{
    public class SelectionOutline : BaseUIComponent
    {
        private RectangleShape border;

        public SelectionOutline(Vector2i pos, Vector2i size) : base(pos)
        {
            this.size = size;

            border = new RectangleShape((Vector2f)size);
            border.Origin = new Vector2f(size.X / 2, size.Y / 2);
            border.Position = (Vector2f)pos;
            border.FillColor = Color.Transparent;
            border.OutlineColor = StyleVariables.colorPrimary;
            border.OutlineThickness = StyleVariables.selectionOutlineThickness;
        }

        public override void HandleSizeUpdate(Vector2i size)
        {
            base.HandleSizeUpdate(size);
            if (border != null )
            {
                border.Size = new Vector2f(size.X, size.Y);
                border.Origin = new Vector2f(size.X * origin.X, size.Y * origin.Y);
            }
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            if (border != null)
            {
                border.Position = (Vector2f)pos;
            }
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            window.Draw(border);
        }
    }
}
