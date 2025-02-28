using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //SCENE SELECTOR

        public void GoToGame()
        {

            SceneManager.LoadScene("Game");

        }

        public void GoToMenu()
        {

            SceneManager.LoadScene("Menu");

        }

        public void GoToSettings()
        {

            SceneManager.LoadScene("Settings");

        }

        public void GoToCredits()
        {

            SceneManager.LoadScene("Credits");

        }

        public void Quit()
        {

            Application.Quit();

        }

        public void Save()
        {
            
            GameManager.instance.SaveData();

        }

        public void Resume()
        {

            UIManager.instance.CloseMenu();

        }

}
