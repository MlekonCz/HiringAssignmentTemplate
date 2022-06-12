using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scenes
{
    public class LevelUiController : MonoBehaviour
    {
        private VisualElement _pauseMenu;
        private VisualElement _wonMenu;
        private VisualElement _lostMenu;

        private Button _pauseButton;
        private Button _continueButton;
        private Button _nextLevelButton;
        private Button _restartButton;
        
        private Button _mainMenuButton_P;
        private Button _mainMenuButton_W;
        private Button _mainMenuButton_L;


        private VisualElement _root;

        private SceneLoader _sceneLoader;
        
        private void Awake()
        {
            _sceneLoader = FindObjectOfType<SceneLoader>();
            _root = GetComponent<UIDocument>().rootVisualElement;
            AssignUiElements();
        }

        private void Start()
        {
            _wonMenu.visible = false;
            _lostMenu.visible = false;
            _pauseMenu.visible = false;
        }

        private void AssignUiElements()
        {
            _wonMenu = _root.Q<VisualElement>("Won-menu");
            _lostMenu = _root.Q<VisualElement>("Lost-menu");
            _pauseMenu = _root.Q<VisualElement>("Pause-menu");

            _pauseButton = _root.Q<Button>("Pause-button");
            
            _continueButton = _root.Q<Button>("Continue-button");
            _nextLevelButton = _root.Q<Button>("NextLevel-button");
            _restartButton = _root.Q<Button>("Restart-button");
            
            _mainMenuButton_P = _root.Q<Button>("Quit-button-P");
            _mainMenuButton_W = _root.Q<Button>("Quit-button-W");
            _mainMenuButton_L = _root.Q<Button>("Quit-button-L");
            
        }

        public void ShowWonMenu()
        {
            StartCoroutine(DelayedWindowPopUp(_wonMenu, 2f));
        }
        public void ShowLostMenu()
        {
            StartCoroutine(DelayedWindowPopUp(_lostMenu, 2f));
        }
        private void ShowPauseMenu()
        {
            _pauseMenu.visible = true;
            Time.timeScale = 0;
        }

        private IEnumerator DelayedWindowPopUp(VisualElement visualElement, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            visualElement.visible = true;
            Time.timeScale = 0;
        }

        private void HidePauseMenu()
        {
            Time.timeScale = 1;
            _pauseMenu.visible = false;
        }

        private void LoadNextLevel()
        {
            Time.timeScale = 1;
            _sceneLoader.LoadNextScene();
            
        }
        private void RestartLevel()
        {
            Time.timeScale = 1; 
            _sceneLoader.RestartCurrentScene();
        }
        private void QuitToMainMenu()
        {
            Time.timeScale = 1;
            _sceneLoader.LoadMainMenu();
        }

        private void OnEnable()
        {
            _mainMenuButton_L.clicked += QuitToMainMenu;
            _mainMenuButton_P.clicked += QuitToMainMenu;
            _mainMenuButton_W.clicked += QuitToMainMenu;

            _pauseButton.clicked += ShowPauseMenu;
            _continueButton.clicked += HidePauseMenu;

            _nextLevelButton.clicked += LoadNextLevel;
            _restartButton.clicked += RestartLevel;
        }

        private void OnDisable()
        {
            _mainMenuButton_L.clicked -= QuitToMainMenu;
            _mainMenuButton_P.clicked -= QuitToMainMenu;
            _mainMenuButton_W.clicked -= QuitToMainMenu;          
            
            _pauseButton.clicked -= ShowPauseMenu;
            _continueButton.clicked -= HidePauseMenu;

            _nextLevelButton.clicked -= LoadNextLevel;
            _restartButton.clicked -= RestartLevel;

        }
    }
}
