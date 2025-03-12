using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private Spawner LevelGenerator;
    [SerializeField] private GameObject GamePrefab;
    [SerializeField] private GameObject Game;
    [SerializeField] private int ClassicLevelCount=4;
    [SerializeField] private bool[] isLevelAvailable;
    private int CurrentLevel=-1;

    //GETTERS

        public bool GetisLevelAvailable(int index){return isLevelAvailable[index];}//Getter For isLevelAvailable
        public int GetCurrentLevel(){return CurrentLevel;}//Getter For CurrentLevel

    //SETTERS

        public void InitCurrentLevel(){CurrentLevel=-1;}//Setter To Reset CurrentLevel
        public void SetisLevelAvailable(bool value){isLevelAvailable[CurrentLevel+1]=value;}


    public static LevelManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this);
        }//Allow To Call This From Any Class

    public void Start()
    {

        DisplayGame(false);
        Game.transform.SetParent(this.transform);
        isLevelAvailable=new bool[ClassicLevelCount];
        isLevelAvailable[0]=true;
        for(int i=1;i<ClassicLevelCount;++i)
        {

            isLevelAvailable[i]=false;

        }

    }


    public void ResetDisplay()
    {


        DisplayGame(false);

    }


    public void LaunchArcadeMode()
    {

        GameManager.instance.LaunchGame();
        DisplayGame(true);
        CurrentLevel=1;
        SpawnLevel(LevelGenerator.GetDifficulty());
        GameManager.instance.ResetGame(5);
        GameManager.instance.SetCurrentGameMode(GameManager.GameMode.Arcade);
        UIManager.instance.DisplayGameUI(true);
        UIManager.instance.SetLevelDisplay("Stage :",(CurrentLevel+1).ToString());
        UIManager.instance.UpdateScoreDisplay(0);

    }

    public void NextStage(GameManager.GameMode CurrentGameMode)
    {

        CurrentLevel++;

        switch(CurrentGameMode)
        {

            case GameManager.GameMode.Arcade :

                UIManager.instance.SetLevelDisplay("Stage :",(CurrentLevel+1).ToString());
                LevelGenerator.SpawnMap();
                GameManager.instance.IncreaseLives(3);

            break;

            case GameManager.GameMode.Classic :

                
                if(CurrentLevel<ClassicLevelCount)
                {

                    GameManager.instance.ResetGame(3);
                    GoToLevel(CurrentLevel);
                    UIManager.instance.SetLevelDisplay("Level :",(CurrentLevel+1).ToString());

                }else
                {

                    SceneManager.LoadScene("Credits");
                    ResetDisplay();
                    UIManager.instance.HideUI();
                    CurrentLevel=-1;

                }

                
                

            break;

            default :

                Debug.Log("This Game Mode Doesn't Exist Yet");

            break;

        }


        UIManager.instance.DisplayWinPanel(false);
        

    }

    public void SpawnLevel(Spawner.Difficulty DifficultyToApply)
    {

        LevelGenerator.SetDifficulty(DifficultyToApply);
        LevelGenerator.SpawnMap();

    }


    public void DisplayGame(bool isDisplay)
    {

        Debug.Log(Game);

        if(Game==null)
            Game=Instantiate(GamePrefab);

        Debug.Log(Game);

        Game.gameObject.SetActive(isDisplay);

        Debug.Log("here");

    }

    public void GoToLevel(int LevelToGo)
    {

        CurrentLevel=LevelToGo;
        DisplayGame(true);
        SceneManager.LoadScene("Classic");
        SceneManager.LoadScene("Level "+CurrentLevel,LoadSceneMode.Additive);
        GameManager.instance.ResetGame(3);
        GameManager.instance.SetCurrentGameMode(GameManager.GameMode.Classic);
        UIManager.instance.DisplayGameUI(true);
        UIManager.instance.SetLevelDisplay("Level :",(CurrentLevel+1).ToString());
        UIManager.instance.UpdateScoreDisplay(0);

    }

}
