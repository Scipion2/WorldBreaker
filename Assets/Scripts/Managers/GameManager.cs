using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    
    [HideInInspector] public bool canPlay;
    [HideInInspector] public Paddle paddle;

    public float startDelay;
    public AudioClip breakSound;
    public AudioClip winSound;
    public AudioClip looseSound;
    private AudioSource audiosource;
    private int score;

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

    public void SaveData()
    {}

    // Start is called before the first frame update, but we don't know which script will be executed first!
    // So, as the instance can be found by other scripts, this script has been set before default time in Edit>Project Setting>Scripts Execution Order.
    void Start()
    {
        
        audiosource = GetComponent<AudioSource>();
        // Cache the content of the existing text
       // ResetGame();
    }


    void ResetGame() 
    {
        // Show instruction panel
        UIManager.instance.DisplayStartPanel(true);
        // Start game after delay
        Invoke("StartGame", startDelay);
    }

    void StartGame() 
    {
        canPlay = true;
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
        score += scoreAdded;
    }

    void WinLevel() 
    {
        canPlay = false;
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
        canPlay = false;
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
