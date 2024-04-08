using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;
using System.Runtime.InteropServices;

namespace online_osu_beatmap_editor_client.components.Container
{
    public enum ContainerOrientation
    {
        Vertical,
        Horizontal
    }

    public class UIContainer : BaseUIComponent
    {
        private Color bgColor = StyleVariables.colorBgSecondary;
        private List<BaseUIComponent> elements = new List<BaseUIComponent>();
        private int spacing;
        private ContainerOrientation orientation;
        private RectangleShape background;

        public UIContainer(Vector2i pos, Vector2i size, int spacing, ContainerOrientation orientation, [Optional]Color bgColor) : base(pos)
        {
            if (bgColor != new Color(0,0,0,0))
            {
                this.bgColor = bgColor;
            }
            
            this.orientation = orientation;
            this.spacing = spacing;
            this.size = size;

            InitBackground();
        }

        public void AddElement(BaseUIComponent element)
        {
            elements.Add(element);
            RepositionElements();
        }

        private void RepositionElements()
        {
            int currentX = pos.X + spacing;
            int currentY = pos.Y + spacing;
            foreach (var element in elements)
            {
                int elementPosX = orientation == ContainerOrientation.Horizontal ? currentX : pos.X + spacing;
                int elementPosY = orientation == ContainerOrientation.Vertical ? currentY : pos.Y + spacing;
                element.pos = new Vector2i(elementPosX, elementPosY);

                if (orientation == ContainerOrientation.Horizontal)
                    currentX += element.size.X + spacing;
                else
                    currentY += element.size.Y + spacing;
            }
        }

        private void InitBackground()
        {
            background = new RectangleShape((Vector2f)size);
            background.Position = (Vector2f)pos;
            background.FillColor = bgColor;
        }

        public override void HandlePositionUpdate(Vector2i pos)
        {
            if (background != null)
            {
                background.Position = (Vector2f)pos;
            }
            RepositionElements();
        }

        public override void Draw()
        {
            window.Draw(background);

            foreach (var element in elements)
            {
                element.Draw();
            }
        }

        public override void Update()
        {
            foreach (var element in elements)
            {
                element.Update();
            }
        }
    }
}