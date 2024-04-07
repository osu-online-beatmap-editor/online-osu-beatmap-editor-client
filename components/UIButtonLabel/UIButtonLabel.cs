using SFML.Graphics;
using SFML.System;
using online_osu_beatmap_editor_client.config;

namespace online_osu_beatmap_editor_client.components.Button
{
    public class UIButtonLabel : ClickableUIObject
    {
        private int paddingX = 10;

        private Text buttonText;
        private RectangleShape buttonShape;

        private string label;

        public UIButtonLabel(string label, int posX = 0, int posY = 0) : base(posX, posY, StyleVariables.colorBgTertiary, StyleVariables.colorPrimary)
        {
            this.label = label;

            GenerateButtonText();
            GenerateButtonShape();

            width = (int)buttonShape.Size.X;
            height = (int)buttonShape.Size.Y;
        }

        private void GenerateButtonShape()
        {
            buttonShape = new RectangleShape(new Vector2f(width, height));
            buttonShape.Position = new Vector2f(posX, posY);
            buttonShape.FillColor = currentColor;

            int labelWidth = (int)buttonText.GetLocalBounds().Width + paddingX * 2;
            buttonShape.Size = new Vector2f(labelWidth + paddingX * 2, 50);
        }

        private void GenerateButtonText()
        {
            buttonText = new Text(label, StyleVariables.mainFont);
            buttonText.CharacterSize = 20;
            buttonText.FillColor = Color.White;
            buttonText.Origin = new Vector2f(buttonText.GetGlobalBounds().Width / 2, buttonText.GetGlobalBounds().Height / 2 + 5); //@TODO Find out why it is not centered without the +5.
        }

        public override void HandlePositionUpdate(int posX, int posY)
        {
            buttonShape.Position = new Vector2f(posX, posY);
            buttonText.Position = new Vector2f(posX + width / 2, posY + height / 2);
        }

        public override void Draw()
        {
            buttonShape.FillColor = currentColor;
            window.Draw(buttonShape);
            window.Draw(buttonText);
        }
    }
}
