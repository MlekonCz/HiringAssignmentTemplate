using System;
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
        
     
        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _startButton = root.Q<Button>("Start-button");
            _endlessModeButton = root.Q<Button>("Endless-button");
            _quitButton = root.Q<Button>("Quit-button");
        }
        
        

        private void StartButtonPressed()
        {
            PersistentObjects.Instance.SceneLoader.LoadScene(PersistentObjects.Instance.GameManager.AccessibleLevel);
        }

        private void EndlessButtonPressed()
        {
            PersistentObjects.Instance.SceneLoader.LoadScene("EndlessMode");
        }

        private void QuitButtonPressed()
        {
            PersistentObjects.Instance.SceneLoader.QuitGame();
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
