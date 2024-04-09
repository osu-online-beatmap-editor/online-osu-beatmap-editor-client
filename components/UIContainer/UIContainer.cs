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
        public List<BaseUIComponent> elements = new List<BaseUIComponent>();
        private int spacing;
        private ContainerOrientation orientation;
        private RectangleShape background;
        private bool isAutoSizingX = false;
        private bool isAutoSizingY = false;

        public UIContainer(Vector2i pos, Vector2i size, int spacing, ContainerOrientation orientation, [Optional]Color bgColor) : base(pos)
        {
            if (bgColor != new Color(0,0,0,0))
            {
                this.bgColor = bgColor;
            }

            this.size = size;

            if (size.X == 0)
            {
                isAutoSizingX = true;
            }
            
            if (size.Y == 0)
            {
                isAutoSizingY = true;
            }

            this.orientation = orientation;
            this.spacing = spacing;

            InitBackground();
        }

        private BaseUIComponent FindBiggestElement(ContainerOrientation axis)
        {
            BaseUIComponent result = null;

            foreach (var element in elements)
            {
                if (result == null || 
                    (axis == ContainerOrientation.Vertical  && result.size.X < element.size.X) ||
                    (axis == ContainerOrientation.Horizontal && result.size.Y < element.size.Y)
                )
                {
                    result = element;
                }
            }

            return result;
        }

        public void AddElement(BaseUIComponent element)
        {
            elements.Add(element);
            RepositionElements();

            Vector2i newSize = new Vector2i(size.X, size.Y);

            if (isAutoSizingY)
            {
                if (orientation == ContainerOrientation.Vertical)
                {
                    newSize.Y = size.Y + element.size.Y + spacing + (spacing / 2);
                }
                else
                {
                    newSize.Y = FindBiggestElement(ContainerOrientation.Horizontal).size.Y + spacing * 2;
                }
            }

            if (isAutoSizingX)
            {
                if (orientation == ContainerOrientation.Horizontal)
                {
                    newSize.X = size.X + element.size.X + spacing + (spacing / 2); ;
                }
                else
                {
                    newSize.X = FindBiggestElement(ContainerOrientation.Vertical).size.X + spacing * 2;
                }
            }

            size = newSize;
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

        public override void HandleSizeUpdate(Vector2i size)
        {
            base.HandleSizeUpdate(size);
            if (background != null)
            {
                background.Size = (Vector2f)size;
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