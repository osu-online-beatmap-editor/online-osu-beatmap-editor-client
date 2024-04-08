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
        private Vector2i baseEditorFieldSize = new Vector2i(512, 384);
        private RectangleShape fieldShape;
        private Color fieldColor = new Color(255, 255, 255, 100); // Przezroczysty biały kolor
        private int gridSize = 16; // Rozmiar kratki w pikselach
        private Color gridColor = new Color(255, 255, 255, 50); // Przezroczysty biały kolor kratki
        private List<RectangleShape> gridLines = new List<RectangleShape>(); // Lista linii kratownicy

        private int circleIndex = 1;
        private int currentColor = 0;
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
            width = (int)(baseEditorFieldSize.X * scale);
            height = (int)(baseEditorFieldSize.Y * scale);

            fieldShape = new RectangleShape(new Vector2f(width, height));
            fieldShape.Origin = new Vector2f(width / 2, height / 2);
            fieldShape.Position = new Vector2f(posX, posY);
            fieldShape.FillColor = fieldColor;

            GenerateGrid();
        }

        private void GenerateGrid()
        {
            for (int x = 0; x <= baseEditorFieldSize.X; x += gridSize)
            {
                RectangleShape line = new RectangleShape(new Vector2f(1, height));
                line.Position = new Vector2f(posX - width / 2 + x * scale, posY - height / 2);
                line.FillColor = gridColor;
                gridLines.Add(line);
            }

            for (int y = 0; y <= baseEditorFieldSize.Y; y += gridSize)
            {
                RectangleShape line = new RectangleShape(new Vector2f(width, 1));
                line.Position = new Vector2f(posX - width / 2, posY - height / 2 + y * scale);
                line.FillColor = gridColor;
                gridLines.Add(line);
            }
        }

        public override void Draw()
        {
            window.Draw(fieldShape);
            foreach (var line in gridLines)
            {
                window.Draw(line);
            }
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
            Vector2i newHitCirclePos = EditorHelper.CalculateCirclePosition(posX, posY, width, height, clickPoint, scale);
            HitCircle newHitCircle = new HitCircle(newHitCirclePos.X, newHitCirclePos.Y, circleIndex, EditorData.CS, circleColor);

            circles.Add(newHitCircle);
            circleIndex++;
            EditorData.isNewComboActive = false;
        }

        private void HandleClick(Vector2i clickPoint)
        {
            EditorTools currentlySelectedEditorTool = EditorData.currentlySelectedEditorTool;
            switch (currentlySelectedEditorTool)
            {
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
