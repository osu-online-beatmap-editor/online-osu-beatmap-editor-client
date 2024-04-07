using System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace online_osu_beatmap_editor_client.components.Button
{
    public class UIButton : BaseUIComponent
    {
        private Color normalColor = StyleVariables.colorBgSecondary;
        private Color activeColor = StyleVariables.colorPrimary;
        private Vector2f iconSize = new Vector2f(50, 50);
        private Vector2f buttonSize = new Vector2f(75, 75);

        private Texture iconTexture;
        private Sprite iconSprite;

        private bool isActive;
        private bool isClicked;

        private RectangleShape buttonShape;
        public event EventHandler Clicked;

        public UIButton(string iconPath, int posX, int posY, bool isActive = false)
            : base(posX, posY)
        {
            this.isActive = isActive;
            this.GenerateBaseButtonShape();
            this.GenerateIcon(iconPath);
        }

        private void GenerateBaseButtonShape()
        {
            buttonShape = new RectangleShape(buttonSize);
            buttonShape.Position = new Vector2f(posX, posY);
            buttonShape.FillColor = isActive ? activeColor : normalColor;
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

        private void HandleClick()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            bool isMouseOver = buttonShape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);

            if (isMouseOver && Mouse.IsButtonPressed(Mouse.Button.Left) && !isClicked)
            {
                isClicked = true;
                //buttonShape.FillColor = activeColor;
                Clicked?.Invoke(this, EventArgs.Empty);
            }
            else if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isClicked = false;
                //buttonShape.FillColor = isActive ? activeColor : normalColor;
            }
        }

        public override void Update()
        {
            this.HandleClick();
        }

        public override void Draw()
        {
            window.Draw(buttonShape);
            window.Draw(iconSprite);
        }
    }
}