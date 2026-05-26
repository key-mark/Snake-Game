using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public static class SpriteGenerator
    {
        private static readonly Dictionary<Color, Sprite> _cache = new Dictionary<Color, Sprite>();

        public static Sprite GetSquareSprite(Color color)
        {
            if (_cache.TryGetValue(color, out var cached))
                return cached;

            int size = 16;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            var pixels = new Color[size * size];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = color;

            tex.SetPixels(pixels);
            tex.Apply();

            var sprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
            _cache[color] = sprite;
            return sprite;
        }
    }
}
