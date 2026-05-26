using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class Snake : MonoBehaviour
    {
        [Header("Colors")]
        [SerializeField] private Color headColor = new Color(0.2f, 0.9f, 0.2f);
        [SerializeField] private Color bodyColor = new Color(0.1f, 0.65f, 0.1f);
        [SerializeField] private Color bodyAltColor = new Color(0.12f, 0.55f, 0.12f);

        public List<Vector2Int> Segments { get; private set; } = new List<Vector2Int>();
        public Vector2Int Direction { get; private set; } = Vector2Int.right;
        public Vector2Int CurrentDirection => Direction;

        private Vector2Int _nextDirection = Vector2Int.right;
        private bool _shouldGrow;
        private readonly List<GameObject> _bodyObjects = new List<GameObject>();
        private Transform _segmentsContainer;

        public void Initialize(Transform container)
        {
            _segmentsContainer = container;
            Segments.Clear();

            int cx = GridConfig.Width / 2;
            int cy = GridConfig.Height / 2;
            Segments.Add(new Vector2Int(cx, cy));
            Segments.Add(new Vector2Int(cx - 1, cy));
            Segments.Add(new Vector2Int(cx - 2, cy));

            Direction = Vector2Int.right;
            _nextDirection = Vector2Int.right;
            _shouldGrow = false;

            RenderAllSegments();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                SetDirection(Vector2Int.up);
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                SetDirection(Vector2Int.down);
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                SetDirection(Vector2Int.left);
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                SetDirection(Vector2Int.right);
        }

        public void SetDirection(Vector2Int dir)
        {
            if (dir + Direction != Vector2Int.zero)
                _nextDirection = dir;
        }

        public void Move()
        {
            Direction = _nextDirection;
            Vector2Int newHead = Segments[0] + Direction;
            Segments.Insert(0, newHead);

            if (!_shouldGrow)
            {
                Segments.RemoveAt(Segments.Count - 1);
            }
            else
            {
                _shouldGrow = false;
            }

            RenderAllSegments();
        }

        public void Grow()
        {
            _shouldGrow = true;
        }

        public bool CheckWallCollision(Vector2Int headPos)
        {
            return headPos.x < 0 || headPos.x >= GridConfig.Width ||
                   headPos.y < 0 || headPos.y >= GridConfig.Height;
        }

        public bool CheckSelfCollision(Vector2Int headPos)
        {
            for (int i = 1; i < Segments.Count; i++)
            {
                if (Segments[i] == headPos)
                    return true;
            }
            return false;
        }

        public static Vector3 GridToWorld(Vector2Int gridPos)
        {
            float x = (gridPos.x - GridConfig.Width / 2f + 0.5f) * GridConfig.CellSize;
            float y = (gridPos.y - GridConfig.Height / 2f + 0.5f) * GridConfig.CellSize;
            return new Vector3(x, y, 0);
        }

        public void Clear()
        {
            foreach (var obj in _bodyObjects)
            {
                if (obj != null) Destroy(obj);
            }
            _bodyObjects.Clear();
            Segments.Clear();
        }

        private void RenderAllSegments()
        {
            while (_bodyObjects.Count < Segments.Count)
            {
                var obj = new GameObject("Segment");
                obj.transform.SetParent(_segmentsContainer);
                var sr = obj.AddComponent<SpriteRenderer>();
                sr.sprite = SpriteGenerator.GetSquareSprite(Color.white);
                sr.sortingOrder = 1;
                _bodyObjects.Add(obj);
            }

            for (int i = 0; i < _bodyObjects.Count; i++)
            {
                _bodyObjects[i].SetActive(i < Segments.Count);
            }

            for (int i = 0; i < Segments.Count; i++)
            {
                var go = _bodyObjects[i];
                go.transform.position = GridToWorld(Segments[i]);
                var sr = go.GetComponent<SpriteRenderer>();

                if (i == 0)
                    sr.color = headColor;
                else if (i % 2 == 0)
                    sr.color = bodyColor;
                else
                    sr.color = bodyAltColor;
            }
        }
    }
}
