using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public class EditorField : BaseUIComponent
    {
        protected bool isMouseButtonPressed;
        protected bool isHovered;

        private bool isDraging = false;
        private Vector2i dragingOffset;

        private float scale = 2.3f;
        private Vector2i baseEditorFieldSize = new Vector2i(512, 384);
        private Color fieldColor = new Color(255, 255, 255, 100);
        private int gridSize = 16; 
        private Color gridColor = new Color(255, 255, 255, 50); 
        private List<RectangleShape> gridLines = new List<RectangleShape>();

        private int selectedCircleIndex;
        private HitCircle selectedCircle;

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

        private void SelectTool(Vector2i clickPoint)
        {
            if (selectedCircle != null && selectedCircle.IsMouseOver(clickPoint))
            {
                isDraging = true;
                dragingOffset = selectedCircle.pos - clickPoint;
            }
            else
            {
                int clickedCircleIndex = circles.FindIndex(circle => circle.IsMouseOver(clickPoint));

                if (selectedCircle != null)
                {
                    selectedCircle.isSelected = false;
                }

                if (clickedCircleIndex == -1)
                {
                    selectedCircle = null;
                    return;
                }

                selectedCircle = circles[clickedCircleIndex];
                selectedCircleIndex = clickedCircleIndex;
                selectedCircle.isSelected = true;
            }
        }

        private void HandleClick(Vector2i clickPoint)
        {
            EditorTools currentlySelectedEditorTool = EditorData.currentlySelectedEditorTool;
            switch (currentlySelectedEditorTool)
            {
                case EditorTools.Select:
                    SelectTool(clickPoint);
                    return;
                case EditorTools.Circle:
                    Vector2i rawClickPosOnField = EditorHelper.GetRawClickPosOnField(clickPoint, pos, size);
                    Vector2i unscaledClickPosOnField = EditorHelper.GetUnscaledClickPosOnField(rawClickPosOnField, scale);
                    PlaceCircle(unscaledClickPosOnField);
                    return;
                default:
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
                HandleClick(mousePosition);
            }
            else if (isMouseButtonPressed && !Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                isMouseButtonPressed = false;
                isDraging = false;
            }
        }

        public override void Update()
        {
            AddClickListener();
            if (EditorData.currentlySelectedEditorTool != EditorTools.Select && selectedCircle != null)
            {
                selectedCircle.isSelected = false;
                selectedCircle = null;
                selectedCircleIndex = -1;
            }
            if (isDraging && selectedCircle != null)
            {
                Vector2i mousePosition = Mouse.GetPosition(window);

                if (EditorData.isDistanceSnapActive && selectedCircleIndex > 0)
                {
                    selectedCircle.pos = EditorHelper.GetNewCircleDraggingPositionWithSnaping(mousePosition, dragingOffset, pos, size, 200, circles[selectedCircleIndex - 1].pos);
                } 
                else 
                {
                    selectedCircle.pos = EditorHelper.CalculateDraggingPositionBorder(mousePosition, dragingOffset, pos, size);
                }
            }
        }

        private bool IsMouseOver()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            return (mousePosition.X >= pos.X - size.X / 2 && mousePosition.X <= pos.X + size.X - size.X / 2 &&
                    mousePosition.Y >= pos.Y - size.Y / 2 && mousePosition.Y <= pos.Y + size.Y - size.Y / 2);
        }
    }
}
