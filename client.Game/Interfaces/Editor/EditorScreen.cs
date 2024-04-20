using System.Diagnostics;
using System.Runtime.CompilerServices;
using client.Game.Config;
using client.Game.Graphics.UserInterface;
using client.Game.Interfaces.Editor.Components;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK.Graphics;

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
                new Toolbar()
            };
        }
    }
}
