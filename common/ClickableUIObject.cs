using System;
using SFML.System;
using SFML.Window;
using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using online_osu_beatmap_editor_client.config;

namespace online_osu_beatmap_editor_client.components
{
    public abstract class ClickableUIObject : BaseUIComponent
    {
        protected bool isHovered;
        protected bool isPressed;
        protected Color defaultColor;
        protected Color activeColor;
        protected Color currentColor;

        public event EventHandler Clicked;

        public ClickableUIObject(int posX, int posY, Color defaultColor, Color activeColor) : base(posX, posY)
        {
            this.defaultColor = defaultColor;
            this.activeColor = activeColor;
            this.currentColor = defaultColor;

            isHovered = false;
            isPressed = false;
        }

        public virtual void OnClick() { }

        public virtual void OnRelease() { }

        public override void Update()
        {
            isHovered = IsMouseOver();
            if (isHovered && !isPressed && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isPressed = true;
                currentColor = activeColor;
                Clicked?.Invoke(this, EventArgs.Empty);
                OnClick();
            }
            else if (isPressed && !Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isPressed = false;
                currentColor = defaultColor;
                OnRelease();
            }
        }

        private bool IsMouseOver()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            return (mousePosition.X >= posX && mousePosition.X <= posX + width &&
                    mousePosition.Y >= posY && mousePosition.Y <= posY + height);
        }
    }
}