using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Core
{
    public class MainMenuUiController : MonoBehaviour
    {
        private Button startButton;
        private Button endlessModeButton;
        private Button quitButton;


        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            startButton = root.Q<Button>("Start-button");
            endlessModeButton = root.Q<Button>("Endless-button");
            quitButton = root.Q<Button>("Quit-button");

          
        }

        private void StartButtonPressed()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void EndlessButtonPressed()
        {
            SceneManager.LoadSceneAsync("EndlessMode");
        }

        private void QuitButtonPressed()
        {
            Application.Quit();
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
