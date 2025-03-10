using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [Header("Scene Names")]
    [Space(10)]

        public const string GameMode="GameMode";
        public const string ClassicSelect="ClassicSelection";
        public const string Arcade="Arcade";
        public const string Classic="Classic";
        public const string Menu="Menu";
        public const string Settings="Settings";
        public const string Credits="Credits";


    //SCENE SELECTOR

        public void GoToGame()
        {

            SceneManager.LoadScene(GameMode);

        }

        public void GoToClassic()
        {

            SceneManager.LoadScene(ClassicSelect);

        }

        public void GoToArcade()
        {

            SceneManager.LoadScene(Arcade);

        }

        public void GoToClassicGame()
        {

            SceneManager.LoadScene(Classic);

        }

        public void GoToMenu()
        {

            SceneManager.LoadScene(Menu);
            HideUI();

        }

        public void GoToSettings()
        {

            SceneManager.LoadScene(Settings);
            HideUI();

        }

        public void GoToCredits()
        {

            SceneManager.LoadScene(Credits);
            HideUI();

        }

        public void GoToNextLevel()
        {

            LevelManager.instance.NextStage(GameManager.instance.GetCurrentGameMode());

        }

        public void Quit()
        {

            Application.Quit();

        }


    //DATA MODIFIERS

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

            switch(GameManager.instance.GetCurrentGameMode())
            {

                case GameManager.GameMode.Classic :

                    UIManager.instance.HideUI();
                    LevelManager.instance.GoToLevel(LevelManager.instance.GetCurrentLevel());
                
                break;

                case GameManager.GameMode.Arcade :

                    UIManager.instance.DisplayScoreRecord(true);
                    UIManager.instance.SetScoreRecord();
                    UIManager.instance.HideUI();
                    LevelManager.instance.LaunchArcadeMode();

                break;

                default :

                    Debug.Log("This Game Mode Doesn't Exist Yet");

                break;

            }

        }

        private void HideUI()
        {

            UIManager.instance.HideUI();

            if(LevelManager.instance!=null)
            {

                LevelManager.instance.ResetDisplay();

            }

        }

}
