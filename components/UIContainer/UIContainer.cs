using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;

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

        public UIContainer(int posX, int posY, int width, int height, int spacing, ContainerOrientation orientation) : base(posX, posY)
        {
            this.orientation = orientation;
            this.spacing = spacing;
            this.width = width;
            this.height = height;
        }

        public void AddElement(BaseUIComponent element)
        {
            elements.Add(element);
            RepositionElements();
        }

        private void RepositionElements()
        {
            int currentX = posX + spacing;
            int currentY = posY + spacing;
            foreach (var element in elements)
            {
                int elementPosX = orientation == ContainerOrientation.Horizontal ? currentX : posX + spacing;
                int elementPosY = orientation == ContainerOrientation.Vertical ? currentY : posY + spacing;
                element.SetPosition(elementPosX, elementPosY);

                if (orientation == ContainerOrientation.Horizontal)
                    currentX += element.width + spacing;
                else
                    currentY += element.height + spacing;
            }
        }

        private void DrawBackground()
        {
            RectangleShape background = new RectangleShape(new Vector2f(width, height));
            background.Position = new Vector2f(posX, posY);
            background.FillColor = bgColor;
            window.Draw(background);
        }

        public override void Draw()
        {
            DrawBackground();

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