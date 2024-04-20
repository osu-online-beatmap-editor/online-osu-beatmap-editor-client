using client.Game.Config;
using client.Game.Interfaces.Editor.Components;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

namespace client.Game.Interfaces.Editor
{
    public partial class EditorScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Styles.BACKGROUND_COLOR,
                    RelativeSizeAxes = Axes.Both,
                },
                new Toolbar {
                    RelativeSizeAxes = Axes.Y,
                }
            };
        }
    }
}
