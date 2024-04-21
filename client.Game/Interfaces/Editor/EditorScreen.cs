using client.Game.Config;
using client.Game.Interfaces.Editor.Components;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

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
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Margin = new MarginPadding { Top = 30 + 75 }, // Height of navbar and timeline combined
                                },
                                new Timeline {
                                    Y = 30, // Height of navbar
                                },
                                new NavBar(),
                                new BottomBar(),
                            }
                        }
                    }
                }
            };
        }
    }
}
