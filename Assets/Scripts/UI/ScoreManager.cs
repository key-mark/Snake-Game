using UnityEngine;

namespace SnakeGame
{
    public class ScoreManager : MonoBehaviour
    {
        private const string HighScoreKey = "SnakeHighScore";

        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }

        private void Awake()
        {
            HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        }

        public void AddScore(int amount)
        {
            CurrentScore += amount;
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }

        public bool SaveHighScore()
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt(HighScoreKey, HighScore);
                PlayerPrefs.Save();
                return true;
            }
            return false;
        }
    }
}
