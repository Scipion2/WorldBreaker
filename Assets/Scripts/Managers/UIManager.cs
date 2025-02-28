using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("UI Prefabs")]
    [Space(10)]
    
        [SerializeField] private GameObject MenuPrefab;
        [SerializeField] private GameObject WinPanelPrefab;
        [SerializeField] private GameObject LosePanelPrefab;
        [SerializeField] private GameObject StartPanelPrefab;

    private GameObject MenuWindow,WinPanel,LosePanel,StartPanel;
    private bool isWindowOpen=false;

    //GETTERS

        public bool GetisWindowOpen(){return isWindowOpen;}//Getter For isWindowOpen

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

        StartPanel.gameObject.SetActive(isDisplay);

    }

    public void DisplayWinPanel(bool isDisplay)
    {

        WinPanel.gameObject.SetActive(isDisplay);

    }

    public void DisplayLosePanel(bool isDisplay)
    {

        LosePanel.gameObject.SetActive(isDisplay);

    }

}
