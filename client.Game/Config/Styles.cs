using osuTK.Graphics;

namespace client.Game.Config
{
    public static class Styles
    {
        public static readonly Color4 BACKGROUND_COLOR = new Color4(30, 30, 30, byte.MaxValue);
        public static readonly Color4 BACKGROUND_SECONDARY_COLOR = new Color4(35, 35, 35, byte.MaxValue);
        public static readonly Color4 BACKGROUND_TERTIARY_COLOR = new Color4(40, 40, 40, byte.MaxValue);
        public static readonly Color4 COLOR_PRIMARY = new Color4(255, 123, 44, byte.MaxValue);

        public static readonly Color4 BUTTON_COLOR = new Color4(44, 44, 44, byte.MaxValue);
        public static readonly Color4 BUTTON_COLOR_ACTIVE = COLOR_PRIMARY;

        public static readonly int CORNER_RADIUS_DEFAULT = 10;

        public static readonly int TOOLBAR_BUTTON_SIZE = 70;
        public static readonly int TOOLBAR_GAP = 10;

        public static Color4 GetButtonColor(bool isActive)
        {
            return isActive ? BUTTON_COLOR_ACTIVE : BUTTON_COLOR;
        }
    }
}
