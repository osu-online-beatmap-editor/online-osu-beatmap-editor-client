using System;
using System.Collections.Generic;
using client.Game.Config;
using client.Game.Core.Data;
using client.Game.Graphics.UserInterface;
using client.Game.Resources;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;

namespace client.Game.Interfaces.Editor.Components
{
    public partial class Toolbar : CompositeDrawable
    {
        private Container container;

        private int buttonSize = Styles.TOOLBAR_BUTTON_SIZE;
        private int gap = 10;

        private Dictionary<EditorTools, IconButton> toolButtons = new();
        private Dictionary<EditorToggleButtons, IconButton> toggleButtons = new();

        private int getPositionY(int index)
        {
            return (buttonSize + gap) * index;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            RelativeSizeAxes = Axes.Y;
            Width = buttonSize + gap * 2;
            InternalChild = container = new Container
            {
                Y = gap,
                X = gap,
                RelativeSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    new Box
                    {
                        X = -gap,
                        Y = -gap,
                        Origin = Anchor.TopLeft,
                        Width = buttonSize + gap * 2,
                        RelativeSizeAxes = Axes.Y,
                        Colour = Styles.BACKGROUND_SECONDARY_COLOR,
                    },
                }
            };
            setupButtons();
            setupEvents();
        }

        private IconButton createButton(int index, string icon, bool isActive)
        {
            IconButton button = new IconButton
            {
                Y = getPositionY(index),
                Origin = Anchor.TopLeft,
                IsActive = isActive,
                Icon = icon,
            };
            container.Add(button);
            return button;
        }

        private void setupButtons()
        {
            int buttonIndex = 0;

            foreach (EditorTools buttonTool in Enum.GetValues(typeof(EditorTools)))
            {
                bool isActive = EditorData.IsToolActive(buttonTool);
                string icon = IconsMapper.GetEditorToolIcon(buttonTool);
                IconButton button = createButton(buttonIndex, icon, isActive);

                toolButtons.Add(buttonTool, button);
                buttonIndex++;
            }

            foreach (EditorToggleButtons buttonEnum in Enum.GetValues(typeof(EditorToggleButtons)))
            {
                bool isActive = EditorData.GetToggleButtonState(buttonEnum);
                string icon = IconsMapper.GetEditorToggleIcon(buttonEnum);
                IconButton button = createButton(buttonIndex, icon, isActive);
              
                toggleButtons.Add(buttonEnum, button);
                buttonIndex++;
            }
        }

        private void setupEvents()
        {
            EditorTools[] toolButtonsArray = (EditorTools[])Enum.GetValues(typeof(EditorTools));

            foreach (var entry in toolButtonsArray)
            {
                var button = toolButtons[entry];

                button.Action = () => { EditorData.CurrentTool = entry; };
                EditorData.CurrentToolChanged += (s, e) => { button.IsActive = EditorData.IsToolActive(entry); };
            }

            EditorToggleButtons[] toggleButtonsArray = (EditorToggleButtons[])Enum.GetValues(typeof(EditorToggleButtons));

            foreach (var entry in toggleButtonsArray)
            {
                var button = toggleButtons[entry];

                button.Action = () => { EditorData.SetToggleButtonState(entry, !EditorData.IsToggleActive(entry)); };
                EditorData.ToggleButtonStatesChanged += (s, e) => { button.IsActive = EditorData.IsToggleActive(entry); };
            }
        }
    }
}
