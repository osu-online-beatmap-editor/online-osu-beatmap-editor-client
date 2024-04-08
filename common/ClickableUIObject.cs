using System;
using SFML.System;
using SFML.Window;
using online_osu_beatmap_editor_client.common;
using SFML.Graphics;

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

        private bool _isActive = false;

        public bool isActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    currentColor = value ? activeColor : defaultColor;
                }
            }
        }

        public ClickableUIObject(Vector2i pos, Color defaultColor, Color activeColor) : base(pos)
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
                if (!isActive)
                {
                    currentColor = defaultColor;
                }
                OnRelease();
            }
        }

        private bool IsMouseOver()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            return (mousePosition.X >= pos.X && mousePosition.X <= pos.X + size.X &&
                    mousePosition.Y >= pos.Y && mousePosition.Y <= pos.Y + size.Y);
        }
    }
}