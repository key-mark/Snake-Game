using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public enum GameState { Ready, Playing, GameOver }

    public class GameManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float tickInterval = 0.15f;
        [SerializeField] private int scorePerFood = 10;

        public GameState State { get; private set; } = GameState.Ready;

        private Snake _snake;
        private FoodSpawner _foodSpawner;
        private ScoreManager _scoreManager;
        private UIManager _uiManager;
        private float _tickTimer;
        private Transform _segmentsContainer;

        private void Awake()
        {
            BuildScene();
        }

        private void Start()
        {
            _uiManager.UpdateHighScore(_scoreManager.HighScore);
            EnterReady();
        }

        private void Update()
        {
            switch (State)
            {
                case GameState.Ready:
                    if (AnyDirectionKeyDown())
                        StartGame();
                    break;

                case GameState.Playing:
                    _tickTimer += Time.deltaTime;
                    while (_tickTimer >= tickInterval)
                    {
                        _tickTimer -= tickInterval;
                        GameTick();
                        if (State != GameState.Playing)
                            break;
                    }
                    break;

                case GameState.GameOver:
                    if (AnyDirectionKeyDown())
                        Restart();
                    break;
            }
        }

        // ── Scene Construction ──────────────────────────────────────────────

        private void BuildScene()
        {
            // Camera
            var cam = Camera.main;
            if (cam == null)
            {
                var camObj = new GameObject("Main Camera");
                cam = camObj.AddComponent<Camera>();
            }
            cam.orthographic = true;
            cam.orthographicSize = Mathf.Max(GridConfig.Width, GridConfig.Height) * GridConfig.CellSize / 2f + 1f;
            cam.transform.position = new Vector3(0, 0, -10);
            cam.backgroundColor = new Color(0.05f, 0.05f, 0.08f);
            cam.clearFlags = CameraClearFlags.SolidColor;

            // Game Container (holds all gameplay objects)
            var gameContainer = new GameObject("GameContainer");

            // UIManager (must exist before BuildUI)
            var uiObj = new GameObject("UIManager");
            uiObj.transform.SetParent(gameContainer.transform);
            _uiManager = uiObj.AddComponent<UIManager>();

            // Canvas + UI (needs _uiManager to be initialized)
            var canvasObj = new GameObject("Canvas");
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            BuildUI(canvasObj.transform);

            // Snake
            var snakeObj = new GameObject("Snake");
            snakeObj.transform.SetParent(gameContainer.transform);
            _snake = snakeObj.AddComponent<Snake>();
            _segmentsContainer = new GameObject("Segments").transform;
            _segmentsContainer.SetParent(snakeObj.transform);

            // FoodSpawner
            var foodObj = new GameObject("FoodSpawner");
            foodObj.transform.SetParent(gameContainer.transform);
            _foodSpawner = foodObj.AddComponent<FoodSpawner>();

            // ScoreManager
            var scoreObj = new GameObject("ScoreManager");
            scoreObj.transform.SetParent(gameContainer.transform);
            _scoreManager = scoreObj.AddComponent<ScoreManager>();

            // GridRenderer (visual reference lines)
            var gridObj = new GameObject("GridRenderer");
            gridObj.transform.SetParent(gameContainer.transform);
            gridObj.AddComponent<GridRenderer>();
        }

        private void BuildUI(Transform canvasTransform)
        {
            // Score text (top-left)
            var scoreGO = CreateUIText(canvasTransform, "ScoreText", "Score: 0",
                30, TextAnchor.MiddleLeft, new Vector2(0, 1), new Vector2(0, 1),
                new Vector2(20, -20));
            _uiManager.ScoreText = scoreGO.GetComponent<Text>();

            // High score text (top-right)
            var highScoreGO = CreateUIText(canvasTransform, "HighScoreText", "Best: 0",
                30, TextAnchor.MiddleRight, new Vector2(1, 1), new Vector2(1, 1),
                new Vector2(-20, -20));
            _uiManager.HighScoreText = highScoreGO.GetComponent<Text>();

            // Start prompt (center)
            var startPrompt = new GameObject("StartPrompt");
            startPrompt.transform.SetParent(canvasTransform, false);
            var startRect = startPrompt.AddComponent<RectTransform>();
            startRect.anchorMin = new Vector2(0.5f, 0.5f);
            startRect.anchorMax = new Vector2(0.5f, 0.5f);
            startRect.sizeDelta = new Vector2(600, 100);
            startRect.anchoredPosition = Vector2.zero;
            var startText = startPrompt.AddComponent<Text>();
            startText.text = "Press Arrow Keys or WASD to Start";
            startText.fontSize = 32;
            startText.alignment = TextAnchor.MiddleCenter;
            startText.color = Color.white;
            _uiManager.StartPrompt = startPrompt;
            _uiManager.StartPromptText = startText;

            // GameOver panel (initially hidden)
            var goPanel = new GameObject("GameOverPanel");
            goPanel.transform.SetParent(canvasTransform, false);
            var goRect = goPanel.AddComponent<RectTransform>();
            goRect.anchorMin = Vector2.zero;
            goRect.anchorMax = Vector2.one;
            goRect.offsetMin = Vector2.zero;
            goRect.offsetMax = Vector2.zero;

            var goBg = goPanel.AddComponent<Image>();
            goBg.color = new Color(0, 0, 0, 0.75f);

            // GameOver title
            var titleGO = CreateUIText(goPanel.transform, "GameOverTitle", "GAME OVER",
                56, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(0, 80), 500, 80);
            titleGO.GetComponent<Text>().color = new Color(0.9f, 0.25f, 0.25f);

            // Final score
            var finalScoreGO = CreateUIText(goPanel.transform, "FinalScoreText", "Score: 0",
                36, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(0, 20), 500, 50);
            _uiManager.FinalScoreText = finalScoreGO.GetComponent<Text>();

            // GameOver high score
            var goHighScoreGO = CreateUIText(goPanel.transform, "GameOverHighScoreText", "Best: 0",
                32, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(0, -30), 500, 50);
            _uiManager.GameOverHighScoreText = goHighScoreGO.GetComponent<Text>();

            // Restart prompt
            CreateUIText(goPanel.transform, "RestartPrompt", "Press Arrow Keys or WASD to Restart",
                24, TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(0, -90), 500, 50);

            _uiManager.GameOverPanel = goPanel;
            goPanel.SetActive(false);
        }

        private static GameObject CreateUIText(Transform parent, string name, string text,
            int fontSize, TextAnchor alignment,
            Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition,
            float width = 500, float height = 50)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.sizeDelta = new Vector2(width, height);
            rt.anchoredPosition = anchoredPosition;
            var txt = go.AddComponent<Text>();
            txt.text = text;
            txt.fontSize = fontSize;
            txt.alignment = alignment;
            txt.color = Color.white;
            return go;
        }

        // ── State Machine ───────────────────────────────────────────────────

        private void EnterReady()
        {
            State = GameState.Ready;
            _snake.Initialize(_segmentsContainer);
            _scoreManager.ResetScore();
            _uiManager.UpdateScore(0);
            _uiManager.ShowStartPrompt(true);
            _uiManager.HideGameOver();
            _foodSpawner.HideFood();
        }

        private void StartGame()
        {
            State = GameState.Playing;
            _uiManager.ShowStartPrompt(false);
            _snake.Initialize(_segmentsContainer);
            _foodSpawner.SpawnFood(_snake.Segments);
            _tickTimer = 0f;
        }

        private void GameTick()
        {
            _snake.Move();
            Vector2Int headPos = _snake.Segments[0];

            if (_snake.CheckWallCollision(headPos) || _snake.CheckSelfCollision(headPos))
            {
                EndGame();
                return;
            }

            if (headPos == _foodSpawner.FoodPosition)
            {
                _snake.Grow();
                _scoreManager.AddScore(scorePerFood);
                _uiManager.UpdateScore(_scoreManager.CurrentScore);
                _foodSpawner.SpawnFood(_snake.Segments);
            }
        }

        private void EndGame()
        {
            State = GameState.GameOver;
            bool isNew = _scoreManager.SaveHighScore();
            _uiManager.ShowGameOver(_scoreManager.CurrentScore, _scoreManager.HighScore, isNew);
        }

        private void Restart()
        {
            _snake.Clear();
            EnterReady();
            StartGame();
        }

        private static bool AnyDirectionKeyDown()
        {
            return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
                   Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) ||
                   Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) ||
                   Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
        }
    }
}
