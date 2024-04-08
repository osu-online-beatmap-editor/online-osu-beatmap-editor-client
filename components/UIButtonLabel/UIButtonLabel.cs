using SFML.Graphics;
using SFML.System;
using online_osu_beatmap_editor_client.config;
using System.Runtime.InteropServices;
using System;
using online_osu_beatmap_editor_client.common;

namespace online_osu_beatmap_editor_client.components.Button
{
    public class UIButtonLabel : ClickableUIObject
    {
        private int paddingX = 3;

        private UIText buttonText;
        private RectangleShape buttonShape;

        private string label;

        public UIButtonLabel(string label, [Optional]Vector2i pos) : base(pos, StyleVariables.colorBgTertiary, StyleVariables.colorPrimary)
        {
            this.label = label;

            GenerateButtonText();
            GenerateButtonShape();

            this.size = new Vector2i((int)buttonShape.Size.X, (int)buttonShape.Size.Y);            
        }

        private void GenerateButtonShape()
        {
            buttonShape = new RectangleShape(new Vector2f(size.X, size.Y));
            buttonShape.Position = new Vector2f(pos.X, pos.Y);
            buttonShape.FillColor = currentColor;

            int labelWidth = (int)buttonText.size.X + paddingX * 2;
            buttonShape.Size = new Vector2f(labelWidth + paddingX * 2, 15);
        }

        private void GenerateButtonText()
        {
            buttonText = new UIText(label, pos);
            buttonText.origin = new Vector2f(0.5f, 0.5f);
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            buttonShape.Position = (Vector2f)pos;
            buttonText.pos = new Vector2i(pos.X + size.X / 2, pos.Y + size.Y / 2 - 5);
        }

        public override void HandleSizeUpdate(Vector2i size)
        {
            base.HandleSizeUpdate(size);
            buttonText.pos = new Vector2i(pos.X + size.X / 2, pos.Y + size.Y / 2 - 5);
        }

        public override void Draw()
        {
            buttonShape.FillColor = currentColor;
            window.Draw(buttonShape);
            buttonText.Draw();
        }
    }
}
