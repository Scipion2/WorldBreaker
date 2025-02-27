using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    
    [Header("Bool Settings")]
    [Space(10)]
        private bool isFullScreen=true;

    //SETTERS
    
        public void SwitchFullScreen(){Screen.fullScreen=isFullScreen=!isFullScreen;}//Setter For isFullScreen

    public static SettingsManager instance;
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

}
