using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private Color foodColor = new Color(0.95f, 0.25f, 0.25f);

        public Vector2Int FoodPosition { get; private set; }

        private GameObject _foodObject;
        private SpriteRenderer _foodRenderer;

        private void Awake()
        {
            _foodObject = new GameObject("Food");
            _foodObject.transform.SetParent(transform);
            _foodRenderer = _foodObject.AddComponent<SpriteRenderer>();
            _foodRenderer.sprite = SpriteGenerator.GetSquareSprite(foodColor);
            _foodRenderer.sortingOrder = 2;
            _foodObject.SetActive(false);
        }

        public void SpawnFood(List<Vector2Int> occupiedPositions)
        {
            var available = new List<Vector2Int>();
            for (int x = 0; x < GridConfig.Width; x++)
            {
                for (int y = 0; y < GridConfig.Height; y++)
                {
                    var pos = new Vector2Int(x, y);
                    if (!occupiedPositions.Contains(pos))
                        available.Add(pos);
                }
            }

            if (available.Count == 0)
            {
                _foodObject.SetActive(false);
                return;
            }

            FoodPosition = available[Random.Range(0, available.Count)];
            _foodObject.transform.position = Snake.GridToWorld(FoodPosition);
            _foodObject.SetActive(true);
        }

        public void HideFood()
        {
            if (_foodObject != null)
                _foodObject.SetActive(false);
        }
    }
}
