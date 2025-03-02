using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    
    [Header("Game Datas")]
    [Space(10)]
        private int Score=0;
        private int Lives=3;
        private bool isAbleToPlay;
        private float startDelay;

    [Header("Game Components")]
    [Space(10)]
        [SerializeField] private List<Ball> PlayerBalls=new List<Ball>();
        [SerializeField] private Transform PadleTransform;


    [Header("Audio Components")]
    [Space(10)]    
        private AudioClip breakSound;
        private AudioClip winSound;
        private AudioClip looseSound;
        [SerializeField] private AudioSource audiosource;

    //GETTERS
        public bool GetIsAbleToPlay(){return isAbleToPlay;}//Getter For isAbleToPlay
        public Transform GetPadleTransform(){return PadleTransform;}//Getter For PadleTransform

    //SETTERS

    //ESSENTIALS

        public static GameManager instance;
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

            if(Input.GetButtonDown("Cancel"))
            {

                if(UIManager.instance.GetisWindowOpen())
                {

                    UIManager.instance.CloseMenu();

                }else
                {

                    UIManager.instance.OpenMenu();

                }

            }

        }

    //

    public void SaveData()
    {}


    void ResetGame() 
    {
        // Show instruction panel
        UIManager.instance.DisplayStartPanel(true);
        // Start game after delay
        Invoke("StartGame", startDelay);
    }

    void StartGame() 
    {
        isAbleToPlay = true;
        UIManager.instance.DisplayStartPanel(false);
    }


    // Public method to update bricks count, score, called by Brick.cs
    public void RegisterBreak(int value, Brick brick) 
    {

        IncreaseScore(value);

        if (BrickManager.instance.Remove(brick)) 
        {
            WinLevel();
        }
        if (breakSound != null)
        {
            //Charge the audiosource with the right clip and play it
            audiosource.clip = breakSound;
            audiosource.Play();
        }
    }

    // Also called by Reward.cs
    public void IncreaseScore(int scoreAdded)
    {
        Score += scoreAdded;
    }

    void WinLevel() 
    {
        isAbleToPlay = false;
        if (winSound)
        {
            // PlayOneShot instantiates our audiosource, maybe already playing a break sound
            audiosource.PlayOneShot(winSound);
        }
        UIManager.instance.DisplayWinPanel(true);

    }

    // Called by Ball.cs
    public void LooseLevel()
    {
        isAbleToPlay = false;
        if (winSound)
        {
            audiosource.PlayOneShot(looseSound);
        }
        UIManager.instance.DisplayLosePanel(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
