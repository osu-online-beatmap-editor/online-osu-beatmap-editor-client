using System;
using System.Collections.Generic;
using online_osu_beatmap_editor_client.common;
using SFML.Graphics;
using SFML.System;

namespace online_osu_beatmap_editor_client.components.Container
{
    public class UIVerticalContainer : BaseUIComponent
    {
        private List<BaseUIComponent> elements = new List<BaseUIComponent>();
        private int spacing;

        public UIVerticalContainer(int posX, int posY, int spacing) : base(posX, posY)
        {
            this.spacing = spacing;
        }

        public void AddElement(BaseUIComponent element)
        {
            elements.Add(element);
            RepositionElements();
        }

        private void RepositionElements()
        {
            int currentY = posY;
            foreach (var element in elements)
            {
                element.SetPosition(posX, currentY);
                currentY += element.height + spacing;
            }
        }

        public override void Draw()
        {
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