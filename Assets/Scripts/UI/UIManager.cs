using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public class UIManager : MonoBehaviour
    {
        public Text ScoreText { get; set; }
        public Text HighScoreText { get; set; }
        public GameObject GameOverPanel { get; set; }
        public Text FinalScoreText { get; set; }
        public Text GameOverHighScoreText { get; set; }
        public GameObject StartPrompt { get; set; }
        public Text StartPromptText { get; set; }

        public void UpdateScore(int score)
        {
            if (ScoreText != null)
                ScoreText.text = $"Score: {score}";
        }

        public void UpdateHighScore(int highScore)
        {
            if (HighScoreText != null)
                HighScoreText.text = $"Best: {highScore}";
        }

        public void ShowStartPrompt(bool show)
        {
            if (StartPrompt != null)
                StartPrompt.SetActive(show);
            if (StartPromptText != null)
                StartPromptText.text = "Press Arrow Keys or WASD to Start";
        }

        public void ShowGameOver(int score, int highScore, bool isNewHighScore)
        {
            if (GameOverPanel != null)
                GameOverPanel.SetActive(true);
            if (FinalScoreText != null)
                FinalScoreText.text = $"Score: {score}";
            if (GameOverHighScoreText != null)
                GameOverHighScoreText.text = isNewHighScore
                    ? $"NEW HIGH SCORE: {highScore}!"
                    : $"Best: {highScore}";
        }

        public void HideGameOver()
        {
            if (GameOverPanel != null)
                GameOverPanel.SetActive(false);
        }
    }
}
