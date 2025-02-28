using UnityEngine;

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


    public void LaunchArcadeMode(){}
    public void LeunchLevel(int LevelIndex){}


    public void DisplayLevelSelection()
    {

        LevelSelection.gameObject.SetActive(true);
        GameModeSelection.gameObject.SetActive(false);

    }
}
