using SFML.Graphics;
using SFML.System;
using System;
using System.Drawing;

namespace online_osu_beatmap_editor_client.views.Editor
{
    public static class EditorHelper
    {
        public static Vector2i CalculateCirclePosition(Vector2i pos, Vector2i size, Vector2i clickPoint, float scale) {
            Vector2i newHitCirlcePos = new Vector2i(pos.X - size.X / 2 + (int)(clickPoint.X * scale), pos.Y - size.Y / 2 + (int)(clickPoint.Y * scale));
            return newHitCirlcePos;
        }

        public static Vector2i GetRawClickPosOnField(Vector2i mousePosition, Vector2i pos, Vector2i size)
        {
            Vector2i clickPos = new Vector2i(mousePosition.X - pos.X + size.X / 2, mousePosition.Y - pos.Y + size.Y / 2);
            return clickPos;
        }

        public static Vector2i GetUnscaledClickPosOnField(Vector2i rawClickPoint, float scale)
        {
            Vector2i clickPos = new Vector2i((int)(rawClickPoint.X / scale), (int)(rawClickPoint.Y / scale));
            return clickPos;
        }

        public static Vector2i CalculateCirclePositionBorder(Vector2i mousePosition, Vector2i dragingOffset, Vector2i pos, Vector2i size)
        {
            Vector2i mousePositionWithOffset = mousePosition + dragingOffset;

            int x = Math.Max(Math.Min(mousePositionWithOffset.X, pos.X + size.X / 2), pos.X - size.X / 2);
            int y = Math.Max(Math.Min(mousePositionWithOffset.Y, pos.Y + size.Y / 2), pos.Y - size.Y / 2);

            return new Vector2i(x, y);
        }

        public static Vector2i GetNewCircleDraggingPositionWithSnaping(Vector2i mousePosition, Vector2i dragingOffset, Vector2i pos, Vector2i size, float radius, Vector2i? prevousCirclePos)
        {
            Vector2i mousePositionWithOffset = mousePosition + dragingOffset;

            Vector2i newPosition = mousePositionWithOffset;

            if (prevousCirclePos != null)
            {
                Vector2i anchorPos = prevousCirclePos.Value;

                System.Numerics.Vector2 delta = new System.Numerics.Vector2(mousePositionWithOffset.X + dragingOffset.X, mousePositionWithOffset.Y + dragingOffset.Y) - new System.Numerics.Vector2(anchorPos.X, anchorPos.Y);

                float distance = delta.Length();

                delta *= radius / distance;
                newPosition = (Vector2i)new Vector2f(anchorPos.X + delta.X, anchorPos.Y + delta.Y);
            }

            Vector2i result = CalculateCirclePositionBorder(newPosition, new Vector2i(0, 0), pos, size);
            return result;

        }

        public static Vector2i SnapCircleToGrid(Vector2i position)
        {

            Size Grid = new Size(16, 12);
            Size HalfGrid = new Size(Grid.Width / 2, Grid.Height / 2);

            int snapX = ((position.X + HalfGrid.Width) / Grid.Width) * Grid.Width;
            int snapY = ((position.Y + HalfGrid.Height) / Grid.Height) * Grid.Height;

            return new Vector2i(snapX, snapY);
        }

        public static Vector2i SnapToGrid(Vector2i position, int gridSize)
        {
            // Obliczanie nowych współrzędnych zaokrąglonych do najbliższej komórki siatki
            int snappedX = (int)(Math.Round((float)position.X / gridSize) * gridSize);
            int snappedY = (int)(Math.Round((float)position.Y / gridSize) * gridSize);

            return new Vector2i(snappedX, snappedY);
        }
    }
}
