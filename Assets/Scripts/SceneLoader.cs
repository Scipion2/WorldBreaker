using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //SCENE SELECTOR

        public void GoToGame()
        {

            SceneManager.LoadScene("Game");
            if(LevelManager.instance!=null)
                LevelManager.instance.DisplayGameModeSelection();

        }

        public void GoToMenu()
        {

            SceneManager.LoadScene("Menu");
            HideGame();

        }

        public void GoToSettings()
        {

            SceneManager.LoadScene("Settings");
            HideGame();

        }

        public void GoToCredits()
        {

            SceneManager.LoadScene("Credits");
            HideGame();

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

        public void Retry()
        {

            //

        }

        public void GoToNextLevel()
        {

            LevelManager.instance.NextStage(GameManager.instance.GetCurrentGameMode());

        }

        private void HideGame()
        {

            UIManager.instance.HideAll();

            if(LevelManager.instance!=null)
            {

                LevelManager.instance.ResetDisplay();

            }

        }

}
