using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private Spawner LevelGenerator;
    [SerializeField] private GameObject GamePrefab;
    private GameObject Game;
    [SerializeField] private int ClassicLevelCount=4;
    [SerializeField] private bool[] isLevelAvailable;
    private int LevelNumber=1,CurrentLevel=-1;

    //GETTERS

        public bool GetisLevelAvailable(int index){return isLevelAvailable[index];}//Getter For isLevelAvailable
        public int GetCurrentLevel(){return CurrentLevel;}//Getter For CurrentLevel

    //SETTERS

        public void InitLevelNumber(){LevelNumber=-1;}//Setter To Reset LevelNumber
        public void SetisLevelAvailable(bool value){isLevelAvailable[LevelNumber]=value;}


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

        DisplayGame(true);
        LevelNumber=1;
        SpawnLevel(Spawner.Difficulty.Easy);
        GameManager.instance.ResetGame(5);
        GameManager.instance.SetCurrentGameMode(GameManager.GameMode.Arcade);
        UIManager.instance.DisplayGameUI(true);
        UIManager.instance.SetLevelDisplay("Stage :",LevelNumber.ToString());
        UIManager.instance.UpdateScoreDisplay(0);

    }

    public void NextStage(GameManager.GameMode Current)
    {

        switch(Current)
        {

            case GameManager.GameMode.Arcade :

                UIManager.instance.SetLevelDisplay("Stage :",LevelNumber.ToString());
                LevelGenerator.SpawnMap();
                GameManager.instance.IncreaseLives();

            break;

            case GameManager.GameMode.Classic :

                
                if(LevelNumber<ClassicLevelCount)
                {

                    GameManager.instance.ResetGame(3);
                    GoToLevel(LevelNumber);
                    UIManager.instance.SetLevelDisplay("Level :",LevelNumber.ToString());

                }else
                {

                    SceneManager.LoadScene("Credits");
                    ResetDisplay();
                    UIManager.instance.HideUI();

                }

                CurrentLevel++;
                

            break;

            default :

                Debug.Log("This Game Mode Doesn't Exist Yet");

            break;

        }


        UIManager.instance.DisplayWinPanel(false);
        LevelNumber++;

        

    }

    public void SpawnLevel(Spawner.Difficulty DifficultyToApply)
    {

        LevelGenerator.SetDifficulty(DifficultyToApply);
        LevelGenerator.SpawnMap();

    }


    public void DisplayGame(bool isDisplay)
    {

        if(Game == null)
            Game=Instantiate(GamePrefab);

        Game.gameObject.SetActive(isDisplay);

    }

    public void GoToLevel(int LevelNumber)
    {

        DisplayGame(true);
        string LevelName = "Level "+LevelNumber.ToString();
        SceneManager.LoadScene("Classic");
        SceneManager.LoadScene(LevelName,LoadSceneMode.Additive);
        GameManager.instance.ResetGame(3);
        GameManager.instance.SetCurrentGameMode(GameManager.GameMode.Classic);
        UIManager.instance.DisplayGameUI(true);
        UIManager.instance.SetLevelDisplay("Level :",(LevelNumber+1).ToString());
        UIManager.instance.UpdateScoreDisplay(0);
        CurrentLevel=LevelNumber;

    }

}
