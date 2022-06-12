using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scenes
{
    public class MainMenuUiController : MonoBehaviour
    {
        private Button startButton;
        private Button endlessModeButton;
        private Button quitButton;

        private SceneLoader _sceneLoader;

        private int _accessibleLevel;
        public void SetAccessibleLevel(int level){_accessibleLevel = level;}
        

        private void Awake()
        {
            _sceneLoader = FindObjectOfType<SceneLoader>();
            var root = GetComponent<UIDocument>().rootVisualElement;

            startButton = root.Q<Button>("Start-button");
            endlessModeButton = root.Q<Button>("Endless-button");
            quitButton = root.Q<Button>("Quit-button");
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
            startButton.clicked += StartButtonPressed;
            endlessModeButton.clicked += EndlessButtonPressed;
            quitButton.clicked += QuitButtonPressed;
        }
        private void OnDisable()
        {
            startButton.clicked -= StartButtonPressed;
            endlessModeButton.clicked -= EndlessButtonPressed;
            quitButton.clicked -= QuitButtonPressed;
        }
    }
}
