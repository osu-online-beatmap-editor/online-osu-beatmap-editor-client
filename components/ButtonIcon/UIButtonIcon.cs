using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using System;

namespace online_osu_beatmap_editor_client.components.Button
{
    public class UIButtonIcon : ClickableUIObject
    {
        private Vector2f iconSize = new Vector2f(50, 50);
        private Vector2f buttonSize = new Vector2f(75, 75);

        private Texture iconTexture;
        private Sprite iconSprite;

        private RectangleShape buttonShape;

        public UIButtonIcon(string iconPath, int posX = 0, int posY = 0, bool isActive = false)
            : base(posX, posY, StyleVariables.colorBgTertiary, StyleVariables.colorPrimary, isActive)
        {
            this.GenerateBaseButtonShape();
            this.GenerateIcon(iconPath);

            width = (int)buttonSize.X;
            height = (int)buttonSize.Y;
        }

        private void GenerateBaseButtonShape()
        {
            buttonShape = new RectangleShape(buttonSize);
            buttonShape.Position = new Vector2f(posX, posY);
            buttonShape.FillColor = currentColor;
        }

        private void GenerateIcon(string iconPath)
        {
            iconTexture = new Texture(iconPath);
            iconSprite = new Sprite(iconTexture);
            iconSprite.Scale = new Vector2f(iconSize.X / iconTexture.Size.X, iconSize.Y / iconTexture.Size.Y);
            FloatRect iconBounds = iconSprite.GetLocalBounds();
            iconSprite.Origin = new Vector2f(iconBounds.Left + iconBounds.Width / 2, iconBounds.Top + iconBounds.Height / 2);
            iconSprite.Position = new Vector2f(posX + buttonShape.Size.X / 2, posY + buttonShape.Size.Y / 2);
        }

        public override void HandlePositionUpdate(int posX, int posY)
        {
            buttonShape.Position = new Vector2f(posX, posY);
            iconSprite.Position = new Vector2f(posX + buttonShape.Size.X / 2, posY + buttonShape.Size.Y / 2);
        }

        public override void Draw()
        {
            buttonShape.FillColor = currentColor;
            window.Draw(buttonShape);
            window.Draw(iconSprite);
        }
    }
}