using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        [SerializeField] private AudioSource MusicAudioSource;

    [Header("Audio Settings")]
    [Space(10)]
        private int SoundVolume; //0-100
    
    [Header("Bool Settings")]
    [Space(10)]
        private bool isFullScreen=true;

    //SETTERS
    
        public void SwitchFullScreen(){Screen.fullScreen=isFullScreen=!isFullScreen;}//Setter For isFullScreen

    //ESSENTIALS

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


    //AUDIO

        public void ChangeVolume(Slider AudioSlider)
        {

            SoundVolume=(int)AudioSlider.value;
            MusicAudioSource.volume=(float)SoundVolume/100;

        }

}
