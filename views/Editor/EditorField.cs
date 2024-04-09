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

        private float scale = 2f;
        private Vector2i baseEditorFieldSize = new Vector2i(512, 384);
        private Color fieldColor = new Color(255, 255, 255, 100);
        private Color gridColor = new Color(255, 255, 255, 50); 
        private List<RectangleShape> gridLines = new List<RectangleShape>();

        private int selectedCircleIndex;
        private HitCircle selectedCircle;

        private HitCircle circlePreview;

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

            circlePreview = new HitCircle(pos, 1, EditorData.CS, colors[0]);

            EditorData.GridTypeChanged += (sender, e) => GenerateGrid();
            EditorData.IsNewComboActiveChanged += (sender, e) => HandleIsNewComboChange();
        }

        private Color GetNextCircleColor ()
        {
            int currentColorCopy = currentColor + 1;
            if (currentColorCopy >= colors.Length)
            {
                currentColorCopy = 0;
            }

            return colors[currentColorCopy];
        }

        private void HandleIsNewComboChange ()
        {
            Console.WriteLine(EditorData.isNewComboActive);
            UpdateCirclePreviewNumberAndColor();
        }

        private void UpdateCirclePreviewNumberAndColor()
        {
            bool isNewCombo = EditorData.isNewComboActive;

            if (isNewCombo)
            {
                circlePreview.color = GetNextCircleColor();
                circlePreview.number = 1;
                return;
            }

            circlePreview.color = colors[currentColor];
            circlePreview.number = circleIndex;
        }

        private void GenerateGrid()
        {
            gridLines.Clear();

            int gridDivider = GridTypeMapper.GetGridValue(EditorData.gridType);

            int gridSize = size.X / gridDivider;

            for (int x = 0; x <= size.X; x += gridSize)
            {
                RectangleShape line = new RectangleShape(new Vector2f(1, size.Y));
                line.Position = new Vector2f(pos.X - size.X / 2 + x, pos.Y - size.Y / 2);
                line.FillColor = gridColor;
                gridLines.Add(line);
            }

            for (int y = 0; y <= size.Y; y += gridSize)
            {
                RectangleShape line = new RectangleShape(new Vector2f(size.X, 1));
                line.Position = new Vector2f(pos.X - size.X / 2, pos.Y - size.Y / 2 + y);
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

            if (IsMouseOver() && EditorData.currentlySelectedEditorTool == EditorTools.Circle)
            {
                circlePreview.Draw();
            }
        }

        private Vector2i GetUnscaledMousePosOnField ()
        {
            Vector2i mousePosition2 = Mouse.GetPosition(window);
            Vector2i rawClickPosOnField = EditorHelper.GetRawClickPosOnField(mousePosition2, pos, size);
            Vector2i unscaledClickPosOnField = EditorHelper.GetUnscaledClickPosOnField(rawClickPosOnField, scale);

            return unscaledClickPosOnField;
        }

        private Vector2i GetCirclePosition()
        {
            Vector2i mousePosition = Mouse.GetPosition(window);
            Vector2i result = mousePosition;
            if (EditorData.isDistanceSnapActive)
            {
                if(selectedCircleIndex > 0)
                {
                    result = EditorHelper.GetNewCircleDraggingPositionWithSnaping(result, dragingOffset, pos, size, 200, circles[selectedCircleIndex - 1].pos);
                }
                else if(circles.Count > 0)
                {
                    result = EditorHelper.GetNewCircleDraggingPositionWithSnaping(result, dragingOffset, pos, size, 200, circles[circles.Count - 1].pos);
                }
            }
            if (EditorData.isGridSnapActive)
            {
                Vector2i rawClickPosOnField = EditorHelper.GetRawClickPosOnField(result, pos, size);
                Vector2i unscaledClickPosOnField = EditorHelper.GetUnscaledClickPosOnField(rawClickPosOnField, scale);

                int gridDivider = GridTypeMapper.GetGridValue(EditorData.gridType);
                int gridSize = (int)(size.X / gridDivider / scale);

                Vector2i snap = EditorHelper.SnapToGrid(unscaledClickPosOnField, gridSize);

                result = pos - size / 2 + new Vector2i((int)(snap.X * scale), (int)(snap.Y * scale));
            }

            return result;
        }

        private void PlaceCircle()
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
            Vector2i newHitCirclePos = GetCirclePosition();

            HitCircle newHitCircle = new HitCircle(newHitCirclePos, circleIndex, EditorData.CS, circleColor);

            circles.Add(newHitCircle);
            circleIndex++;
            EditorData.isNewComboActive = false;
            UpdateCirclePreviewNumberAndColor();
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
                    PlaceCircle();
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
                selectedCircle.pos = GetCirclePosition();
            }
            if (EditorData.currentlySelectedEditorTool == EditorTools.Circle)
            {
                circlePreview.pos = GetCirclePosition();
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
