using System;
using System.Collections.Generic;
using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.config;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.components.Container
{
    public class UIVerticalContainer : BaseUIComponent
    {
        private Color bgColor = StyleVariables.colorBgSecondary;
        private List<BaseUIComponent> elements = new List<BaseUIComponent>();
        private int spacing;

        public UIVerticalContainer(int posX, int posY, int width, int height, int spacing) : base(posX, posY)
        {
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
            int currentY = posY + spacing;
            foreach (var element in elements)
            {
                int elementPosX = posX + spacing;
                element.SetPosition(elementPosX, currentY);
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
            this.DrawBackground();

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