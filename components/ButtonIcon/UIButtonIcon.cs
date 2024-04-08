using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using System;
using System.Runtime.InteropServices;

namespace online_osu_beatmap_editor_client.components.Button
{
    public class UIButtonIcon : ClickableUIObject
    {
        private Vector2f iconSize = new Vector2f(50, 50);
        private Vector2f buttonSize = new Vector2f(75, 75);

        private Texture iconTexture;
        private Sprite iconSprite;

        private RectangleShape buttonShape;

        public UIButtonIcon(string iconPath, [Optional]Vector2i pos)
            : base(pos, StyleVariables.colorBgTertiary, StyleVariables.colorPrimary)
        {
            GenerateBaseButtonShape();
            GenerateIcon(iconPath);

            size = new Vector2i((int)buttonSize.X, (int)buttonSize.Y);
        }

        private void GenerateBaseButtonShape()
        {
            buttonShape = new RectangleShape(buttonSize);
            buttonShape.Position = (Vector2f)pos;
            buttonShape.FillColor = currentColor;
        }

        private void GenerateIcon(string iconPath)
        {
            iconTexture = new Texture(iconPath);
            iconSprite = new Sprite(iconTexture);
            iconSprite.Scale = new Vector2f(iconSize.X / iconTexture.Size.X, iconSize.Y / iconTexture.Size.Y);
            FloatRect iconBounds = iconSprite.GetLocalBounds();
            iconSprite.Origin = new Vector2f(iconBounds.Left + iconBounds.Width / 2, iconBounds.Top + iconBounds.Height / 2);
            iconSprite.Position = new Vector2f(pos.X + buttonShape.Size.X / 2, pos.Y + buttonShape.Size.Y / 2);
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            base.HandlePositionUpdate(pos);
            buttonShape.Position = (Vector2f)pos;
            iconSprite.Position = new Vector2f(pos.X + buttonShape.Size.X / 2, pos.Y + buttonShape.Size.Y / 2);
        }

        public override void Draw()
        {
            buttonShape.FillColor = currentColor;
            window.Draw(buttonShape);
            window.Draw(iconSprite);
        }
    }
}