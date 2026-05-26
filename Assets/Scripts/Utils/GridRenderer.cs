using UnityEngine;

namespace SnakeGame
{
    public class GridRenderer : MonoBehaviour
    {
        [SerializeField] private Color borderColor = new Color(0.3f, 0.3f, 0.4f, 0.8f);
        [SerializeField] private Color gridLineColor = new Color(0.1f, 0.1f, 0.15f, 0.3f);
        [SerializeField] private float lineWidth = 0.05f;

        private void Start()
        {
            DrawGrid();
        }

        public void DrawGrid()
        {
            DrawBorder();
            DrawGridLines();
        }

        private void DrawBorder()
        {
            float w = GridConfig.Width * GridConfig.CellSize;
            float h = GridConfig.Height * GridConfig.CellSize;
            float ox = -w / 2f;
            float oy = -h / 2f;

            DrawLine(new Vector2(ox, oy), new Vector2(ox + w, oy), borderColor);          // bottom
            DrawLine(new Vector2(ox + w, oy), new Vector2(ox + w, oy + h), borderColor);  // right
            DrawLine(new Vector2(ox + w, oy + h), new Vector2(ox, oy + h), borderColor);  // top
            DrawLine(new Vector2(ox, oy + h), new Vector2(ox, oy), borderColor);           // left
        }

        private void DrawGridLines()
        {
            float w = GridConfig.Width * GridConfig.CellSize;
            float h = GridConfig.Height * GridConfig.CellSize;
            float ox = -w / 2f;
            float oy = -h / 2f;

            for (int x = 1; x < GridConfig.Width; x++)
            {
                float px = ox + x * GridConfig.CellSize;
                DrawLine(new Vector2(px, oy), new Vector2(px, oy + h), gridLineColor);
            }

            for (int y = 1; y < GridConfig.Height; y++)
            {
                float py = oy + y * GridConfig.CellSize;
                DrawLine(new Vector2(ox, py), new Vector2(ox + w, py), gridLineColor);
            }
        }

        private void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            var lineObj = new GameObject("GridLine");
            lineObj.transform.SetParent(transform);

            var lr = lineObj.AddComponent<LineRenderer>();
            lr.useWorldSpace = true;
            lr.positionCount = 2;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.startColor = color;
            lr.endColor = color;
            lr.sortingOrder = 0;

            // Use built-in default material
            lr.material = new Material(Shader.Find("Sprites/Default"));
        }
    }
}
