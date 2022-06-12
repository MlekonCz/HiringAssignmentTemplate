using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scenes
{
    public class MainMenuUiController : MonoBehaviour
    {
        private Button _startButton;
        private Button _endlessModeButton;
        private Button _quitButton;

        private SceneLoader _sceneLoader;

        private int _accessibleLevel;
        public void SetAccessibleLevel(int level){_accessibleLevel = level;}
        
        private void Awake()
        {
            _sceneLoader = FindObjectOfType<SceneLoader>();
            var root = GetComponent<UIDocument>().rootVisualElement;

            _startButton = root.Q<Button>("Start-button");
            _endlessModeButton = root.Q<Button>("Endless-button");
            _quitButton = root.Q<Button>("Quit-button");
        }

        private void StartButtonPressed()
        {
            _sceneLoader.LoadScene(_accessibleLevel);
        }

        private void EndlessButtonPressed()
        {
            _sceneLoader.LoadScene("EndlessMode");
        }

        private void QuitButtonPressed()
        {
            _sceneLoader.QuitGame();
        }
        private void OnEnable()
        {
            _startButton.clicked += StartButtonPressed;
            _endlessModeButton.clicked += EndlessButtonPressed;
            _quitButton.clicked += QuitButtonPressed;
        }
        private void OnDisable()
        {
            _startButton.clicked -= StartButtonPressed;
            _endlessModeButton.clicked -= EndlessButtonPressed;
            _quitButton.clicked -= QuitButtonPressed;
        }
    }
}
