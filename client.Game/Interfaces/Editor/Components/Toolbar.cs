﻿using System;
using System.Collections.Generic;
using client.Game.Core.Data;
using client.Game.Graphics.UserInterface;
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

        private int buttonSize = 70;
        private int gap = 5;

        private Dictionary<EditorTools, IconButton> toolButtons = new();
        private Dictionary<EditorToggleButtons, IconButton> toggleButtons = new();

        private int getPositionY(int index)
        {
            return (buttonSize + gap) * index;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = container = new Container
            {
                Y = gap,
                X = gap,
                Children = new Drawable[]
                {
                    new Box
                    {
                        X = -gap,
                        Y = -gap,
                        Origin = Anchor.TopLeft,
                        Width = buttonSize + gap * 2,
                        Height = 1080,
                        Colour = Colour4.Brown,
                    },
                }
            };
            setupButtons();
            setupEvents();
        }

        private IconButton createButton(int index, bool isActive)
        {
            IconButton button = new IconButton
            {
                Y = getPositionY(index),
                Origin = Anchor.TopLeft,
                IsActive = isActive
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
                IconButton button = createButton(buttonIndex, isActive);

                toolButtons.Add(buttonTool, button);
                buttonIndex++;
            }

            foreach (EditorToggleButtons buttonEnum in Enum.GetValues(typeof(EditorToggleButtons)))
            {
                bool isActive = EditorData.GetToggleButtonState(buttonEnum);
                IconButton button = createButton(buttonIndex, isActive);
              
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
