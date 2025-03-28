using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("UI Components")]
    [Space(10)]

        [SerializeField] private Transform UIParent;

    [Header("UI Prefabs")]
    [Space(10)]
    
        [SerializeField] private GameObject MenuPrefab;
        [SerializeField] private GameObject WinPanelPrefab;
        [SerializeField] private GameObject LosePanelPrefab;
        [SerializeField] private GameObject StartPanelPrefab;
        [SerializeField] private ScoreBoard ScoreBoardPrefab;
        [SerializeField] private ScoreRecord ScoreRecordPrefab;
        [SerializeField] private GameUI GameUIPrefab;

    [Header("UI Datas")]
    [Space(10)]

        private GameObject MenuWindow,WinPanel,LosePanel,StartPanel;
        private GameUI GameUIDisplay;
        private ScoreBoard ScoreBoardDisplay;
        private ScoreRecord ScoreRecordDisplay;
        private ClassicBackground ClassicBackSprite;
        private bool isWindowOpen=false;

    //GETTERS

        public bool GetisWindowOpen(){return isWindowOpen;}//Getter For isWindowOpen

    //SETTERS

        public void SetClassicBackground(ClassicBackground src){ClassicBackSprite=src;}//Setter For ClassicBackSprite
        public void SetClassicBackgroundSprite(int index){ClassicBackSprite.SetImage(index);}

    //ESSENTIALS

        public static UIManager instance;
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
            DontDestroyOnLoad(this.gameObject);
        }//Allow To Call This From Any Class

        public void Update()
        {

            if(Input.GetKeyDown(KeyCode.Tab))
            {

                DisplayScoreBoard(true);

            }

            if(Input.GetKeyUp(KeyCode.Tab))
            {

                DisplayScoreBoard(false);

            }

        }

    public void HideUI()
    {

        DisplayLosePanel(false);
        DisplayWinPanel(false);
        DisplayGameUI(false);

    }

    public void UpdateScoreDisplay(int Score)
    {

        GameUIDisplay.SetScore(Score.ToString());

    }

    public void SetLevelDisplay(string LevelType,string LevelNumber)
    {

        GameUIDisplay.SetLevelTitle(LevelType);
        GameUIDisplay.SetLevel(LevelNumber);

    }

    public void DisplayGameUI(bool isDisplay)
    {

        if(GameUIDisplay==null)
            GameUIDisplay=Instantiate(GameUIPrefab,this.transform);

        GameUIDisplay.gameObject.SetActive(isDisplay);

    }


    public void OpenMenu()
    {

        if(MenuWindow==null)
            MenuWindow=Instantiate(MenuPrefab,this.transform);
        
        MenuWindow.gameObject.SetActive(true);
        isWindowOpen=true;

    }

    public void CloseMenu()
    {

        MenuWindow.gameObject.SetActive(false);
        isWindowOpen=false;

    }

    public void DisplayStartPanel(bool isDisplay)
    {

        if(StartPanel==null)
        {

            StartPanel=Instantiate(StartPanelPrefab,UIParent);

        }

        StartPanel.gameObject.SetActive(isDisplay);

    }

    public void DisplayWinPanel(bool isDisplay)
    {

        if(WinPanel==null)
        {

            WinPanel=Instantiate(WinPanelPrefab,UIParent);

        }

        WinPanel.gameObject.SetActive(isDisplay);

    }

    public void DisplayLosePanel(bool isDisplay)
    {

        if(LosePanel==null)
        {

            LosePanel=Instantiate(LosePanelPrefab,UIParent);

        }

        LosePanel.gameObject.SetActive(isDisplay);

    }

    public void DisplayScoreBoard(bool isDisplay)
    {

        if(ScoreBoardDisplay==null)
        {

            ScoreBoardDisplay=Instantiate(ScoreBoardPrefab,UIParent);

        }

        ScoreBoardDisplay.gameObject.SetActive(isDisplay);

    }

    public void DisplayScoreRecord(bool isDisplay)
    {

        if(ScoreRecordDisplay==null)
        {

            ScoreRecordDisplay=Instantiate(ScoreRecordPrefab,UIParent);

        }

        ScoreRecordDisplay.gameObject.SetActive(isDisplay);

    }

    public void SetScoreRecord()
    {

        //

    }

}

