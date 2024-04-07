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

        public UIContainer(int posX, int posY, int width, int height, int spacing, ContainerOrientation orientation, [Optional]Color bgColor) : base(posX, posY)
        {
            if (bgColor != new Color(0,0,0,0))
            {
                this.bgColor = bgColor;
            }
            
            this.orientation = orientation;
            this.spacing = spacing;
            this.width = width;
            this.height = height;

            InitBackground();
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

        private void InitBackground()
        {
            background = new RectangleShape(new Vector2f(width, height));
            background.Position = new Vector2f(posX, posY);
            background.FillColor = bgColor;
        }

        public override void HandlePositionUpdate(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            background.Position = new Vector2f(posX, posY);
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