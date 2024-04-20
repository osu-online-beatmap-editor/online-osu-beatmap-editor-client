using client.Game.Config;
using client.Game.Interfaces.Editor.Components;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace client.Game.Interfaces.Editor
{
    public partial class EditorScreen : Screen
    {
        private Toolbar toolbar;

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
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        toolbar = new Toolbar(),
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Margin = new MarginPadding { Left = Styles.TOOLBAR_GAP * 2 + Styles.TOOLBAR_BUTTON_SIZE },
                            Children = new Drawable[]
                            {
                                new NavBar(),
                                new Timeline {
                                    Y = 50, // Height of navbar
                                },
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Margin = new MarginPadding { Top = 50 + 75 }, // Height of navbar and timeline combined
                                },
                                new BottomBar(),
                            }
                        }
                    }
                }
            };
        }
    }
}
