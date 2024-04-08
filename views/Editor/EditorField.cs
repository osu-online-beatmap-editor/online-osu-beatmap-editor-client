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

        public EditorField(Vector2i pos) : base(pos)
        {
            isMouseButtonPressed = false;
            isHovered = false;
            this.size = new Vector2i((int)(baseEditorFieldSize.X * scale), (int)(baseEditorFieldSize.Y * scale));

            fieldShape = new RectangleShape((Vector2f)size);
            fieldShape.Origin = new Vector2f(size.X / 2, size.Y / 2);
            fieldShape.Position = (Vector2f)pos;
            fieldShape.FillColor = fieldColor;

            GenerateGrid();
        }

        private void GenerateGrid()
        {
            for (int x = 0; x <= baseEditorFieldSize.X; x += gridSize)
            {
                RectangleShape line = new RectangleShape(new Vector2f(1, size.Y));
                line.Position = new Vector2f(pos.X - size.X / 2 + x * scale, pos.Y - size.Y / 2);
                line.FillColor = gridColor;
                gridLines.Add(line);
            }

            for (int y = 0; y <= baseEditorFieldSize.Y; y += gridSize)
            {
                RectangleShape line = new RectangleShape(new Vector2f(size.X, 1));
                line.Position = new Vector2f(pos.X - size.X / 2, pos.Y - size.Y / 2 + y * scale);
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
            Vector2i newHitCirclePos = EditorHelper.CalculateCirclePosition(pos, size, clickPoint, scale);
            HitCircle newHitCircle = new HitCircle(newHitCirclePos, circleIndex, EditorData.CS, circleColor);

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
                default:
                    foreach (var circle in circles)
                    {
                        circle.pos = pos;
                    }
                    break;
            }
        }

        private void AddClickListener()
        {
            isHovered = IsMouseOver();
            if (isHovered && !isMouseButtonPressed && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isMouseButtonPressed = true;
                Vector2i mousePosition = Mouse.GetPosition(window);
                Vector2i clickPoint = new Vector2i(mousePosition.X - pos.X + size.X / 2, mousePosition.Y - pos.Y + size.Y / 2);
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
            return (mousePosition.X >= pos.X - size.X / 2 && mousePosition.X <= pos.X + size.X - size.X / 2 &&
                    mousePosition.Y >= pos.Y - size.Y / 2 && mousePosition.Y <= pos.Y + size.Y - size.Y / 2);
        }
    }
}
