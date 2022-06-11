using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        
        
        
        
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
        
        public void LoadNextScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}