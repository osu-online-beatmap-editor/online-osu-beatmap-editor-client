﻿using online_osu_beatmap_editor_client.common;
using online_osu_beatmap_editor_client.components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        private Color gridColor = new Color(255, 255, 255, 50); 
        private List<RectangleShape> gridLines = new List<RectangleShape>();

        private int selectedCircleIndex;

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

        private int _currentTime;
        public int currentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value <= 15 ? 15 : value;
                    Console.WriteLine(_currentTime);
                }
            }
        }

        private int totalTime = 180000;
        private int bpm = 200;
        private int timeSnapping = 3;

        #region Setup

        public EditorField(Vector2i pos, EditorShortcuts editorShortcuts) : base(pos)
        {
            currentTime = 15;
            isMouseButtonPressed = false;
            isHovered = false;
            this.size = new Vector2i((int)(baseEditorFieldSize.X * scale), (int)(baseEditorFieldSize.Y * scale));

            GenerateGrid();
            InitCirclePreview();
            InitListeners(editorShortcuts);
        }

        private void InitCirclePreview()
        {
            circlePreview = new HitCircle(pos, 1, EditorData.CS, colors[0]);
            UpdateCirclePreviewNumberAndColor();
        }

        private void InitListeners(EditorShortcuts editorShortcuts)
        {
            EditorData.GridTypeChanged += (sender, e) => HandleGridChange();
            EditorData.IsNewComboActiveChanged += (sender, e) => HandleIsNewComboChange();
            editorShortcuts.DeleteCircleEvent += (sender, e) => HandleDeleteCircle();
            editorShortcuts.TimeForwardEvent += (sender, e) => HandleTimeForward();
            editorShortcuts.TimeBackwardEvent += (sender, e) => HandleTimeBackward();
        }

        #endregion Setup

        #region Listeners

        private void HandleGridChange()
        {
            GenerateGrid();
        }

        private void HandleIsNewComboChange ()
        {
            UpdateCirclePreviewNumberAndColor();
        }

        private void HandleDeleteCircle ()
        {
            if (EditorData.selectedCircle == null || circles.Count <= selectedCircleIndex)
            {
                return;
            }

            circles.RemoveAt(selectedCircleIndex);
            EditorData.selectedCircle.isSelected = false;
            EditorData.selectedCircle = null;
            selectedCircleIndex = -1;
        }

        private void HandleTimeForward()
        {
            currentTime = EditorHelper.GetNextTickTime(currentTime, totalTime, bpm, timeSnapping);
        }

        private void HandleTimeBackward()
        {
            if (currentTime <= 15)
            {
                return;
            }

            currentTime = EditorHelper.GetPreviousTickTime(currentTime, totalTime, bpm, timeSnapping);
        }

        #endregion Listeners

        #region Calculate circle position

        private Vector2i ApplyDistanceSnappingForCircle(Vector2i value)
        {
            Vector2i result = value;
            if (selectedCircleIndex > 0)
            {
                result = EditorHelper.UpdateCirclePositionWithDistanceSnapping(result, dragingOffset, pos, size, EditorHelper.GetDistanceSnapping(scale), circles[selectedCircleIndex - 1].pos);
            }
            else if (circles.Count > 0)
            {
                result = EditorHelper.UpdateCirclePositionWithDistanceSnapping(result, dragingOffset, pos, size, EditorHelper.GetDistanceSnapping(scale), circles[circles.Count - 1].pos);
            }
            return result;
        }

        private Vector2i ApplyGridSnappingForCircle(Vector2i value)
        {
            Vector2i result = value;
            Vector2i rawClickPosOnField = EditorHelper.GetRawClickPosOnField(result, pos, size);
            Vector2i unscaledClickPosOnField = EditorHelper.GetUnscaledClickPosOnField(rawClickPosOnField, scale);

            int gridDivider = GridTypeMapper.GetGridValue(EditorData.gridType);
            int gridSize = (int)(size.X / gridDivider / scale);

            Vector2i snap = EditorHelper.SnapToGrid(unscaledClickPosOnField, gridSize);

            result = pos - size / 2 + new Vector2i((int)(snap.X * scale), (int)(snap.Y * scale));

            return result;
        }

        private Vector2i ApplyFieldBordersForCircle(Vector2i value)
        {
            Vector2i result = value;
            result = EditorHelper.CalculateCirclePositionBorder(value, dragingOffset, pos, size);
            return result;
        }

        private Vector2i CalculateCirclePos()
        {
            Vector2i result = Mouse.GetPosition(window);

            if (EditorData.isDistanceSnapActive)
            {
                result = ApplyDistanceSnappingForCircle(result);
            }

            if (EditorData.isGridSnapActive)
            {
                result = ApplyGridSnappingForCircle(result);
            }

            result = ApplyFieldBordersForCircle(result);

            return result;
        }

        #endregion Calculate circle position

        #region Manage circle data

        private void SetNewCircleColor()
        {
            currentColor++;
            if (currentColor >= colors.Length)
            {
                currentColor = 0;
            }
        }

        private Color GetCircleColor([Optional]bool next)
        {
            int currentColorCopy = currentColor;
            
            if (next == true)
            {
                currentColorCopy++;
                if (currentColorCopy >= colors.Length)
                {
                    currentColorCopy = 0;
                }
            }

            return colors[currentColorCopy];
        }

        private int GetCircleNumber([Optional]bool isNewCombo)
        {
            if (isNewCombo == true)
            {
                return 1;
            }

            return circleIndex;
        }

        private void ResetCircleIndex()
        {
            circleIndex = 1;
        }

        private void IncreateCircleIndex()
        {
            circleIndex++;
        }

        private float GetCircleSize()
        {
            return EditorData.CS;
        }

        #endregion Manage circle data

        #region Grid 
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

        private void DrawGrid()
        {
            foreach (var line in gridLines)
            {
                window.Draw(line);
            }
        }

        #endregion Grid

        #region Circle preview
        private void UpdateCirclePreviewNumberAndColor()
        {
            bool isNewCombo = EditorData.isNewComboActive;

            if (isNewCombo)
            {
                circlePreview.color = GetCircleColor(isNewCombo);
                circlePreview.number = GetCircleNumber(true);
                return;
            }

            circlePreview.color = colors[currentColor];
            circlePreview.number = GetCircleNumber();
        }

        private void UpdateCirclePreviewPos()
        {
            if (EditorData.currentlySelectedEditorTool == EditorTools.Circle)
            {
                circlePreview.pos = CalculateCirclePos();
            }
        }

        private void DrawCirclePreview()
        {
            if (EditorData.currentlySelectedEditorTool == EditorTools.Circle)
            {
                circlePreview.Draw();
            }
        }

        #endregion Circle preview

        #region Tools 
        private void PlaceCircle()
        {
            if (EditorData.isNewComboActive)
            {
                SetNewCircleColor();
                ResetCircleIndex();
            }

            Color newCircleColor = GetCircleColor();
            int newCircleIndex = GetCircleNumber();
            Vector2i newHitCirclePos = CalculateCirclePos();
            float newCircleSize = GetCircleSize();

            HitCircle newHitCircle = new HitCircle(newHitCirclePos, newCircleIndex, newCircleSize, newCircleColor);

            EditorData.isNewComboActive = false;
            circles.Add(newHitCircle);
            IncreateCircleIndex();
            UpdateCirclePreviewNumberAndColor();
        }

        private void SelectTool(Vector2i clickPoint)
        {
            if (EditorData.selectedCircle != null && EditorData.selectedCircle.IsMouseOver(clickPoint))
            {
                isDraging = true;
                dragingOffset = EditorData.selectedCircle.pos - clickPoint;
            }
            else
            {
                int clickedCircleIndex = circles.FindIndex(circle => circle.IsMouseOver(clickPoint));

                if (EditorData.selectedCircle != null)
                {
                    EditorData.selectedCircle.isSelected = false;
                }

                if (clickedCircleIndex == -1)
                {
                    EditorData.selectedCircle = null;
                    return;
                }

                EditorData.selectedCircle = circles[clickedCircleIndex];
                selectedCircleIndex = clickedCircleIndex;
                EditorData.selectedCircle.isSelected = true;
            }
        }

        #endregion Tools

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

        private void DrawCircles()
        {
            foreach (var circle in circles)
            {
                circle.Draw();
            }
        }

        public override void Draw()
        {
            DrawGrid();
            DrawCircles();
            DrawCirclePreview();
        }

        public override void Update()
        {
            AddClickListener();
            UpdateCirclePreviewPos();
            if (EditorData.currentlySelectedEditorTool != EditorTools.Select && EditorData.selectedCircle != null)
            {
                EditorData.selectedCircle.isSelected = false;
                EditorData.selectedCircle = null;
                selectedCircleIndex = -1;
                dragingOffset = new Vector2i(0, 0);
            }
            if (isDraging && EditorData.selectedCircle != null)
            {
                EditorData.selectedCircle.pos = CalculateCirclePos();
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
