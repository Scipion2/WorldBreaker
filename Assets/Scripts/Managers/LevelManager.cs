using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private Spawner LevelGenerator;
    [SerializeField] private GameObject GameModeSelection,LevelSelection,Game;
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

        GameModeSelection.gameObject.SetActive(true);
        LevelSelection.gameObject.SetActive(false);
        Game.gameObject.SetActive(false);
        isLevelAvailable=new bool[ClassicLevelCount];
        isLevelAvailable[0]=true;
        for(int i=1;i<ClassicLevelCount;++i)
        {

            isLevelAvailable[i]=false;

        }

    }

    public void DisplayGameModeSelection()
    {

        GameModeSelection.gameObject.SetActive(true);

    }

    public void ResetDisplay()
    {

        GameModeSelection.gameObject.SetActive(false);
        LevelSelection.gameObject.SetActive(false);
        Game.gameObject.SetActive(false);

    }


    public void LaunchArcadeMode()
    {

        GameModeSelection.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
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
                    UIManager.instance.HideAll();

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


    public void DisplayLevelSelection()
    {

        LevelSelection.gameObject.SetActive(true);
        GameModeSelection.gameObject.SetActive(false);

    }

    public void GoToLevel(int LevelNumber)
    {

        LevelSelection.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
        string LevelName = "Level "+LevelNumber.ToString();
        SceneManager.LoadScene(LevelName,LoadSceneMode.Additive);
        GameManager.instance.ResetGame(3);
        GameManager.instance.SetCurrentGameMode(GameManager.GameMode.Classic);
        UIManager.instance.DisplayGameUI(true);
        UIManager.instance.SetLevelDisplay("Level :",(LevelNumber+1).ToString());
        UIManager.instance.UpdateScoreDisplay(0);
        CurrentLevel=LevelNumber;

    }

}
