using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorField : BaseUIComponent
    {
        protected bool isMouseButtonPressed;
        protected bool isHovered;

        private float scale = 2.3f;
        private Vector2i baseEditorFielSize = new Vector2i(512, 384); 
        private RectangleShape fieldShape;
        private Color fieldColor = Color.White;

        private int circleIndex = 1; //@TODO temp
        private int currentColor = 0; //@TODO temp
        private Color[] colors = new Color[]
        {
            new Color(255, 0, 0),
            new Color(0, 255, 0), 
            new Color(0, 0, 255)
        };

        private List<HitCircle> circles = new List<HitCircle>();

        public EditorField(int posX, int posY) : base(posX, posY)
        {
            isMouseButtonPressed = false;
            isHovered = false; 
            width = (int)(baseEditorFielSize.X * scale);
            height = (int)(baseEditorFielSize.Y * scale);

            fieldShape = new RectangleShape(new Vector2f(width, height));
            fieldShape.Origin = new Vector2f(width / 2, height / 2); 
            fieldShape.Position = new Vector2f(posX, posY);
            fieldShape.FillColor = fieldColor;
        }

        public override void Draw()
        {
            window.Draw(fieldShape);
            foreach (var circle in circles)
            {
                circle.Draw();
            }
        }


        private void PlaceCircle(Vector2i clickPoint)
        {
            if (EditorData.isNewComboActive)
            {
                circleIndex = 1;
                currentColor++;
                if (currentColor >= colors.Length)
                {
                    currentColor = 0;
                }
            }
            Color circleColor = colors[currentColor];
            Vector2i newHitCirlcePos = EditorHelper.CalculateCirclePosition(posX, posY, width, height, clickPoint, scale);
            HitCircle newHitCircle = new HitCircle(newHitCirlcePos.X, newHitCirlcePos.Y, circleIndex, EditorData.CS, circleColor);

            circles.Add(newHitCircle);
            circleIndex++;
            EditorData.isNewComboActive = false;
        }

        private void HandleClick(Vector2i clickPoint)
        {
            EditorTools currentlySelectedEditorTool = EditorData.currentlySelectedEditorTool;
            switch (currentlySelectedEditorTool) {
                case EditorTools.Circle:
                    PlaceCircle(clickPoint);
                    return;
            }
        }

        private void AddClickListener()
        {
            isHovered = IsMouseOver();
            if (isHovered && !isMouseButtonPressed && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isMouseButtonPressed = true;
                Vector2i mousePosition = Mouse.GetPosition(window);
                Vector2i clickPoint = new Vector2i(mousePosition.X - posX + width / 2, mousePosition.Y - posY + height / 2);
                Vector2i unScaledClickPoint = new Vector2i((int)(clickPoint.X / scale), (int)(clickPoint.Y / scale));
                HandleClick(unScaledClickPoint);
            }
            else if (isMouseButtonPressed && !Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isMouseButtonPressed = false;
            }
        }

        public override void Update()
        {
            AddClickListener();
        }

        private bool IsMouseOver()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            return (mousePosition.X >= posX - width / 2 && mousePosition.X <= posX + width - width / 2 &&
                    mousePosition.Y >= posY - height / 2 && mousePosition.Y <= posY + height - height / 2);
        }
    }
}
