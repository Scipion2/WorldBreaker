using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private Spawner LevelGenerator;
    [SerializeField] private GameObject GameModeSelection,LevelSelection,Game;
    private int LevelNumber=1;


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
        }//Allow To Call This From Any Class

    public void Start()
    {

        GameModeSelection.gameObject.SetActive(true);
        LevelSelection.gameObject.SetActive(false);
        Game.gameObject.SetActive(false);

    }


    public void LaunchArcadeMode()
    {

        GameModeSelection.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
        SpawnLevel(Spawner.Difficulty.Easy);
        GameManager.instance.ResetGame(5);
        GameManager.instance.SetCurrentGameMode(GameManager.GameMode.Arcade);
        UIManager.instance.DisplayGameUI(true);

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

    }
}
