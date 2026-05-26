using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeGame.Editor
{
    public static class SceneSetupEditor
    {
        private const string ScenePath = "Assets/Scenes/MainScene.unity";

        [MenuItem("Snake Game/Setup Scene")]
        public static void SetupScene()
        {
            // Create new scene
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // Ensure 2D mode
            EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;

            // Create GameManager GameObject (builds entire scene at runtime)
            var gmObj = new GameObject("GameManager");
            gmObj.AddComponent<GameManager>();

            // Save scene
            EditorSceneManager.SaveScene(scene, ScenePath);
            Debug.Log($"Snake Game scene created at: {ScenePath}");
            Debug.Log("Press Play to start the game!");
        }

        [MenuItem("Snake Game/Setup Scene", validate = true)]
        public static bool SetupSceneValidate()
        {
            return !EditorApplication.isPlaying;
        }
    }
}
